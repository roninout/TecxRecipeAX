using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TechRecipeAX;
using DevExpress.XtraGrid.Views.Grid;

namespace TecxRecipeAX
{
	public partial class FormEditParameters: XtraForm
	{
        private RecipeDbContext _context;

        public FormEditParameters()
		{
            InitializeComponent();

            _context = new RecipeDbContext(ConfigHelper.GetConnectionString());

            InitializeDataGrid();
        }

        #region Table

        // Загрузка параметров в таблицу
        private void InitializeDataGrid()
        {
            gridControlParameters.DataSource = _context.BaseParameters.ToList();
            GridView view = gridControlParameters.MainView as GridView;

            // Настройка колонок
            view.Columns["Id"].Visible = false;
            view.Columns["Name"].Caption = "Name";
            view.Columns["Type"].Caption = "Type";
            view.Columns["Description"].Caption = "Description";

            // Настройка ширины колонки Description
            view.Columns["Description"].Width = 252;

            // Включение редактирования
            view.OptionsBehavior.Editable = true;
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
        }

        #endregion

        #region Buttons

        // Кнопка "Добавить параметр"
        private void btnAddParameter_Click(object sender, EventArgs e)
        {
            var newParam = new BaseParameter
            {
                name = "New parameter",
                type = 0,
                description = "Description"
            };

            _context.BaseParameters.Add(newParam);
            _context.SaveChanges();

            // Обновляем источник данных
            gridControlParameters.DataSource = _context.BaseParameters.ToList();
        }

        // Кнопка "Удалить параметр"
        private void btnDeleteParameter_Click(object sender, EventArgs e)
        {
            GridView view = gridControlParameters.MainView as GridView;
            if (view.FocusedRowHandle >= 0)
            {
                int paramId = (int)view.GetFocusedRowCellValue("Id");
                var param = _context.BaseParameters.Find(paramId);
                if (param != null)
                {
                    _context.BaseParameters.Remove(param);
                    _context.SaveChanges();

                    // Обновляем источник данных
                    gridControlParameters.DataSource = _context.BaseParameters.ToList();
                }
            }
        }

        // Кнопка "Сохранить изменения"
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            // Для DevExpress обычно используется привязка данных, но в этом случае сохраняем напрямую
            _context.SaveChanges();
            XtraMessageBox.Show("Changes saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        // Обработчик события редактирования в GridView
        private void gridViewParameters_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            // Автоматическое сохранение при редактировании (опционально)
            _context.SaveChanges();
        }
    }
}