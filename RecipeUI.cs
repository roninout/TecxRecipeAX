using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;
using TecxRecipeAX;
using System.Data.Entity;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;
using System.Globalization;
using DevExpress.XtraGrid.Columns;
using System.Threading;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraExport.Xls;
using TecxRecipeAX.Properties;
using DevExpress.XtraEditors.Repository;
using DevExpress.DocumentView;

namespace TechRecipeAX
{
    [ComVisible(true)]
    [Guid("A855AD2E-66E5-4961-8423-E28E37EA1A25")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(ITechRecipeAX))]
    [ComSourceInterfaces(typeof(ITechRecipeAXEvents))]

    public partial class RecipeUI : DevExpress.XtraEditors.XtraUserControl, ITechRecipeAX
    {
        private RecipeDbContext _context;
        private BindingList<StageGridItem> stageGridItems;
        private Recipe selectedRecipe;
        private bool writeToPLC;
        private bool isEditingCell = false;
        private CtApi _ctApi;

        private bool isShowingPLCData = false;
        private Dictionary<string, string> paramDictionary;

        private readonly SemaphoreSlim _plcReadLock = new SemaphoreSlim(1, 1);
        private CancellationTokenSource _plcReadCts;
        private readonly System.Windows.Forms.Timer _plcReadTimer = new System.Windows.Forms.Timer();
        private RecipePLC recipeToPLC;
        public int currentBaseRecipeId;

        public event Action<string> PageNameStrPropertyChanged;
        public event Action<int> CurStageIntPropertyChanged;
        public event Action<int> RecipeAccessIntPropertyChanged;
        public event Action<bool> RecipeModeIntPropertyChanged;

        #region External property

        private string pageNameStrProperty;
        public string PageNameStrProperty
        {
            get
            {
                return pageNameStrProperty;
            }
            set
            {
                if (pageNameStrProperty != value)
                {
                    pageNameStrProperty = value;
                    PageNameStrPropertyChanged?.Invoke(value);
                }
            }
        }

        private int сurStageIntProperty;
        public int CurStageIntProperty
        {
            get
            {
                return сurStageIntProperty;
            }
            set
            {
                if (сurStageIntProperty != value)
                {
                    сurStageIntProperty = value;
                    CurStageIntPropertyChanged?.Invoke(value);
                }
            }
        }

        private int recipeAccessIntProperty;
        public int RecipeAccessIntProperty
        {
            get
            {
                return recipeAccessIntProperty;
            }
            set
            {
                if (recipeAccessIntProperty != value)
                {
                    recipeAccessIntProperty = value;
                    RecipeAccessIntPropertyChanged?.Invoke(value);
                }
            }
        }

        private bool recipeModeIntProperty;
        public bool RecipeModeIntProperty
        {
            get
            {
                return recipeModeIntProperty;
            }
            set
            {
                if (recipeModeIntProperty != value)
                {
                    recipeModeIntProperty = value;
                    RecipeModeIntPropertyChanged?.Invoke(value);
                }
            }
        }

        #endregion

        public RecipeUI()
        {
            InitializeComponent();
            // Запускаем асинхронную инициализацию
            this.Load += async (sender, e) => await InitializeAsync();
            InitCtApi();
        }

        #region Init data

        private async Task InitializeAsync()
        {
            Logger.Info(@" -------------------- Начало инициализации приложения ---------------- ");

            try
            {
                UpdateButtonsState(false);
                await InitializeDbContextAsync();
                await LoadDataAsync(); // Теперь можно вызывать асинхронно
                ConfigureGridView();
                //LoadRecipeComboBox();

                // вызываем SetSelectedRecipe(PageNameStrProperty) по изменению значения
                SubscribeToPageNamePropertyChanged();

                InitializePlcReadTimer(); // запус
                Logger.Info(@"Инициализация приложения завершена успешно");
            }
            catch (Exception ex)
            {
                Logger.Error($"Ошибка при инициализации приложения: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }

        private async Task InitializeDbContextAsync()
        {
            try
            {
                string connectionString = ConfigHelper.GetConnectionString();
                Logger.Debug($"Получена строка подключения: {connectionString}");

                _context = new RecipeDbContext(connectionString);

                // Проверяем подключение асинхронно
                await _context.Database.Connection.OpenAsync();
                Logger.Info($"Версия SQL Server: {_context.Database.Connection.ServerVersion}");
                _context.Database.Connection.Close();

            }
            catch (System.Exception ex)
            {
                Logger.Error($"Ошибка подключения к БД: {ex.Message}");

                // Обработка ошибок подключения
                toolStripStatusLabel1.Text = $"Ошибка подключения к БД: {ex.Message}";
                toolStripStatusLabel1.ForeColor = Color.Red;
                throw; // Можно обработать иначе, в зависимости от требований
            }
        }

        private async Task LoadDataAsync()
        {
            Logger.Info("Старт асинхронной загрузки данных");

            try
            {
                if (_context == null) { await InitializeDbContextAsync(); }

                toolStripStatusLabel1.Text = "Подключение к базе данных...";
                toolStripStatusLabel1.ForeColor = Color.Black;

                progressBarControl1.Visible = true;
                progressBarControl1.EditValue = 0;

                int totalItems = await _context.RecipeStages.CountAsync();
                int currentItem = 0;

                var stages = await _context.RecipeStages.ToListAsync();
                Logger.Debug($"Загружено {stages.Count} записей");

                foreach (var stage in stages)
                {
                    currentItem++;
                    progressBarControl1.EditValue = (int)((double)currentItem / totalItems * 100);
                    await Task.Delay(5);
                }

                progressBarControl1.Visible = false;
                gridControlStages.Visible = true;
                toolStripStatusLabel1.Text = "Подключение установлено.";
                toolStripStatusLabel1.ForeColor = Color.Green;
                Logger.Info("Данные успешно загружены.");
            }
            catch (System.Exception ex)
            {
                Logger.Error("Ошибка при загрузке данных");
                toolStripStatusLabel1.Text = $"Ошибка: {ex.Message}";
                toolStripStatusLabel1.ForeColor = Color.Red;
                progressBarControl1.Visible = false;
            }
        }

        private async void InitCtApi()
        {
            _ctApi = new CtApi();
            _ctApi.SetCtApiDirectory(ConfigHelper.CtApiPath);
            await _ctApi.OpenAsync(ConfigHelper.CtApiIp, ConfigHelper.CtApiUser, ConfigHelper.CtApiPassword);

            var findResult = await _ctApi.FindAsync("Tag", "Tag=*S01_Recipe*", "", "TAG");
        }

        #endregion

        #region External properties events
        private void SubscribeToPageNamePropertyChanged()
        {
            // Проверим, не подписались ли уже (если нужно)
            PageNameStrPropertyChanged += (newValue) =>
            {
                Logger.Info($"PageNameStrProperty изменился: {newValue}");
                SetSelectedRecipe(newValue);
            };

            // Если значение уже есть — обработаем сразу
            if (!string.IsNullOrEmpty(PageNameStrProperty))
            {
                SetSelectedRecipe(PageNameStrProperty);
            }
        }
        #endregion

        #region Table
        // настройка таблицы - общее поведение
        private void ConfigureGridView()
        {
            gridViewStages.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gridViewStages.OptionsView.ColumnAutoWidth = false;
            gridViewStages.OptionsView.ShowGroupPanel = false;

            gridViewStages.OptionsBehavior.Editable = true;
            gridViewStages.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            gridViewStages.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            gridViewStages.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;

            //gridViewStages.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //gridViewStages.OptionsView.RowAutoHeight = true;

            gridViewStages.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            gridViewStages.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridViewStages.Appearance.HeaderPanel.Font = new Font(gridViewStages.Appearance.HeaderPanel.Font, FontStyle.Bold);
            gridViewStages.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gridViewStages.BestFitColumns();
        }

        // инициализация колонок в таблице
        private void CreateGridColumns(BindingList<StageGridItem> items)
        {
            var gridView = gridViewStages;
            gridView.Columns.Clear();

            // Столбец №
            var colStageNumb = gridView.Columns.AddVisible(nameof(StageGridItem.stageNumb), "№");
            colStageNumb.OptionsColumn.AllowEdit = false;
            colStageNumb.Width = 30;
            colStageNumb.OptionsColumn.FixedWidth = true;

            // Столбец Stage
            var colStageName = gridView.Columns.AddVisible(nameof(StageGridItem.name), "Стадія");
            colStageName.OptionsColumn.AllowEdit = true;
            colStageName.UnboundType = DevExpress.Data.UnboundColumnType.String;
            colStageName.Width = 175; // 150
            colStageName.OptionsColumn.FixedWidth = true;

            // Теперь динамические параметры
            if (items.Any())
            {
                var baseParameters = _context.BaseParameters.ToList(); // Загружаем BaseParameters один раз

                // Собираем все параметры из всех строк
                var paramInfoList = items
                    .SelectMany(item => item.Parameters.Values) // берем значения, а не ключи
                    .GroupBy(p => p.BaseParameterId)
                    .Select(g => g.First()) // уникальные по BaseParameterId
                    .ToList();

                // Сортировка по BaseParameter.type
                var sortedParameters = paramInfoList
                .Select(p =>
                {
                    var paramInfo = baseParameters.FirstOrDefault(bp => bp.id == p.BaseParameterId);
                    return new
                    {
                        Id = p.BaseParameterId,
                        Name = p.Name,
                        Type = paramInfo?.type ?? int.MaxValue,
                        Description = paramInfo?.description ?? "",
                        Kind = paramInfo.kind,
                        Format = paramInfo?.format
                    };
                })
                .OrderBy(p => p.Type)
                .ThenBy(p => p.Name)
                .ToList();

                foreach (var param in sortedParameters)
                {
                    var description = (param.Description ?? "").Replace("\\n", Environment.NewLine);

                    var col = gridView.Columns.AddField(param.Name);
                    col.Caption = param.Name + Environment.NewLine + description;
                    col.Visible = true;
                    col.OptionsColumn.FixedWidth = false;
                    col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
                    col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    if (param.Kind == ParamKind.InputField)
                    {
                        col.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                        col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        col.DisplayFormat.FormatString = param.Format.Value.ToString() == "Default" ? "" : param.Format.Value.ToString(); // N2
                        //col.Width = 57; // 100
                    }
                    if (param.Kind == ParamKind.CheckBox)
                    {
                        col.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
                        col.Width = 40; // 100
                        col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;                        
                        col.Caption = string.Join("\n", param.Name.ToCharArray()); // Разбиваем текст заголовка по буквам с переносом
                        col.OptionsColumn.FixedWidth = true;
                    }
                }
            }

            // Столбец Auto
            var colStageAuto = gridView.Columns.AddVisible(nameof(StageGridItem.auto), "Auto");
            colStageAuto.OptionsColumn.AllowEdit = true;
            colStageAuto.Width = 40;
            colStageAuto.OptionsColumn.FixedWidth = true;
            // Поворачиваем текст в заголовке на 90 градусов
            colStageAuto.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            colStageAuto.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colStageAuto.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            // Разбиваем текст заголовка по буквам с переносом
            colStageAuto.Caption = string.Join("\n", "Auto".ToCharArray());


            // отображение значений в ячейках по центру, кроме колонки "name"
            foreach (GridColumn column in gridViewStages.Columns)
                column.AppearanceCell.TextOptions.HAlignment = column.FieldName != "name" ? DevExpress.Utils.HorzAlignment.Center : DevExpress.Utils.HorzAlignment.Default;
            //gridView.BestFitColumns();
        }

        // событие выделение строки зеленым цветом
        private void gridViewStages_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.RowHandle >= 0)
            {
                // Получаем значение stageNumb текущей строки
                var stageNumberObj = view.GetRowCellValue(e.RowHandle, "stageNumb");

                if (stageNumberObj != null && int.TryParse(stageNumberObj.ToString(), out int stageNumber))
                {
                    if (stageNumber == CurStageIntProperty)
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                        e.HighPriority = true; // обязательно, чтобы перекрыть другие стили
                    }
                }
            }
        }

        // событие отрисовки серых ячеек
        private void gridViewStages_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            var stageItem = gridViewStages.GetRow(e.RowHandle) as StageGridItem;

            if (stageItem != null
                //&& !e.Column.FieldName.Equals(nameof(StageGridItem.stageNumb))
                && !e.Column.FieldName.Equals(nameof(StageGridItem.name))
                && !e.Column.FieldName.Equals(nameof(StageGridItem.auto)))
            {
                bool hasParam = stageItem.Parameters.ContainsKey(e.Column.FieldName);

                if (!hasParam)
                {
                    e.Appearance.BackColor = SystemColors.Control; //Color.LightGray;
                    e.Appearance.ForeColor = SystemColors.GrayText; //Color.DarkGray;
                    e.Appearance.Options.UseBackColor = true;
                    e.Appearance.Options.UseForeColor = true;
                }
                else if (isShowingPLCData)
                {
                    var param = stageItem.Parameters[e.Column.FieldName];
                    if (param.HasDifference)
                    {
                        // Жёлтый цвет — значение отличается от БД
                        e.Appearance.BackColor = Color.LightYellow;
                        e.Appearance.Options.UseBackColor = true;
                    }
                }
            }

            //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        }

        // события запрета ввода данных в серых ячейках
        private void gridViewStages_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null || view.FocusedRowHandle < 0 || view.FocusedColumn == null)
                return;

            var stageItem = view.GetRow(view.FocusedRowHandle) as StageGridItem;
            if (stageItem == null)
                return;

            string fieldName = view.FocusedColumn.FieldName;

            if (!fieldName.Equals(nameof(StageGridItem.stageNumb)) &&
                !fieldName.Equals(nameof(StageGridItem.name)) &&
                !fieldName.Equals(nameof(StageGridItem.auto)))
            {
                bool hasParam = stageItem.Parameters.ContainsKey(fieldName);

                if (!hasParam)
                {
                    e.Cancel = true;  // ОТМЕНЯЕМ редактирование только этой ячейки!
                }
            }
        }

        // отлавливаем изменение параметров и сохраняем
        private void GridViewStages_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Row is StageGridItem stageItem && e.Column != null)
            {
                string paramName = e.Column.FieldName;

                if (stageItem.Parameters.TryGetValue(paramName, out var param))
                {
                    if (e.IsGetData)
                    {
                        // Проверяем, чекбокс-ли это колонка
                        e.Value = e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Boolean ? param.Value != 0 : (object)param.Value;
                    }
                    else if (e.IsSetData && e.Value != null)
                    {
                        double newValue;

                        if (e.Column.UnboundType == DevExpress.Data.UnboundColumnType.Boolean)
                        {
                            // Преобразуем bool в decimal
                            newValue = (bool)e.Value ? 1 : 0;
                        }
                        else if (double.TryParse(e.Value.ToString(), out double doubleValue))
                        {
                            newValue = (double)doubleValue;
                        }
                        else
                        {
                            // Если не смогли преобразовать число — выходим
                            return;
                        }

                        // Обновляем значение
                        param.Value = newValue;

                        // Записываем в PLC или в БД
                        if (isShowingPLCData)
                            WriteSingleTagToPLC(stageItem, paramName, (double)newValue);
                        else
                            SaveParameterValueToDatabase(stageItem.RecipeStageId, paramName, (double)newValue);
                    }
                }
            }
        }

        // валидация ячеек параметров
        private void gridViewStages_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;

            string fieldName = view.FocusedColumn?.FieldName;
            // Разрешаем редактирование текстового имени без числовой валидации
            if (fieldName == "name")
            {
                e.Valid = true;
            }
            else
            {
                if (e.Value is string strValue)
                {
                    // Заменяем запятую на точку
                    string normalized = strValue.Replace(',', '.');

                    if (double.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                    {
                        e.Value = result;
                    }
                    else
                    {
                        e.Valid = false;
                        e.ErrorText = "Введите числовое значение.";
                    }
                }
            }
        }

        // событие для отлавливание изменений в колоках auto и name
        private void gridViewStages_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            // Проверяем, что изменена колонка 'auto'
            if (e.Column.FieldName == "auto")
            {
                var item = stageGridItems[e.RowHandle];
                item.auto = (bool)e.Value;  // Обновляем значение 'auto' в StageGridItem

                // Сохраняем изменения в базе данных
                SaveStageAutoToDatabase(item.RecipeStageId, item.auto);
            }
            // Проверяем, что изменена колонка 'name'
            else if (e.Column.FieldName == "name")
            {
                var item = stageGridItems[e.RowHandle];
                item.name = (string)e.Value;  // Обновляем значение 'name' в StageGridItem

                // Сохраняем изменения в базе данных
                SaveStageNameToDatabase(item.RecipeStageId, item.name);
            }
        }

        // блокируем обновлени таблицы
        private void gridViewStages_ShownEditor(object sender, EventArgs e)
        {
            isEditingCell = true;
        }

        // разрешаем обновлени таблицы
        private void gridViewStages_HiddenEditor(object sender, EventArgs e)
        {
            isEditingCell = false;
        }

        #endregion

        #region ComboBox
        private void LoadRecipeComboBox()
        {
            var recipes = _context.Recipes.Where(r => r.baseRecipeId == currentBaseRecipeId && r.id != currentBaseRecipeId).ToList();

            lookUpEditRecipe.Properties.DataSource = recipes;
            lookUpEditRecipe.Properties.DisplayMember = "name";
            lookUpEditRecipe.Properties.ValueMember = "id";

            // Только одна колонка с именем
            lookUpEditRecipe.Properties.Columns.Clear();
            lookUpEditRecipe.Properties.Columns.Add(new LookUpColumnInfo("name", "Рецепт"));

            // Убираем заголовок, если не нужен
            lookUpEditRecipe.Properties.ShowHeader = false;

            // Устанавливаем первый элемент по умолчанию
            if (recipes.Count > 0)
            {
                lookUpEditRecipe.EditValue = selectedRecipe.id;
            }
        }

        private void lookUpEditRecipe_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditRecipe.EditValue != null)
            {
                int selectedId = Convert.ToInt32(lookUpEditRecipe.EditValue);
                selectedRecipe = _context.Recipes.FirstOrDefault(r => r.id == selectedId);

                if (selectedRecipe != null)
                {
                    LoadRecipeStagesAndParameters(selectedRecipe.id);
                    textEditRecipeName.EditValue = $"[DB] {selectedRecipe.name}";

                    var findRecipe = _context.RecipePages.FirstOrDefault(rp => rp.baseRecipeId == currentBaseRecipeId && rp.page == pageNameStrProperty);
                    btnAssignRecipeToPages.Enabled = findRecipe.recipeId != selectedId;
                }
            }
        }

        #endregion

        #region Регистрация класса в реестре
        [ComRegisterFunction()]
        public static void RegisterClass(string key)
        {
            // Удаляем HKEY_CLASSES_ROOT\ из переданного ключа
            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // Открываем ключ CLSID\{guid} в режиме записи
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // Создаем ключ элемента управления ActiveX – это позволяет ему появиться
            //в контейнере элемента управления ActiveX
            RegistryKey ctrl = k.CreateSubKey("Control");
            ctrl.Close();

            // Создаем запись CodeBase
            RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);
            inprocServer32.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
            inprocServer32.Close();

            // И в заключении закрываем ключ реестра 
            k.Close();
        }

        [ComUnregisterFunction()]
        public static void UnregisterClass(string key)
        {
            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // Открываем ключ HKCR\CLSID\{guid} в режиме записи
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // Удаляем ключ элемента управления ActiveX 
            k.DeleteSubKey("Control", false);

            // Затем открываем ключ InprocServer32
            RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);

            // Удаляем ключ CodeBase
            k.DeleteSubKey("CodeBase", false);

            // И в заключении закрываем ключ реестра
            k.Close();
        }
        #endregion

        #region Form buttons
        // делаем кнопки неактивными
        private void UpdateButtonsState(bool type)
        {
            btnDebug.Visible = false;
            btnSaveAs.Visible = false;
            btnRefreshRecipe.Visible = false;

            checkButtonBaseStage.Visible = RecipeAccessIntProperty > 2;
            checkBoxShowPLC.Visible = RecipeAccessIntProperty > 1;

            btnEditParameters.Visible = type && RecipeAccessIntProperty > 2;
            btnEditStages.Visible = type && RecipeAccessIntProperty > 2;
            btnEditRecipe.Visible = type && RecipeAccessIntProperty > 1;
            btnDataToPlc.Visible = type && RecipeAccessIntProperty > 1;

            lookUpEditRecipe.Visible = type && RecipeAccessIntProperty > 1;
            btnAssignRecipeToPages.Visible = type && RecipeAccessIntProperty > 1;
            lbSelectRecipe.Visible = type && RecipeAccessIntProperty > 1;

            // отображение кнопок в режиме онлайн
            btnSaveFromPLC.Visible = !type && RecipeAccessIntProperty > 1;
        }

        // создание и редактирование параметров
        private void btnEditParameters_Click(object sender, EventArgs e)
        {
            using (var form = new FormEditParameters(_context, currentBaseRecipeId))
            {
                form.ShowDialog();
            }
        }

        // создание и редактирование стадий
        private void btnEditStages_Click(object sender, EventArgs e)
        {
            using (var form = new FormEditStages(_context, currentBaseRecipeId))
            {
                form.ShowDialog();
            }
        }

        // создание и редактирование рецептов
        private void btnEditRecipe_Click(object sender, EventArgs e)
        {
            using (var form = new RecipeEditForm(_context, currentBaseRecipeId, selectedRecipe, RecipeAccessIntProperty))
            {
                form.ShowDialog();
            }
        }

        // перечитываем текущий рецепт
        private void btnRefreshRecipe_Click(object sender, EventArgs e)
        {
            if (selectedRecipe != null)
                RefreshData(selectedRecipe.id);
        }

        // Отображение логов и CtApi
        private void btnDebug_Click(object sender, EventArgs e)
        {
            using (var form = new FormDebug(_context, _ctApi))
            {
                form.ShowDialog();
            }
        }

        // Записать параметры в PLC
        private void btnDataToPlc_Click(object sender, EventArgs e)
        {
            if (RecipeModeIntProperty)
            {
                XtraMessageBox.Show($"Завантаження рецепту до PLC можливо лише в ручному режимі реактора/пермікса!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmResult = XtraMessageBox.Show($"Ви дійсно хочете завантажити дані рецепту «{selectedRecipe.name}» до PLC?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult != DialogResult.Yes)
                return;

            var plcData = PrepareRecipeDate();
            var paramDictionary = CreateParamTagsForPLC(plcData);

            if (!writeToPLC)
            {
                WriteToPLC(paramDictionary);
                XtraMessageBox.Show($"Рецепт «{selectedRecipe.name}» успішно завантажений до контроллера", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                Logger.Warning("Я ще не закінчив писати в PLC!!!!");
        }

        // переключение источника для отображения в таблице
        private async void checkBoxShowPLC_CheckedChanged(object sender, EventArgs e)
        {
            isShowingPLCData = checkBoxShowPLC.Checked;

            if (isShowingPLCData)
            {
                recipeToPLC = PrepareRecipeDate();                          // формируем данные с текущего рецепта
                paramDictionary = CreateParamTagsForPLC(recipeToPLC);       // Dictionary c тегами и значениями для записи
                var recipeFromPLC = CopyStrucToRecipeFromPLC(recipeToPLC);  // создаем recipeFromPLC с recipeToPLC
                var plcValues = await ReadFromPLC(recipeFromPLC);           // читаем данные с PLC
                UpdateRecipeFromPLCValues(recipeFromPLC, plcValues);        // обновляем данные с PLC в recipeFromPLC

                LoadRecipeFromPLC(recipeFromPLC);                           // отображаем в таблице

                checkBoxShowPLC.ImageOptions.SvgImage = Resources.electronics_desktopmac;
                textEditRecipeName.ForeColor = Color.Gray; //Color.FromArgb(64, 64, 64);
                textEditRecipeName.EditValue = $"[PLC] {selectedRecipe.name}";
            }
            else
            {
                LoadRecipeFromDatabase();

                checkBoxShowPLC.ImageOptions.SvgImage = Resources.editdatasource;
                textEditRecipeName.ForeColor = Color.Orange;
                textEditRecipeName.EditValue = $"[DB] {selectedRecipe.name}";
            }

            UpdateButtonsState(!isShowingPLCData); // делаем кнопки неактивными
        }

        // переключение источника для отображения в таблице (Base stages)
        private void checkButtonBaseStage_CheckedChanged(object sender, EventArgs e)
        {
            int recipeId = _context.RecipePages.FirstOrDefault(r => r.page == PageNameStrProperty).recipeId;
            if (checkButtonBaseStage.Checked)
            {
                SetBaseRecipe(recipeId);
                checkButtonBaseStage.ImageOptions.SvgImage = Resources.bo_attention;
            }
            else
            {
                var curSelectedRecipe = _context.Recipes.FirstOrDefault(r => r.id == recipeId);
                if (curSelectedRecipe != null)
                    selectedRecipe = curSelectedRecipe;
                checkButtonBaseStage.ImageOptions.SvgImage = null;
            }

            // Вызываем событие checkBoxShowPLC_CheckedChanged
            checkBoxShowPLC_CheckedChanged(checkBoxShowPLC, EventArgs.Empty);
        }

        // сохранение текущего рецепта как...
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            var availableRecipes = _context.Recipes.ToList();

            using (var form = new FormRecipeCopy(availableRecipes))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string newName = form.NewRecipeName;
                    var replaceRecipe = form.SelectedRecipeToReplace;

                    CreateNewRecipeFromPLC(newName, replaceRecipe);
                }
            }
        }

        // сохранение рецепта из PLC в свой рецепт
        private async void btnSaveFromPLC_Click(object sender, EventArgs e)
        {
            if (selectedRecipe == null || stageGridItems == null)
            {
                MessageBox.Show("Немає даних для збереження.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                "Ви дійсно хочете зберегти поточні значення, зчитані з PLC, у базу даних ?\n" +
                "Це перезапише існуючі значення параметрів рецепту.",
                "Підтвердження збереження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                // Загружаем рецепт со стадиями и параметрами из контекста
                var recipe = await _context.Recipes
                    .Include(r => r.RecipeStages.Select(s => s.RecipeParameters))
                    .FirstOrDefaultAsync(r => r.id == selectedRecipe.id);

                if (recipe == null)
                {
                    MessageBox.Show("Рецепт не знайдено у базі даних.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Обновляем только значения параметров
                foreach (var item in stageGridItems)
                {
                    var stage = recipe.RecipeStages.FirstOrDefault(s => s.stageNumb == item.stageNumb);
                    if (stage == null)
                        continue;

                    foreach (var kvp in item.Parameters)
                    {
                        var paramName = kvp.Key;
                        var newValue = kvp.Value.Value;

                        var parameter = stage.RecipeParameters.FirstOrDefault(p => p.name == paramName);
                        if (parameter != null)
                            parameter.value = newValue;
                    }
                }

                await _context.SaveChangesAsync();
                MessageBox.Show("Значення з PLC успішно збережені в базі даних.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Logger.Error($"Помилка при збереженні PLC-значень у БД: {ex.Message}");
                MessageBox.Show("Помилка збереження даних. Докладніше див. у логах.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // устанавливать recipeId в таблице RecipePage
        private void btnAssignRecipeToPages_Click(object sender, EventArgs e)
        {
            if (selectedRecipe == null)
            {
                XtraMessageBox.Show("Рецепт не вибраний.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(pageNameStrProperty))
            {
                XtraMessageBox.Show("Ім'я сторінки не вказано.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmResult = XtraMessageBox.Show($"Ви дійсно хочете зробити рецепт «{selectedRecipe.name}» поточним?","Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult != DialogResult.Yes)
                return;

            var findRecipe = _context.RecipePages.FirstOrDefault(rp => rp.baseRecipeId == currentBaseRecipeId && rp.page == pageNameStrProperty);                ;

            if (findRecipe == null)
            {
                XtraMessageBox.Show("Немає сторінок із вказаним baseRecipeId та ім'ям сторінки.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                findRecipe.recipeId = selectedRecipe.id;
                _context.SaveChanges();

                // обновим selectedRecipe на всякий случай
                selectedRecipe = _context.Recipes.FirstOrDefault(r => r.id == selectedRecipe.id);
                // обновим представление
                LoadRecipeFromDatabase();

                btnAssignRecipeToPages.Enabled = findRecipe.recipeId != Convert.ToInt32(lookUpEditRecipe.EditValue);
                XtraMessageBox.Show("Рецепт успішно призначений як поточний.","Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Stuff

        // устанавливаем рецепт через PlantScada
        private void SetSelectedRecipe(string pageName)
        {
            int selectedId = _context.RecipePages.FirstOrDefault(r => r.page == pageName).recipeId;
            currentBaseRecipeId = _context.RecipePages.FirstOrDefault(r => r.recipeId == selectedId).baseRecipeId; // запоминаем как глобальную переменную

            if (selectedId == 0)
            {
                Logger.Error($"Рецепт за поточною сторінкою {pageName} - відсутній!!!");
            }

            if (selectedId != 0)
            {
                selectedRecipe = _context.Recipes.FirstOrDefault(r => r.id == selectedId);
                if (selectedRecipe == null)
                {
                    checkBoxShowPLC.Checked = false;
                    UpdateButtonsState(true); // делаем кнопки активными
                    LoadRecipeFromDatabase();

                    checkBoxShowPLC.ImageOptions.SvgImage = Resources.editdatasource;
                    textEditRecipeName.ForeColor = Color.Orange;
                    textEditRecipeName.EditValue = $"[DB] {selectedRecipe.name}";
                }
                else
                {
                    LoadRecipeStagesAndParameters(selectedRecipe.id);
                    checkBoxShowPLC.Checked = true;
                }
                // инициализация комбобоксов
                LoadRecipeComboBox();
            }
        }

        // устанавливаем базовый рецепт
        private void SetBaseRecipe(int recipeId)
        {
            int selectedId = _context.RecipePages.FirstOrDefault(r => r.recipeId == recipeId).baseRecipeId;

            if (selectedId == 0)
                Logger.Error($"Базовий рецепт для рецепта з id: {recipeId} - відсутній!!!");

            if (selectedId != 0)
                selectedRecipe = _context.Recipes.FirstOrDefault(r => r.id == selectedId);
        }

        // отвечает за вычитку стадий и параметром по selectedRecipe
        private void LoadRecipeStagesAndParameters(int recipeId)
        {
            var recipe = _context.Recipes.Include(r => r.RecipeStages.Select(rs => rs.RecipeParameters)).FirstOrDefault(r => r.id == recipeId);

            if (recipe != null)
            {
                selectedRecipe = recipe;
                var sortedStages = recipe.RecipeStages.OrderBy(s => s.stageNumb).ToList();
                var baseParameters = _context.BaseParameters.ToList(); // Загружаем все BaseParameters один раз

                stageGridItems = new BindingList<StageGridItem>();

                foreach (var stage in sortedStages)
                {
                    var item = new StageGridItem
                    {
                        RecipeStageId = stage.id,
                        stageNumb = stage.stageNumb,
                        name = stage.name,
                        auto = stage.auto
                    };

                    foreach (var param in stage.RecipeParameters)
                    {
                        item.Parameters[param.name] = new ParamInfo
                        {
                            BaseParameterId = param.baseParameterId,
                            Name = param.name,
                            Value = param.value
                        };
                    }

                    stageGridItems.Add(item);
                }

                gridControlStages.DataSource = stageGridItems;
                CreateGridColumns(stageGridItems);
                RefreshGridColumns();
            }
        }

        // метод сохранения параметров
        private void SaveParameterValueToDatabase(int recipeStageId, string paramName, double newValue)
        {
            var stage = _context.RecipeStages
                .Include(rs => rs.RecipeParameters)
                .FirstOrDefault(rs => rs.id == recipeStageId);

            if (stage != null)
            {
                var param = stage.RecipeParameters.FirstOrDefault(p => p.name == paramName);
                if (param != null)
                {
                    param.value = newValue;
                    _context.SaveChanges();
                }
            }
        }

        // метод сохраняет изменения в колонке auto
        private void SaveStageAutoToDatabase(int recipeStageId, bool newAutoValue)
        {
            var stage = _context.RecipeStages.FirstOrDefault(rs => rs.id == recipeStageId);

            if (stage != null)
            {
                stage.auto = newAutoValue;
                _context.SaveChanges();
            }
        }

        // метод сохраняет изменения в колонке name
        private void SaveStageNameToDatabase(int recipeStageId, string newName)
        {
            var stage = _context.RecipeStages.FirstOrDefault(rs => rs.id == recipeStageId);

            if (stage != null)
            {
                stage.name = newName;
                _context.SaveChanges();
            }
        }

        // обновляем таблицу при смене рецепта
        private void RefreshData(int recipeId)
        {
            _context.Dispose();
            _context = new RecipeDbContext(ConfigHelper.GetConnectionString());
            LoadRecipeStagesAndParameters(recipeId);
            RefreshGridColumns();
        }

        // обновляем колонки - если их имена (параметры) изменились
        private void RefreshGridColumns()
        {
            if (stageGridItems == null || stageGridItems.Count == 0)
                return;

            CreateGridColumns(stageGridItems);
        }

        // загружаем данные в таблицу с PLC
        private void LoadRecipeFromPLC(RecipePLC recipe)
        {
            if (recipe == null)
                return;

            // Сохраняем текущую строку
            int focusedRowHandle = gridViewStages.FocusedRowHandle;

            var gridItems = recipe.Stages
                .OrderBy(s => s.StageNumb)
                .Select(stage => new StageGridItem
                {
                    RecipeStageId = 0, // из PLC id не нужен
                    stageNumb = stage.StageNumb,
                    name = stage.Name,
                    auto = stage.Auto,
                    Parameters = stage.Parameters.ToDictionary(p => p.Name, p => p)
                })
                .ToList();
            this.Invoke(new Action(() =>
            {
                stageGridItems.Clear();

                foreach (var item in gridItems)
                    stageGridItems.Add(item);

                // Добавляем сравнение с БД
                UpdatePLCDataDifferences(selectedRecipe, stageGridItems);

                // Восстанавливаем выделение строки (если индекс корректен)
                if (focusedRowHandle >= 0 && focusedRowHandle < gridViewStages.RowCount)
                    gridViewStages.FocusedRowHandle = focusedRowHandle;

                Logger.Info("Данные с PLC в таблицу загружены успешно!");
            }));
        }

        // загружаем данные в таблицу с DateBase
        private void LoadRecipeFromDatabase()
        {
            var baseParams = _context.BaseParameters.ToList();

            var gridItems = selectedRecipe.RecipeStages
                .OrderBy(s => s.stageNumb)
                .Select(s => new StageGridItem
                {
                    RecipeStageId = s.id,
                    stageNumb = s.stageNumb,
                    name = s.name,
                    auto = s.auto,
                    Parameters = s.RecipeParameters.ToDictionary(
                        p => p.name,
                        p =>
                        {
                            var baseParam = baseParams.FirstOrDefault(b => b.name == p.name);
                            return new ParamInfo
                            {
                                BaseParameterId = baseParam.id,
                                Name = p.name,
                                Value = p.value,
                                Tag = baseParam?.tag,
                                Type = baseParam?.type ?? int.MaxValue,
                                Kind = baseParam.kind,
                                Format = baseParam.format
                            };
                        })
                })
                .ToList();

            stageGridItems.Clear();

            foreach (var item in gridItems)
                stageGridItems.Add(item);

            Logger.Info("Данные с DB в таблицу загружены успешно!");
        }

        // метод для сравнения и хранения отличий
        private void UpdatePLCDataDifferences(Recipe selectedRecipe, BindingList<StageGridItem> stageGridItems)
        {
            if (selectedRecipe?.RecipeStages == null) return;

            // Создаём словарь для быстрого доступа к параметрам из базы данных по (StageNumb, ParamName)
            var dbValues = selectedRecipe.RecipeStages
                .SelectMany(stage => stage.RecipeParameters.Select(param =>
                    new
                    {
                        Key = (stage.stageNumb, param.name),
                        Value = param.value
                    }))
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var stageItem in stageGridItems)
            {
                foreach (var paramEntry in stageItem.Parameters)
                {
                    string paramName = paramEntry.Key;
                    double plcValue = paramEntry.Value.Value;

                    var key = (stageItem.stageNumb, paramName);

                    if (dbValues.TryGetValue(key, out double dbValue))
                    {
                        // сравнение значений, например, с учетом допусков
                        if (Math.Abs(plcValue - dbValue) > 0.0001)
                        {
                            // Запоминаем, что есть отличие
                            paramEntry.Value.HasDifference = true;
                        }
                        else
                        {
                            paramEntry.Value.HasDifference = false;
                        }
                    }
                }
            }

            gridViewStages.RefreshData(); // Обновить отображение
        }

        // метод создания рецепта из данных PLC
        private void CreateNewRecipeFromPLC(string newRecipeName, Recipe recipeToReplace = null)
        {
            if (recipeToPLC == null)
            {
                XtraMessageBox.Show("Нет данных из PLC.");
                return;
            }

            var newRecipe = new Recipe
            {
                name = newRecipeName,
                //created_at = DateTime.Now,
                RecipeStages = new List<RecipeStage>()
            };

            foreach (var plcStage in recipeToPLC.Stages)
            {
                var newStage = new RecipeStage
                {
                    stageNumb = plcStage.StageNumb,
                    name = plcStage.Name,
                    auto = plcStage.Auto,
                    RecipeParameters = new List<RecipeParameter>()
                };

                foreach (var param in plcStage.Parameters)
                {
                    newStage.RecipeParameters.Add(new RecipeParameter
                    {
                        name = param.Name,
                        value = param.Value
                    });
                }

                newRecipe.RecipeStages.Add(newStage);
            }

            if (recipeToReplace != null)
            {
                // Удаляем старый рецепт
                _context.Recipes.Remove(recipeToReplace);
            }

            _context.Recipes.Add(newRecipe);
            _context.SaveChanges();

            Logger.Info($"Создан новый рецепт \"{newRecipeName}\"");
            XtraMessageBox.Show("Рецепт успешно сохранён!");

            // Обновить список рецептов в ComboBox
            //LoadRecipeComboBox();
        }

        #endregion

        #region PLC

        // формируем RecipeToPLC тип с текущим рецептом
        private RecipePLC PrepareRecipeDate()
        {
            if (selectedRecipe == null)
                return null;

            var baseParams = _context.BaseParameters.ToList();
            var baseStages = _context.BaseRecipeStages.ToList();

            var stages = selectedRecipe.RecipeStages
                .OrderBy(s => s.stageNumb)
                .Select(stage => new RecipeStagePLC
                {
                    StageNumb = stage.stageNumb,
                    BaseStageNumb = baseStages.FirstOrDefault(b => b.id == stage.baseRecipeStageId).description,
                    Name = stage.name,
                    Tag = stage.description,
                    Auto = stage.auto,
                    Parameters = stage.RecipeParameters
                        .Select(p =>
                        {
                            //var bp = baseParams.FirstOrDefault(b => b.name == p.name);
                            var bp = baseParams.FirstOrDefault(b => b.id == p.baseParameterId);
                            return new ParamInfo
                            {
                                Name = p.name,
                                Value = p.value,
                                Type = bp?.type ?? int.MaxValue,
                                Tag = bp.tag,
                                Kind = bp.kind,
                                Format = bp.format
                            };
                        })
                        .OrderBy(p => p.Type)
                        .ThenBy(p => p.Name)
                        .ToList()
                })
                .ToList();

            Logger.Info("Формирование данных для RecipePLC завершено!");

            return new RecipePLC
            {
                RecipeId = selectedRecipe.id,
                RecipeName = selectedRecipe.name,
                Stages = stages
            };
        }

        // преобразовывем RecipeToPLC в теги для PLC + логирование
        private Dictionary<string, string> CreateParamTagsForPLC(RecipePLC recipe)
        {
            var result = new Dictionary<string, string>();

            if (recipe == null || recipe.Stages == null)
            {
                Logger.Warning("CreateTagsForPLC: Пустой рецепт или стадии.");
                return result;
            }

            foreach (var stage in recipe.Stages)
            {
                if (string.IsNullOrWhiteSpace(stage.Tag))
                {
                    Logger.Warning($"Stage {stage.Name} не имеет Tag. Пропуск.");
                    continue;
                }

                // для констант ------------------------------------------
                string keyNum = $"{stage.Tag}_{stage.StageNumb}_NUM";
                if (!result.ContainsKey(keyNum))
                    result.Add(keyNum, stage.BaseStageNumb.ToString());

                string keyAuto = $"{stage.Tag}_{stage.StageNumb}_AUTO_CONT";
                if (!result.ContainsKey(keyAuto))
                    result.Add(keyAuto, stage.Auto.ToString());

                // для параметров ----------------------------------------
                foreach (var param in stage.Parameters)
                {
                    if (string.IsNullOrWhiteSpace(param.Tag))
                    {
                        Logger.Warning($"Параметр {param.Name} в стадии {stage.Name} не имеет Tag. Пропуск.");
                        continue;
                    }

                    string key = $"{stage.Tag}_{stage.StageNumb}_{param.Tag}";
                    var value = param.Value.ToString();

                    if (!result.ContainsKey(key))
                    {
                        result.Add(key, value);
                    }
                    else
                    {
                        Logger.Warning($"Дубликат ключа '{key}' в CreateTagsForPLC. Старое значение: {result[key]}, новое: {value}. Пропуск.");
                    }
                }
            }

            Logger.Info($"Результат CreateTagsForPLC: Рецепт: ID={recipe.RecipeId}, Name=\"{recipe.RecipeName}\"");
            foreach (var kvp in result.OrderBy(k => k.Key))
            {
                Logger.Info($"  {kvp.Key} = {kvp.Value}");
            }

            return result;
        }

        // записать рецепт в PlanScada - CtApi
        private async void WriteToPLC(Dictionary<string, string> paramTags)
        {
            writeToPLC = true;

            try
            {
                foreach (var paramTag in paramTags.OrderBy(k => k.Key))
                    await _ctApi.TagWriteAsync(paramTag.Key, paramTag.Value);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error write recipe tags to PLC: {ex.Message}");
                writeToPLC = false;
            }
            Logger.Info("Finished write recipe tags to PLC");
            writeToPLC = false;
        }

        // cоздание копии RecipePLC
        private RecipePLC CopyStrucToRecipeFromPLC(RecipePLC template)
        {
            if (template == null)
                return null;

            var result = new RecipePLC
            {
                RecipeId = template.RecipeId,
                RecipeName = template.RecipeName,
                Stages = template.Stages
                    .Select(stage => new RecipeStagePLC
                    {
                        StageNumb = stage.StageNumb,
                        BaseStageNumb = stage.BaseStageNumb,
                        Name = stage.Name,
                        Tag = stage.Tag,
                        Auto = stage.Auto,
                        Parameters = stage.Parameters
                            .Select(p => new ParamInfo
                            {
                                Name = p.Name,
                                Type = p.Type,
                                Tag = p.Tag,
                                Value = p.Value // временное значение из БД
                            })
                            .ToList()
                    })
                    .ToList()
            };

            Logger.Info("Копирование данных с RecipePLC в RecipeFromPLC завершено!");

            return result;
        }

        // обновление значений из PLC в RecipeFromPLC
        private void UpdateRecipeFromPLCValues(RecipePLC recipeFromPLC, Dictionary<string, string> plcValues)
        {
            if (recipeFromPLC == null || plcValues == null) return;

            foreach (var stage in recipeFromPLC.Stages)
            {
                if (string.IsNullOrWhiteSpace(stage.Tag)) continue;

                foreach (var param in stage.Parameters)
                {
                    if (string.IsNullOrWhiteSpace(param.Tag)) continue;

                    string key = $"{stage.Tag}_{stage.StageNumb}_{param.Tag}";

                    if (plcValues.TryGetValue(key, out var plcStr) && double.TryParse(plcStr, out var plcVal))
                    {
                        param.Value = plcVal;
                    }
                }
            }

            Logger.Info("Обновляем данные с PLC в recipeFromPLC");
        }

        // вычитываем данные из PLC
        private async Task<Dictionary<string, string>> ReadFromPLC(RecipePLC recipe)
        {
            //Logger.Info("--- Чтение данных из PLC - начало! ---");

            var result = new Dictionary<string, string>();

            if (recipe == null) return result;

            foreach (var stage in recipe.Stages)
            {
                if (string.IsNullOrWhiteSpace(stage.Tag)) continue;

                foreach (var param in stage.Parameters)
                {
                    if (string.IsNullOrWhiteSpace(param.Tag)) continue;

                    string key = $"{stage.Tag}_{stage.StageNumb}_{param.Tag}";

                    try
                    {
                        var value = await _ctApi.TagReadAsync(key);
                        result[key] = value;
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Warning($"Ошибка чтения тега '{key}' из PLC: {ex.Message}");
                    }
                }
            }

            //Logger.Info("--- Чтение данных из PLC закончилось успешно! ---");

            return result;
        }

        // запись отдельного параметра напрямую
        private void WriteSingleTagToPLC(StageGridItem stageItem, string paramName, double newValue)
        {
            if (!stageItem.Parameters.TryGetValue(paramName, out var param))
                return;

            string paramTag = param.Tag;
            if (string.IsNullOrWhiteSpace(paramTag))
                return;

            var stage = recipeToPLC.Stages.FirstOrDefault(s => s.StageNumb == stageItem.stageNumb);
            if (stage == null || string.IsNullOrWhiteSpace(stage.Tag))
                return;

            string fullTag = $"{stage.Tag}_{stage.StageNumb}_{paramTag}";

            try
            {
                _ctApi.TagWrite(fullTag, newValue.ToString());
            }
            catch (System.Exception) { }
        }

        #endregion

        #region Timer

        // инициализация таймера и запуск чтения данных, если isShowingPLCData = true
        private void InitializePlcReadTimer()
        {
            _plcReadCts = new CancellationTokenSource();
            _plcReadTimer.Interval = int.TryParse(ConfigHelper.CtApiRefreshData, out int interval) ? interval : 10_000; // 10 секунд по дефолту            
            _plcReadTimer.Tick += async (s, e) => await ReadPlcPeriodically(_plcReadCts.Token);
            _plcReadTimer.Start();
        }

        // чтение данных с PLC с контролем поточности
        private async Task ReadPlcPeriodically(CancellationToken token)
        {
            if (!isShowingPLCData || token.IsCancellationRequested)
                return;

            if (!await _plcReadLock.WaitAsync(0)) // избегаем параллельных вызовов
                return;

            try
            {
                plcStatusLabel.Text = $"PLC: Reading data from PlantScada - {DateTime.Now:HH:mm:ss}...";

                if (recipeToPLC == null)
                {
                    plcStatusLabel.Text = $"PLC: Recipe not found";
                    return;
                }

                var plcValues = await ReadFromPLC(recipeToPLC);
                UpdateRecipeFromPLCValues(recipeToPLC, plcValues);     // обновляем данные с PLC в recipeFromPLC
                if (!isEditingCell)                                    // не обновляем если вводим значеня
                    LoadRecipeFromPLC(recipeToPLC);                    // отображаем в таблице
                //this.Invoke(new Action(() => LoadRecipeFromPLC(recipeToPLC)));

                // Подсчёт ошибок
                int errorCount = plcValues.Values.Count(v => string.IsNullOrEmpty(v));
                if (errorCount > 0)
                {
                    plcStatusLabel.ForeColor = Color.Red;
                    plcStatusLabel.Text = $"PLC: Data read ({plcValues.Count}), Errors: {errorCount} - {DateTime.Now:HH:mm:ss}";
                }
                else
                {
                    plcStatusLabel.ForeColor = SystemColors.ControlText;
                    plcStatusLabel.Text = $"PLC: Data read ({plcValues.Count}) - {DateTime.Now:HH:mm:ss}";
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Ошибка чтения PLC: {ex.Message}");
            }
            finally
            {
                _plcReadLock.Release();
            }
        }

        #endregion

        #region Form

        // при потере фокуса - закрываем ctApi (Dispose тут не работает)
        private void RecipeUI_VisibleChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    _plcReadCts?.Cancel();  // отменяем таймер
            //    _plcReadCts?.Dispose();
            //    _plcReadTimer?.Stop();
            //    _plcReadTimer?.Dispose();
            //    _ctApi?.Close();

            //    Logger.Info("RecipeUI успешно освобожден и PLC соединение закрыто.");
            //}
            //catch (Exception ex)
            //{
            //    Logger.Warning($"Ошибка при закрытии соединения с PLC: {ex.Message}");
            //}
        }

        #endregion

        #region Not used

        // переобразовываем double в int
        private int ConvertDoubleToIntSmart(double value)
        {
            // Если дробная часть отсутствует, возвращаем как есть
            if (value % 1 == 0)
                return ClampToInt16((int)value);

            // Получаем количество знаков после запятой
            int decimalPlaces = BitConverter.GetBytes(decimal.GetBits((decimal)value)[3])[2];

            // Умножаем на 10^decimalPlaces и округляем
            double scaled = value * Math.Pow(10, decimalPlaces);
            return ClampToInt16((int)Math.Round(scaled));
        }

        // метод ограничивает 16 int
        private int ClampToInt16(int value)
        {
            return Math.Max(short.MinValue, Math.Min(short.MaxValue, value));
        }

        // отображение данных в логах
        private void LogRecipeToPLCTable(RecipePLC recipe)
        {
            if (recipe == null)
            {
                Logger.Warning("RecipeToPLC is null.");
                return;
            }

            // Получаем все уникальные параметры по имени и типу
            var allParamInfos = recipe.Stages
                .SelectMany(s => s.Parameters)
                .GroupBy(p => p.Name)
                .Select(g =>
                {
                    var first = g.First();
                    return new
                    {
                        Name = g.Key,
                        Type = first.Type
                    };
                })
                .OrderBy(p => p.Type)
                .ThenBy(p => p.Name)
                .ToList();

            // Заголовок
            var header = $"{"№",3} | {"Stage",-20} | {"Auto",4} | " + string.Join(" | ", allParamInfos.Select(p => $"{p.Name,10}"));
            var separator = new string('-', header.Length);

            Logger.Info($"Рецепт: ID={recipe.RecipeId}, Name=\"{recipe.RecipeName}\"");
            Logger.Info(separator);
            Logger.Info(header);
            Logger.Info(separator);

            // Строки по стадиям
            foreach (var stage in recipe.Stages)
            {
                string line = $"{stage.StageNumb,3} | {stage.Name,-20} | {(stage.Auto ? "Yes" : "No "),4} | " +
                              string.Join(" | ", allParamInfos.Select(p =>
                              {
                                  var param = stage.Parameters.FirstOrDefault(x => x.Name == p.Name);
                                  return param != null ? $"{param.Value,10:N2}" : $"{"",10}";
                              }));

                Logger.Info(line);
            }

            Logger.Info(separator);
        }

        private void LogRecipeToPLC(RecipePLC recipe)
        {
            if (recipe == null)
            {
                Logger.Warning("RecipeToPLC is null.");
                return;
            }

            Logger.Info($"Рецепт: ID={recipe.RecipeId}, Name=\"{recipe.RecipeName}\"");

            foreach (var stage in recipe.Stages)
            {
                var sortedParams = stage.Parameters
                    .OrderBy(p => p.Type)
                    .ThenBy(p => p.Name)
                    .ToList();

                var paramParts = sortedParams.Select(p => $"({p.Type}, {p.Name}, {p.Tag}, {p.Value:N2})");

                string line = $"{stage.StageNumb} - {stage.BaseStageNumb} - {stage.Name} - {stage.Tag} - {string.Join(" - ", paramParts)} - Auto: {(stage.Auto ? "Yes" : "No")}";
                Logger.Info(line);
            }
        }



        #endregion

        public class StageGridItem
        {
            public int RecipeStageId { get; set; }
            public int stageNumb { get; set; }
            public string name { get; set; }
            public bool auto { get; set; }
            public Dictionary<string, ParamInfo> Parameters { get; set; } = new Dictionary<string, ParamInfo>();
        }

        private void RecipeUI_Load(object sender, EventArgs e)
        {

        }
    }
}
