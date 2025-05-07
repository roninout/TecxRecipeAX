using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using TechRecipeAX;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Entity;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using System.Windows.Forms;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace TecxRecipeAX
{
    public partial class FormEditStages : XtraForm
    {
        private RecipeDbContext _context;
        private BindingList<StageGridItem> _bindingList;
        private List<BaseParameter> _baseParametersList;
        private const int ParameterCount = 16;

        public FormEditStages(RecipeDbContext context)
        {
            InitializeComponent();

            _context = context;
            this.Load += async (s, e) => await InitializeAsync(); // выносим всю инициализацию
        }

        #region Init

        private async Task InitializeAsync()
        {
            await LoadDataAsync();
            ConfigureGridView();
            ConfigureParameterCellStyles(); // стилизация ячеек
        }

        private async Task LoadDataAsync()
        {
            try
            {
                _baseParametersList = await _context.BaseParameters.ToListAsync();
                var stages = await _context.BaseRecipeStages.Include(s => s.BaseParameters).ToListAsync();

                _bindingList = new BindingList<StageGridItem>(stages.Select(s => ConvertToGridItem(s)).ToList());
                _bindingList.ListChanged += BindingList_ListChanged; // вызывается при добавлении новой стадии
                gridControlStages.DataSource = _bindingList;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
        }
        #endregion

        #region Table

        // настройка таблицы с колонками параметров
        private void ConfigureGridView()
        {
            gridViewStages.OptionsBehavior.Editable = true;
            gridViewStages.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            gridViewStages.OptionsView.ShowGroupPanel = false;

            gridViewStages.OptionsSelection.MultiSelect = true;
            gridViewStages.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;

            // Настройка колонок параметров
            for (int i = 1; i <= ParameterCount; i++)
            {
                var column = gridViewStages.Columns.AddVisible($"Parameter{i}Name", $"Parameter {i}");
                column.ColumnEdit = CreateParameterRepository();
                column.Width = 250;
            }

            //var colDescription = gridViewStages.Columns.AddVisible(nameof(StageGridItem.description), "Numb");
            //colDescription.OptionsColumn.AllowEdit = true;
            //colDescription.Width = 50;
            //colDescription.OptionsColumn.FixedWidth = false;

            // Сортировка по имени
            gridViewStages.SortInfo.Add(new GridColumnSortInfo(gridViewStages.Columns["name"], DevExpress.Data.ColumnSortOrder.Ascending));
            //gridViewStages.BestFitColumns();
        }

        // создаем ComboBox в ячейке параметров
        private RepositoryItemLookUpEdit CreateParameterRepository()
        {
            var parameterList = GetParameterListWithEmpty();
            var repo = new RepositoryItemLookUpEdit
            {
                DataSource = parameterList,
                DisplayMember = "Name",
                ValueMember = "Id",
                NullText = "Empty",
                AllowNullInput = DevExpress.Utils.DefaultBoolean.True
            };
            repo.Columns.Add(new LookUpColumnInfo("Name", "Parameter"));
            repo.ValidateOnEnterKey = true;
            return repo;
        }

        // событие нажатие кнопки Delete или Enter
        private void gridViewStages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedStages();
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Enter)
            {
                gridViewStages.CloseEditor();        // Завершаем редактирование ячейки
                gridViewStages.UpdateCurrentRow();   // Сохраняем изменения строки (вызовет RowUpdated)
                e.Handled = true;                        // Останавливаем дальнейшее распространение
            }
        }

        //событие обновления строки - просиходит сохранение
        private void gridViewStages_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row is StageGridItem item)
            {
                _ = SaveStageAsync(item);
            }
        }

        #endregion

        #region Buttons

        // Кнопка "Добавить стадию"
        private void btnAddStage_Click(object sender, EventArgs e)
        {
            // Базовое имя
            string baseName = "New Stage";
            string newName = baseName;
            int counter = 1;

            // Получаем существующие имена из BindingList (без запроса к БД)
            var existingNames = _bindingList.Select(x => x.name).ToList();

            // Генерируем уникальное имя
            while (existingNames.Contains(newName))
            {
                newName = $"{baseName} {counter++}";
            }

            // Создаем новую стадию
            var newStage = new StageGridItem
            {
                name = newName,
                description = "",
                ParameterIds = new int?[ParameterCount]
            };

            // Добавляем в BindingList (автоматически обновляет Grid)
            _bindingList.Add(newStage);

            // Фокусируемся на новой строке
            gridViewStages.FocusedRowHandle = gridViewStages.RowCount - 1;
        }

        // Кнопка "Удалить стадию"
        private void btnDeleteStage_Click(object sender, EventArgs e)
        {
            DeleteSelectedStages();
        }

        #endregion

        #region Stuff

        // Получает список параметров с добавлением "Empty" в начало.
        private List<ParameterItem> GetParameterListWithEmpty()
        {
            var parameterList = _baseParametersList
                    .Select(p => new ParameterItem(p.id, p.name)) // Создаем объект ParameterItem
                    .ToList();

            // Добавляем "Empty" в начало списка
            parameterList.Insert(0, new ParameterItem(null, "Empty")); // id типа int? (nullable)
            return parameterList;
        }

        private StageGridItem ConvertToGridItem(BaseRecipeStage stage)
        {
            var item = new StageGridItem
            {
                id = stage.id,
                name = stage.name,
                description = stage.description,
                ParameterIds = new int?[ParameterCount]
            };

            var baseParamsList = stage.BaseParameters.ToList();
            for (int i = 0; i < baseParamsList.Count && i < ParameterCount; i++)
            {
                item.ParameterIds[i] = baseParamsList[i].id;
            }

            return item;
        }

        // изменения в BindingList (то же самое что и в таблице) - добавление нового и изменения
        private async void BindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                switch (e.ListChangedType)
                {
                    case ListChangedType.ItemAdded:
                        await SaveNewStageAsync(_bindingList[e.NewIndex]);
                        break;

                    case ListChangedType.ItemChanged:
                        await UpdateStageAsync(_bindingList[e.NewIndex]);
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

        private async Task SaveNewStageAsync(StageGridItem item)
        {
            var stage = new BaseRecipeStage
            {
                name = item.name,
                description = item.description,
                BaseParameters = GetParametersFromItem(item)
            };
            _context.BaseRecipeStages.Add(stage);
            await _context.SaveChangesAsync();

            // Обновляем ID в GridItem после сохранения
            item.id = stage.id;
        }

        private async Task UpdateStageAsync(StageGridItem item)
        {
            var stage = await _context.BaseRecipeStages
                .Include(s => s.BaseParameters)
                .FirstOrDefaultAsync(s => s.id == item.id);

            if (stage != null)
            {
                stage.name = item.name;
                stage.description = item.description;
                stage.BaseParameters = GetParametersFromItem(item);
                await _context.SaveChangesAsync();
            }
        }

        private List<BaseParameter> GetParametersFromItem(StageGridItem item)
        {
            return item.ParameterIds
                .Where(id => id.HasValue)
                .Select(id => _baseParametersList.First(p => p.id == id.Value))
                .ToList();
        }

        // основной метод сохранения
        private async Task SaveStageAsync(StageGridItem item)
        {
            try
            {
                var stage = await _context.BaseRecipeStages
                    .Include(s => s.BaseParameters)
                    .FirstOrDefaultAsync(s => s.id == item.id);

                if (stage == null)
                {
                    stage = new BaseRecipeStage
                    {
                        name = item.name,
                        description = item.description,
                        BaseParameters = new List<BaseParameter>()
                    };
                    _context.BaseRecipeStages.Add(stage);
                }

                stage.description = item.description;
                stage.name = item.name;

                stage.BaseParameters.Clear();

                foreach (var paramId in item.ParameterIds.Where(id => id.HasValue))
                {
                    var param = _baseParametersList.FirstOrDefault(p => p.id == paramId.Value);
                    if (param != null)
                    {
                        stage.BaseParameters.Add(param);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

        // метод удаления стадии
        private async void DeleteSelectedStages()
        {
            // Получаем выделенные строки в GridView
            var selectedRowHandles = gridViewStages.GetSelectedRows();
            if (selectedRowHandles.Length == 0) return;

            // Получаем ID выбранных стадий
            var stageIdsToDelete = selectedRowHandles
                .Where(rowHandle => gridViewStages.IsValidRowHandle(rowHandle))
                .Select(rowHandle => (gridViewStages.GetRow(rowHandle) as StageGridItem)?.id)
                .Where(id => id != 0)
                .ToList();

            if (!stageIdsToDelete.Any()) return;

            // Подтверждение удаления
            if (XtraMessageBox.Show($"Удалить выбранные {stageIdsToDelete.Count} стадий?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                // Удаляем из базы данных
                var stagesToDelete = await _context.BaseRecipeStages
                    .Where(s => stageIdsToDelete.Contains(s.id))
                    .ToListAsync();

                _context.BaseRecipeStages.RemoveRange(stagesToDelete);

                // Удаляем из BindingList
                var itemsToRemove = _bindingList
                    .Where(item => stageIdsToDelete.Contains(item.id))
                    .ToList();

                foreach (var item in itemsToRemove)
                {
                    _bindingList.Remove(item);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #endregion

        #region Cell color

        // настройки стилей ячеек
        private void ConfigureParameterCellStyles()
        { 
            //var colorProvider = new ParameterColorProvider(_baseParametersList);

            for (int i = 1; i <= ParameterCount; i++)
            {
                string fieldName = $"Parameter{i}Name";
                int columnIndex = i - 1;

                //gridViewStages.Columns[fieldName].AppearanceCell.BackColor = Color.White;
                //gridViewStages.Columns[fieldName].AppearanceCell.BackColor2 = Color.White;

                gridViewStages.RowCellStyle += (sender, e) =>
                {
                    if (e.Column.FieldName == fieldName)
                    {
                        if (sender is GridView view && view.GetRow(e.RowHandle) is StageGridItem item)
                        {
                            if (columnIndex < item.ParameterIds.Length)
                            {
                                int? paramId = item.ParameterIds[columnIndex];
                                //var color = colorProvider.GetColor(paramId);

                                //e.Appearance.BackColor = color;
                                //e.Appearance.BackColor2 = color;

                                // Если параметр выбран (не null и не "Empty"), делаем текст жирным
                                e.Appearance.Font = paramId.HasValue ? new Font(e.Appearance.Font, FontStyle.Bold) : new Font(e.Appearance.Font, FontStyle.Regular);
                            }
                        }
                    }
                };
            }
        }

        #endregion

    }

    #region Classes
    public class ParameterItem
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public ParameterItem(int? id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class StageGridItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public int?[] ParameterIds { get; set; } = new int?[16];

        public int? Parameter1Name { get => ParameterIds[0]; set => ParameterIds[0] = value; }
        public int? Parameter2Name { get => ParameterIds[1]; set => ParameterIds[1] = value; }
        public int? Parameter3Name { get => ParameterIds[2]; set => ParameterIds[2] = value; }
        public int? Parameter4Name { get => ParameterIds[3]; set => ParameterIds[3] = value; }
        public int? Parameter5Name { get => ParameterIds[4]; set => ParameterIds[4] = value; }
        public int? Parameter6Name { get => ParameterIds[5]; set => ParameterIds[5] = value; }
        public int? Parameter7Name { get => ParameterIds[6]; set => ParameterIds[6] = value; }
        public int? Parameter8Name { get => ParameterIds[7]; set => ParameterIds[7] = value; }
        public int? Parameter9Name { get => ParameterIds[8]; set => ParameterIds[8] = value; }
        public int? Parameter10Name { get => ParameterIds[9]; set => ParameterIds[9] = value; }
        public int? Parameter11Name { get => ParameterIds[10]; set => ParameterIds[10] = value; }
        public int? Parameter12Name { get => ParameterIds[11]; set => ParameterIds[11] = value; }
        public int? Parameter13Name { get => ParameterIds[12]; set => ParameterIds[12] = value; }
        public int? Parameter14Name { get => ParameterIds[13]; set => ParameterIds[13] = value; }
        public int? Parameter15Name { get => ParameterIds[14]; set => ParameterIds[14] = value; }
        public int? Parameter16Name { get => ParameterIds[15]; set => ParameterIds[15] = value; }
    }

    public class ParameterColorProvider
    {
        private readonly Dictionary<int, int> _parameterTypeMap;
        private Dictionary<int, Color> _typeColorMap = new Dictionary<int, Color>();

        public ParameterColorProvider(IEnumerable<BaseParameter> baseParameters)
        {
            _parameterTypeMap = baseParameters
                .Where(p => p != null)
                .ToDictionary(p => p.id, p => p.type);
        }

        public Color GetColor(int? paramId)
        {
            if (!paramId.HasValue)
                return Color.LightGray;

            if (_parameterTypeMap.TryGetValue(paramId.Value, out int type))
            {
                if (!_typeColorMap.TryGetValue(type, out var color))
                {
                    // Привязываем тип к цвету по индексу (циклично)
                    color = _pastelColors[type % _pastelColors.Count];
                    _typeColorMap[type] = color;
                }

                return color;
            }

            return Color.White;
        }

        private readonly List<Color> _pastelColors = new List<Color>
        {
            Color.LightBlue,
            Color.LightGreen,
            Color.LightYellow,
            Color.LightPink,
            Color.LightSalmon,
            Color.LightCyan,
            Color.MistyRose,
            Color.Lavender,
            Color.Honeydew,
            Color.PaleTurquoise,
            Color.LightGoldenrodYellow,
            Color.AliceBlue,
            Color.Beige,
            Color.Moccasin
        };

    }
    #endregion
}