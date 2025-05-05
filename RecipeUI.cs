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
using static DevExpress.Utils.Svg.CommonSvgImages;
using DevExpress.XtraGrid.Columns;

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
        //private BindingList<RecipeStage> recipeStageBindingList;
        private BindingList<StageGridItem> stageGridItems;
        private Recipe selectedRecipe;

        public RecipeUI()
        {
            InitializeComponent();
            // Запускаем асинхронную инициализацию
            this.Load += async (sender, e) => await InitializeAsync();
        }

        #region Init data

        private async Task InitializeAsync()
        {
            Logger.Info(@" -------------------- Начало инициализации приложения ---------------- ");

            try
            {
                await InitializeDbContextAsync();
                await LoadDataAsync(); // Теперь можно вызывать асинхронно
                ConfigureGridView();
                LoadRecipeComboBox();
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                Logger.Error("Ошибка при загрузке данных");
                toolStripStatusLabel1.Text = $"Ошибка: {ex.Message}";
                toolStripStatusLabel1.ForeColor = Color.Red;
                progressBarControl1.Visible = false;
            }
        }

        #endregion

        #region Table
        // настройка таблицы - общее поведение
        private void ConfigureGridView()
        {
            gridViewStages.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            gridViewStages.OptionsView.ColumnAutoWidth = false;
            gridViewStages.OptionsView.ShowGroupPanel = true;

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
            colStageName.Width = 150;
            colStageName.OptionsColumn.FixedWidth = true;

            // Теперь динамические параметры
            if (items.Any())
            {
                var baseParameters = _context.BaseParameters.ToList(); // Загружаем BaseParameters один раз
                var allParameters = items.SelectMany(item => item.Parameters.Keys).Distinct().ToList();

                // Сортировка по BaseParameter.type
                var sortedParameters = allParameters
                .Select(name =>
                {
                    var paramInfo = baseParameters.FirstOrDefault(p => p.name == name);
                    return new
                    {
                        Name = name,
                        Type = paramInfo?.type ?? int.MaxValue,
                        Description = paramInfo?.description ?? ""
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
                    col.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                    col.Visible = true;
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "N2";
                    col.Width = 100;
                    col.OptionsColumn.FixedWidth = true;
                }
            }

            var colStageAuto = gridView.Columns.AddVisible(nameof(StageGridItem.auto), "Auto");
            colStageAuto.OptionsColumn.AllowEdit = true;
            colStageAuto.Width = 60;
            colStageAuto.OptionsColumn.FixedWidth = true;

            // отображение значений в ячейках по центру, кроме колонки "name"
            foreach (GridColumn column in gridViewStages.Columns)
                column.AppearanceCell.TextOptions.HAlignment = column.FieldName != "name" ? DevExpress.Utils.HorzAlignment.Center : DevExpress.Utils.HorzAlignment.Default;
            //gridView.BestFitColumns();
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

                if (e.IsGetData)
                {
                    if (stageItem.Parameters.TryGetValue(paramName, out double value))
                        e.Value = value;
                }
                else if (e.IsSetData)
                {
                    if (e.Value != null && double.TryParse(e.Value.ToString(), out double newValue))
                    {
                        stageItem.Parameters[paramName] = newValue;
                        SaveParameterValueToDatabase(stageItem.RecipeStageId, paramName, newValue);
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

        #endregion

        #region ComboBox
        private void LoadRecipeComboBox()
        {
            var recipes = _context.Recipes.ToList();

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
                lookUpEditRecipe.EditValue = recipes[0].id;
                selectedRecipe = recipes[0];
                LoadRecipeStagesAndParameters(recipes[0].id);
            }

            lookUpEditRecipe.EditValueChanged += (s, e) =>
            {
                if (lookUpEditRecipe.EditValue is int recipeId)
                {
                    selectedRecipe = recipes.FirstOrDefault(r => r.id == recipeId);
                    if (selectedRecipe != null)
                        LoadRecipeStagesAndParameters(selectedRecipe.id);
                }
            };
        }

        private void lookUpEditRecipe_EditValueChanged(object sender, EventArgs e)
        {
            if (lookUpEditRecipe.EditValue != null)
            {
                int selectedId = Convert.ToInt32(lookUpEditRecipe.EditValue);
                selectedRecipe = _context.Recipes.FirstOrDefault(r => r.id == selectedId);

                if (selectedRecipe != null)
                    LoadRecipeStagesAndParameters(selectedRecipe.id);
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
        // создание и редактирование параметров
        private void btnEditParameters_Click(object sender, EventArgs e)
        {
            using (var form = new FormEditParameters(_context))
            {
                form.ShowDialog();
            }
        }

        // создание и редактирование стадий
        private void btnEditStages_Click(object sender, EventArgs e)
        {
            using (var form = new FormEditStages(_context))
            {
                form.ShowDialog();
            }
        }

        // создание и редактирование рецептов
        private void btnEditRecipe_Click(object sender, EventArgs e)
        {
            using (var form = new RecipeEditForm(_context))
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
            using (var form = new FormDebug(_context))
            {
                form.ShowDialog();
            }
        }

        #endregion

        #region Stuff

        private void LoadRecipeStagesAndParameters(int recipeId)
        {
            var recipe = _context.Recipes.Include(r => r.RecipeStages.Select(rs => rs.RecipeParameters)).FirstOrDefault(r => r.id == recipeId);

            if (recipe != null)
            {
                selectedRecipe = recipe;
                var sortedStages = recipe.RecipeStages.OrderBy(s => s.stageNumb).ToList();

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
                        item.Parameters[param.name] = param.value;
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

        #endregion

    }

    public class StageGridItem
    {
        public int RecipeStageId { get; set; }
        public int stageNumb { get; set; }
        public string name { get; set; }
        public bool auto { get; set; }
        public Dictionary<string, double> Parameters { get; set; } = new Dictionary<string, double>();
    }
}
