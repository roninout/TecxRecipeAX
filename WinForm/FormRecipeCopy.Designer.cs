namespace TecxRecipeAX
{
    partial class FormRecipeCopy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.textEditNewName = new DevExpress.XtraEditors.TextEdit();
            this.lookUpEditReplace = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNewName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditReplace.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 261);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 42);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(362, 261);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 42);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textEditNewName
            // 
            this.textEditNewName.Location = new System.Drawing.Point(99, 12);
            this.textEditNewName.Name = "textEditNewName";
            this.textEditNewName.Size = new System.Drawing.Size(276, 20);
            this.textEditNewName.TabIndex = 4;
            // 
            // lookUpEditReplace
            // 
            this.lookUpEditReplace.Location = new System.Drawing.Point(99, 110);
            this.lookUpEditReplace.Name = "lookUpEditReplace";
            this.lookUpEditReplace.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditReplace.Size = new System.Drawing.Size(276, 20);
            this.lookUpEditReplace.TabIndex = 5;
            // 
            // FormRecipeCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 315);
            this.Controls.Add(this.lookUpEditReplace);
            this.Controls.Add(this.textEditNewName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRecipeCopy";
            this.Text = "Зберегти рецепт як...";
            ((System.ComponentModel.ISupportInitialize)(this.textEditNewName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditReplace.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit textEditNewName;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditReplace;
    }
}