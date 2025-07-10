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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace TecxRecipeAX
{
	public partial class FormRecipeRename: DevExpress.XtraEditors.XtraForm
	{
        public string NewRecipeName { get; private set; } = string.Empty;

        public FormRecipeRename(string currentName)
		{
            InitializeComponent();
            textEditRecipeNewName.Text = currentName;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            NewRecipeName = textEditRecipeNewName.Text.Trim();

            if (string.IsNullOrWhiteSpace(NewRecipeName))
            {
                XtraMessageBox.Show("Ім'я рецепта не може бути порожнім.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None; // блокируем закрытие формы
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}