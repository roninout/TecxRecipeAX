
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TechRecipeAX;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Entity;
using System.ComponentModel;
using DevExpress.XtraGrid.Columns;
using System.Runtime.Remoting.Contexts;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Base;
using System.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace TecxRecipeAX
{
    public partial class FormEditParameters : XtraForm
    {
        private HashSet<int> _usedParameterIds;
        private RecipeDbContext _context;
        private BindingList<BaseParameter> _bindingList;
        private int? _currentBaseRecipeId;
        private int? _selectedBaseRecipeId;

        public FormEditParameters(RecipeDbContext context, int? currentBaseRecipeId)
        {
            InitializeComponent();

            _context = context;
            _currentBaseRecipeId = currentBaseRecipeId;

            // Асинхронные методы нужно вызывать через Load
            this.Load += FormEditParameters_Load;
        }

        #region Form
        // Асинхронные методы нужно вызывать через Load
        private async void FormEditParameters_Load(object sender, EventArgs e)
        {
            await InitializeDataGrid();
            await InitializeBaseRecipeIdLookUpEdit();
        }

        // при закрытии очищаем ресурсы
        private void FormEditParameters_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (gridControlParameters != null)
            {
                gridControlParameters.Visible = false; // предотвратит перерисовку
                gridControlParameters.Parent = null;   // удалит из иерархии
                gridControlParameters.DataSource = null;
                gridControlParameters.MainView = null; // убирает GridView
                gridControlParameters.Dispose();
            }
        }
        #endregion

        #region Table

        // Загрузка параметров в таблицу
        private async Task InitializeDataGrid()
        {
            try
            {
                var parameters = await _context.BaseParameters.ToListAsync();

                _bindingList = new BindingList<BaseParameter>(parameters);
                gridControlParameters.BeginUpdate();
                try
                {
                    gridControlParameters.DataSource = null;
                    gridControlParameters.DataSource = _bindingList;

                    gridViewParameters.OptionsBehavior.Editable = true;
                    gridViewParameters.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    gridViewParameters.OptionsView.ShowGroupPanel = false;

                    gridViewParameters.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
                    gridViewParameters.OptionsSelection.MultiSelect = true;
                    gridViewParameters.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
                    gridViewParameters.OptionsSelection.EnableAppearanceFocusedCell = false;

                    // Сортируем данные по колонке Type
                    gridViewParameters.SortInfo.Clear();
                    gridViewParameters.SortInfo.Add(new DevExpress.XtraGrid.Columns.GridColumnSortInfo(gridViewParameters.Columns["type"], DevExpress.Data.ColumnSortOrder.Ascending));

                    // создаем колонку kind с комбобоксами
                    var columnKind = gridViewParameters.Columns.AddVisible("kindInt", "Type");
                    columnKind.ColumnEdit = CreateKindRepository();
                    columnKind.Width = 200;

                    // создаем колонку format с комбобоксами
                    var columnFormat = gridViewParameters.Columns.AddVisible("formatInt", "Format");
                    columnFormat.ColumnEdit = CreateFormatRepository();
                    columnFormat.Width = 100;

                    // Устанавливаем информацию о количестве элементов в строке состояния
                    //gridViewParameters.OptionsView.ShowFooter = true;
                    //GridColumn colType = gridViewParameters.Columns["description"];
                    //colType.Summary.Add(DevExpress.Data.SummaryItemType.Count, "description", "Total items: {0}");

                    LoadUsedParameterIds();
                    gridViewParameters.RefreshData();
                }
                finally
                {
                    gridControlParameters.EndUpdate();
                }                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        // Обработчик события редактирования в GridView
        private void gridViewParameters_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row is BaseParameter param)
            {
                _context.Entry(param).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        // удаление строк
        private void gridViewParameters_RowDeleting(object sender, DevExpress.Data.RowDeletingEventArgs e)
        {
            DeleteSelectedParameters();
            e.Cancel = true; // отменяем стандартное поведение DevExpress, иначе может возникнуть конфликт
        }

        // нажатие кнопки Delete или Enter
        private void gridViewParameters_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gridViewParameters.FocusedRowHandle >= 0)
            {
                gridViewParameters.DeleteRow(gridViewParameters.FocusedRowHandle);
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Enter)
            {
                gridViewParameters.CloseEditor();        // Завершаем редактирование ячейки
                gridViewParameters.UpdateCurrentRow();   // Сохраняем изменения строки (вызовет RowUpdated)
                e.Handled = true;                        // Останавливаем дальнейшее распространение
            }
        }

        // метод выделяет жирным используемые параметры
        private void gridViewParameters_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (_usedParameterIds == null) return;

            var view = sender as GridView;
            var parameter = view?.GetRow(e.RowHandle) as BaseParameter;
            if (parameter == null) return;

            // Только если параметр используется и колонка — type или name
            if (_usedParameterIds.Contains(parameter.id) && (e.Column.FieldName == nameof(BaseParameter.type) || e.Column.FieldName == nameof(BaseParameter.name)))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void gridViewParameters_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            // column kind
            if (e.Column.FieldName == "kindInt")
            {
                var parameter = gridViewParameters.GetRow(e.RowHandle) as BaseParameter;
                if (parameter != null)
                {
                    // e.Value должен быть целым числом (Id из ComboBox)
                    if (e.Value != null && int.TryParse(e.Value.ToString(), out int newKind))
                    {
                        parameter.kind = (ParamKind)newKind;

                        // Обновляем через _bindingList
                        var paramInList = _bindingList.FirstOrDefault(p => p.id == parameter.id);
                        if (paramInList != null)
                        {
                            paramInList.kind = parameter.kind;
                        }

                        // Обновляем в БД
                        _context.Entry(parameter).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                }
            }

            // column format
            if (e.Column.FieldName == "formatInt")
            {
                var parameter = gridViewParameters.GetRow(e.RowHandle) as BaseParameter;
                if (parameter != null)
                {
                    // e.Value должен быть целым числом (Id из ComboBox)
                    if (e.Value != null && int.TryParse(e.Value.ToString(), out int newFormat))
                    {
                        parameter.format = (ParamFormat)newFormat;

                        // Обновляем через _bindingList
                        var paramInList = _bindingList.FirstOrDefault(p => p.id == parameter.id);
                        if (paramInList != null)
                        {
                            paramInList.format = parameter.format;
                        }

                        // Обновляем в БД
                        _context.Entry(parameter).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                }
            }
        }

        // создаем ComboBox в ячейке kind
        private RepositoryItemLookUpEdit CreateKindRepository()
        {
            // Список значений enum
            var paramKinds = Enum.GetValues(typeof(ParamKind))
                                  .Cast<ParamKind>()
                                  .Select(e => new { id = (int)e, kind = e.ToString() })
                                  .ToList();

            var repo = new RepositoryItemLookUpEdit
            {
                DataSource = paramKinds,
                DisplayMember = "kind",
                ValueMember = "id",
                TextEditStyle = TextEditStyles.DisableTextEditor
            };

            repo.Columns.Clear();
            repo.Columns.Add(new LookUpColumnInfo("kind", "Type"));
            repo.ShowHeader = false;
            repo.ShowFooter = false;
            //repo.ValidateOnEnterKey = true;

            return repo;
        }

        // создаем ComboBox в ячейке format
        private RepositoryItemLookUpEdit CreateFormatRepository()
        {
            // Список значений enum с "красивыми" подписями
            var paramFormats = new[]
            {
                new { id = (int)ParamFormat.Default, format = "" },
                new { id = (int)ParamFormat.N1, format = "#.#" },
                new { id = (int)ParamFormat.N2, format = "#.##" },
                new { id = (int)ParamFormat.N3, format = "#.###" },
                new { id = (int)ParamFormat.N4, format = "#.####" },
            }.ToList();

            var repo = new RepositoryItemLookUpEdit
            {
                DataSource = paramFormats,
                DisplayMember = "format",
                ValueMember = "id",
                TextEditStyle = TextEditStyles.DisableTextEditor
            };

            repo.Columns.Clear();
            repo.Columns.Add(new LookUpColumnInfo("format", "Format"));
            repo.ShowHeader = false;
            repo.ShowFooter = false;

            return repo;
        }

        #endregion

        #region Buttons

        // Кнопка "Добавить параметр"
        private void btnAddParameter_Click(object sender, EventArgs e)
        {
            // Находим первый свободный тип (минимальный отсутствующий)
            var newType = Enumerable.Range(1, int.MaxValue).Except(_bindingList.Select(p => p.type)).First();

            var newParam = new BaseParameter
            {
                name = "New parameter",
                type = newType,
                description = "",
                tag = "",
                kind = ParamKind.InputField,
                format = ParamFormat.Default,
                baseRecipeId = _selectedBaseRecipeId
            };

            // Добавляем новый параметр в BindingList
            _bindingList.Add(newParam);

            // Добавляем новый параметр в базу данных
            _context.BaseParameters.Add(newParam);
            _context.SaveChanges();

            // Вызов обработчика comboBoxEdit1_EditValueChanged вручную
            comboBoxEdit1_EditValueChanged(null, EventArgs.Empty);
        }

        // Кнопка "Удалить параметр"
        private void btnDeleteParameter_Click(object sender, EventArgs e)
        {
            DeleteSelectedParameters();

            // Вызов обработчика comboBoxEdit1_EditValueChanged вручную
            comboBoxEdit1_EditValueChanged(null, EventArgs.Empty);
        }

        #endregion

        #region Stuff
        // метод удаления
        private void DeleteSelectedParameters()
        {
            GridView view = gridControlParameters.MainView as GridView;
            if (view == null) return;

            var selectedRows = view.GetSelectedRows();
            if (selectedRows.Length == 0) return;

            string message = selectedRows.Length == 1
                ? $"Удалить параметр \"{(view.GetRow(selectedRows[0]) as BaseParameter)?.name}\"?"
                : $"Удалить {selectedRows.Length} параметров?";

            var confirm = XtraMessageBox.Show(
                message,
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (confirm != DialogResult.Yes)
                return;

            // Собираем параметры в список до удаления
            var parametersToDelete = selectedRows
                .Select(rowHandle => view.GetRow(rowHandle) as BaseParameter)
                .Where(p => p != null)
                .ToList();

            // Удаляем из BindingList и контекста
            foreach (var param in parametersToDelete)
            {
                _bindingList.Remove(param);
                _context.BaseParameters.Remove(param);
            }
            _context.SaveChanges();
        }

        // метод находит использованные параметры
        private void LoadUsedParameterIds()
        {
            _usedParameterIds = _context.RecipeParameters
                .Select(rp => rp.baseParameterId)
                .Distinct()
                .ToHashSet();
        }

        #endregion

        #region ComboBox
        // инициализация ComboBox
        private async Task InitializeBaseRecipeIdLookUpEdit()
        {
            var uniqueIdsWithNames = await _context.RecipePages
                .GroupBy(rp => rp.baseRecipeId)
                .Select(g => new
                {
                    baseRecipeId = g.Key,
                    recipeName = _context.Recipes
                        .Where(r => r.id == g.Key)
                        .Select(r => r.name)
                        .FirstOrDefault()
                })
                .OrderBy(x => x.baseRecipeId)
                .ToListAsync();

            var dataSource = new List<BaseRecipeIdItem> { new BaseRecipeIdItem { id = null, name = "All" } };

            dataSource.AddRange(uniqueIdsWithNames.Select(x => new BaseRecipeIdItem
            {
                id = x.baseRecipeId,
                name = x.recipeName ?? "(No name)"
            }));

            var lookUpEdit = (lookUpEditBaseRecipeId.Control as LookUpEdit);
            lookUpEdit.Properties.BeginUpdate();
            try
            {
                lookUpEdit.Properties.DataSource = null;
                lookUpEdit.Properties.DataSource = dataSource;
                lookUpEdit.Properties.DisplayMember = "name";
                lookUpEdit.Properties.ValueMember = "id";
                lookUpEdit.Properties.NullText = "All";

                lookUpEdit.Properties.PopulateColumns();
                lookUpEdit.Properties.Columns["id"].Caption = "ID";
                lookUpEdit.Properties.Columns["id"].Visible = true;
                lookUpEdit.Properties.Columns["id"].Width = 50;
                lookUpEdit.Properties.Columns["name"].Caption = "Recipe Name";
                lookUpEdit.Properties.Columns["name"].Visible = true;

                lookUpEdit.EditValue = _currentBaseRecipeId;
            }
            finally
            {
                lookUpEdit.Properties.EndUpdate();
            }
        }

        // события ComboBox
        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            // Получаем выбранное значение
            _selectedBaseRecipeId = (lookUpEditBaseRecipeId.Control as LookUpEdit).EditValue as int?;

            // Если "All" — отображаем все параметры
            gridControlParameters.BeginUpdate();
            try
            {
                gridControlParameters.DataSource = null;
                if (_selectedBaseRecipeId == null)
                {
                    gridControlParameters.DataSource = _bindingList;
                }
                else
                {
                    // Отображаем только параметры с baseRecipeId = selectedId
                    var filtered = _bindingList.Where(p => p.baseRecipeId == _selectedBaseRecipeId).ToList();
                    gridControlParameters.DataSource = new BindingList<BaseParameter>(filtered);
                }
                gridViewParameters.RefreshData();
            }
            finally
            {
                gridControlParameters.EndUpdate();
            }
        }
        #endregion
    }

    public class BaseRecipeIdItem
    {
        public int? id { get; set; }
        public string name { get; set; } = string.Empty;
    }
}