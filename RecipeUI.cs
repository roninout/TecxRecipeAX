using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;
using TecxRecipeAX;
using System.Data.Entity;
using System.Data.SqlClient;
using DevExpress.Xpo.Logger;
using System.Diagnostics;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using System.Globalization;
using DevExpress.XtraEditors.Controls;

namespace TechRecipeAX
{
    [ComVisible(true)]
    [Guid("A855AD2E-66E5-4961-8423-E28E37EA1A25")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(ITechRecipeAX))]
    [ComSourceInterfaces(typeof(ITechRecipeAXEvents))]

    public partial class RecipeUI : UserControl, ITechRecipeAX
    {
        private RecipeDbContext _context;
        private BindingList<RecipeStage> recipeStageBindingList;
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
                    await Task.Delay(10);
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
        // настройка таблицы
        private void ConfigureGridView()
        {
            gridViewStages.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            gridViewStages.OptionsBehavior.Editable = true;
            gridViewStages.OptionsView.ShowGroupPanel = false;
            //gridViewStages.RowCellStyle += GridView1_RowCellStyle;
            //gridViewStages.CellValueChanged += GridView1_CellValueChanged;
        }

        private void CreateGridColumns(IEnumerable<RecipeStage> stages)
        {
            gridViewStages.Columns.Clear();

            // Stage Number
            gridViewStages.Columns.Add(new GridColumn
            {
                FieldName = "stageNumb",
                Caption = "#",
                Visible = true,
                Width = 50
            });

            // Stage Name
            gridViewStages.Columns.Add(new GridColumn
            {
                FieldName = "name",
                Caption = "Stage",
                Visible = true,
                Width = 150,
                OptionsColumn = { AllowEdit = false }
            });

            // Unique parameters
            var paramNames = stages.SelectMany(s => s.RecipeParameters).Select(p => p.name).Distinct().OrderBy(n => n);

            foreach (var param in paramNames)
            {
                var column = new GridColumn
                {
                    Caption = param,
                    FieldName = param,
                    UnboundType = UnboundColumnType.Decimal,
                    Visible = true,
                    Width = 100
                };
                column.OptionsColumn.AllowEdit = true;
                column.ShowUnboundExpressionMenu = true;

                gridViewStages.Columns.Add(column);
            }

            // Auto checkbox
            gridViewStages.Columns.Add(new GridColumn
            {
                FieldName = "auto",
                Caption = "Auto",
                Visible = true,
                ColumnEdit = new RepositoryItemCheckEdit()
            });
        }

        private void gridViewStages_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (!e.Column.FieldName.Equals("stageNumb") && !e.Column.FieldName.Equals("name") && !e.Column.FieldName.Equals("auto"))
            {
                var stage = gridViewStages.GetRow(e.RowHandle) as RecipeStage;
                if (stage != null)
                {
                    bool hasParam = stage.RecipeParameters.Any(p => p.name == e.Column.FieldName);
                    if (!hasParam)
                    {
                        e.Appearance.BackColor = Color.LightGray;
                        e.Appearance.ForeColor = Color.DarkGray;
                        e.Column.OptionsColumn.AllowEdit = false;
                    }
                }
            }
        }

        private void gridViewStages_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;

            var stage = gridViewStages.GetRow(e.RowHandle) as RecipeStage;
            if (stage == null || e.Column == null || selectedRecipe == null) return;

            var paramName = e.Column.FieldName;
            var paramValueStr = e.Value?.ToString()?.Replace(',', '.');

            if (!double.TryParse(paramValueStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
            {
                MessageBox.Show("Некорректное значение параметра.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadRecipeStagesAndParameters(selectedRecipe.id);
                return;
            }

            var baseParam = _context.BaseParameters.FirstOrDefault(bp => bp.name == paramName);
            if (baseParam == null)
            {
                MessageBox.Show($"Базовый параметр '{paramName}' не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var parameter = _context.RecipeParameters.FirstOrDefault(p =>
                p.recipeStageId == stage.id && p.baseParameterId == baseParam.id);

            if (parameter != null)
            {
                parameter.value = value;
                _context.SaveChanges();
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

        #endregion

        #region Stuff
        private void LoadRecipeStagesAndParameters(int recipeId)
        {
            var recipe = _context.Recipes
                .Include(r => r.RecipeStages.Select(rs => rs.RecipeParameters))
                .FirstOrDefault(r => r.id == recipeId);

            if (recipe != null)
            {
                var sortedStages = recipe.RecipeStages.OrderBy(s => s.stageNumb).ToList();
                recipeStageBindingList = new BindingList<RecipeStage>(sortedStages);

                gridControlStages.DataSource = recipeStageBindingList;
                CreateGridColumns(recipeStageBindingList);
            }
        }

        private void RefreshData(int recipeId)
        {
            _context.Dispose();
            _context = new RecipeDbContext(ConfigHelper.GetConnectionString());
            LoadRecipeStagesAndParameters(recipeId);
        }
        #endregion
    }
}
