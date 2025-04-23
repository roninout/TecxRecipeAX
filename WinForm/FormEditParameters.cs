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

namespace TecxRecipeAX
{
    public partial class FormEditParameters : XtraForm
    {
        private HashSet<int> _usedParameterIds;
        private RecipeDbContext _context;
        private BindingList<BaseParameter> _bindingList;

        public FormEditParameters(RecipeDbContext context)
        {
            InitializeComponent();

            _context = context;
            _ = InitializeDataGrid();
        }

        #region Table

        // Загрузка параметров в таблицу
        private async Task InitializeDataGrid()
        {
            try
            {
                var parameters = await _context.BaseParameters.ToListAsync();

                _bindingList = new BindingList<BaseParameter>(parameters);
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

                // Устанавливаем информацию о количестве элементов в строке состояния
                //gridViewParameters.OptionsView.ShowFooter = true;
                //GridColumn colType = gridViewParameters.Columns["description"];
                //colType.Summary.Add(DevExpress.Data.SummaryItemType.Count, "description", "Total items: {0}");

                LoadUsedParameterIds();
                gridViewParameters.RefreshData();
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
                description = "Description"
            };

            // Добавляем новый параметр в BindingList
            _bindingList.Add(newParam);

            // Добавляем новый параметр в базу данных
            _context.BaseParameters.Add(newParam);
            _context.SaveChanges();
        }

        // Кнопка "Удалить параметр"
        private void btnDeleteParameter_Click(object sender, EventArgs e)
        {
            DeleteSelectedParameters();
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
    }
}