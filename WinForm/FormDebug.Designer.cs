namespace TecxRecipeAX
{
    partial class FormDebug
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
            this.textBoxLogs = new System.Windows.Forms.TextBox();
            this.textEditTagName = new DevExpress.XtraEditors.TextEdit();
            this.textEditTagValue = new DevExpress.XtraEditors.TextEdit();
            this.btnTagRead = new DevExpress.XtraEditors.SimpleButton();
            this.btnTagWrite = new DevExpress.XtraEditors.SimpleButton();
            this.btnFind = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEditFindResult = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTagName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTagValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditFindResult.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxLogs
            // 
            this.textBoxLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLogs.Location = new System.Drawing.Point(12, 109);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ReadOnly = true;
            this.textBoxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLogs.Size = new System.Drawing.Size(654, 275);
            this.textBoxLogs.TabIndex = 0;
            // 
            // textEditTagName
            // 
            this.textEditTagName.EditValue = "TagName";
            this.textEditTagName.Location = new System.Drawing.Point(13, 13);
            this.textEditTagName.Name = "textEditTagName";
            this.textEditTagName.Size = new System.Drawing.Size(168, 20);
            this.textEditTagName.TabIndex = 1;
            // 
            // textEditTagValue
            // 
            this.textEditTagValue.EditValue = "Value";
            this.textEditTagValue.Location = new System.Drawing.Point(498, 13);
            this.textEditTagValue.Name = "textEditTagValue";
            this.textEditTagValue.Size = new System.Drawing.Size(168, 20);
            this.textEditTagValue.TabIndex = 1;
            // 
            // btnTagRead
            // 
            this.btnTagRead.Location = new System.Drawing.Point(42, 61);
            this.btnTagRead.Name = "btnTagRead";
            this.btnTagRead.Size = new System.Drawing.Size(93, 42);
            this.btnTagRead.TabIndex = 2;
            this.btnTagRead.Text = "Read";
            this.btnTagRead.Click += new System.EventHandler(this.btnTagRead_Click);
            // 
            // btnTagWrite
            // 
            this.btnTagWrite.Location = new System.Drawing.Point(541, 61);
            this.btnTagWrite.Name = "btnTagWrite";
            this.btnTagWrite.Size = new System.Drawing.Size(93, 42);
            this.btnTagWrite.TabIndex = 2;
            this.btnTagWrite.Text = "Write";
            this.btnTagWrite.Click += new System.EventHandler(this.btnTagWrite_Click);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(299, 61);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(93, 42);
            this.btnFind.TabIndex = 2;
            this.btnFind.Text = "Find";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // lookUpEditFindResult
            // 
            this.lookUpEditFindResult.Location = new System.Drawing.Point(227, 13);
            this.lookUpEditFindResult.Name = "lookUpEditFindResult";
            this.lookUpEditFindResult.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditFindResult.Size = new System.Drawing.Size(223, 20);
            this.lookUpEditFindResult.TabIndex = 5;
            this.lookUpEditFindResult.EditValueChanged += new System.EventHandler(this.lookUpEditFindResult_EditValueChanged);
            // 
            // FormDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 396);
            this.Controls.Add(this.lookUpEditFindResult);
            this.Controls.Add(this.btnTagWrite);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnTagRead);
            this.Controls.Add(this.textEditTagValue);
            this.Controls.Add(this.textEditTagName);
            this.Controls.Add(this.textBoxLogs);
            this.Name = "FormDebug";
            this.Text = "FormDebug";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDebug_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.textEditTagName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTagValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditFindResult.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLogs;
        private DevExpress.XtraEditors.TextEdit textEditTagName;
        private DevExpress.XtraEditors.TextEdit textEditTagValue;
        private DevExpress.XtraEditors.SimpleButton btnTagRead;
        private DevExpress.XtraEditors.SimpleButton btnTagWrite;
        private DevExpress.XtraEditors.SimpleButton btnFind;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditFindResult;
    }
}