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
using DevExpress.XtraEditors.Controls;

namespace TecxRecipeAX
{
    public partial class FormRecipeCopy : XtraForm
    {
        public string NewRecipeName => textEditNewName.Text;
        public Recipe SelectedRecipeToReplace => lookUpEditReplace.EditValue as Recipe;


        public FormRecipeCopy(List<Recipe> availableRecipes)
        {
            InitializeComponent();

            lookUpEditReplace.Properties.DataSource = availableRecipes;
            lookUpEditReplace.Properties.DisplayMember = "name";
            //lookUpEditReplace.Properties.ValueMember = "Self"; // добавим ниже
            lookUpEditReplace.Properties.ValueMember = "id"; // добавим ниже


            // Только одна колонка с именем
            lookUpEditReplace.Properties.Columns.Clear();
            lookUpEditReplace.Properties.Columns.Add(new LookUpColumnInfo("name", "Рецепт"));

            // Убираем заголовок, если не нужен
            //lookUpEditReplace.Properties.ShowHeader = false;

            //lookUpEditReplace.EditValue = availableRecipes[0].id;
        }

        #region Form Buttons

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewRecipeName))
            {
                XtraMessageBox.Show("Введите имя нового рецепта.");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

    }
}