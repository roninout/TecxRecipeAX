namespace TecxRecipeAX
{
    partial class FormEditParameters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditParameters));
            this.gridControlParameters = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnAddParameter = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteParameter = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveChanges = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlParameters
            // 
            this.gridControlParameters.Location = new System.Drawing.Point(12, 48);
            this.gridControlParameters.MainView = this.gridView1;
            this.gridControlParameters.Name = "gridControlParameters";
            this.gridControlParameters.Size = new System.Drawing.Size(779, 428);
            this.gridControlParameters.TabIndex = 0;
            this.gridControlParameters.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlParameters;
            this.gridView1.Name = "gridView1";
            // 
            // btnAddParameter
            // 
            this.btnAddParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddParameter.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnEditParameters.ImageOptions.SvgImage")));
            this.btnAddParameter.Location = new System.Drawing.Point(231, 1);
            this.btnAddParameter.Name = "btnAddParameter";
            this.btnAddParameter.Size = new System.Drawing.Size(136, 41);
            this.btnAddParameter.TabIndex = 1;
            this.btnAddParameter.Text = "Add";
            this.btnAddParameter.Click += new System.EventHandler(this.btnAddParameter_Click);
            // 
            // btnDeleteParameter
            // 
            this.btnDeleteParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteParameter.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage")));
            this.btnDeleteParameter.Location = new System.Drawing.Point(373, 1);
            this.btnDeleteParameter.Name = "btnDeleteParameter";
            this.btnDeleteParameter.Size = new System.Drawing.Size(136, 41);
            this.btnDeleteParameter.TabIndex = 1;
            this.btnDeleteParameter.Text = "Delete";
            this.btnDeleteParameter.Click += new System.EventHandler(this.btnDeleteParameter_Click);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveChanges.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton2.ImageOptions.SvgImage")));
            this.btnSaveChanges.Location = new System.Drawing.Point(515, 1);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(136, 41);
            this.btnSaveChanges.TabIndex = 1;
            this.btnSaveChanges.Text = "Save";
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // FormEditParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 488);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.btnDeleteParameter);
            this.Controls.Add(this.btnAddParameter);
            this.Controls.Add(this.gridControlParameters);
            this.Name = "FormEditParameters";
            this.Text = "FormEditParameters";
            ((System.ComponentModel.ISupportInitialize)(this.gridControlParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlParameters;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnAddParameter;
        private DevExpress.XtraEditors.SimpleButton btnDeleteParameter;
        private DevExpress.XtraEditors.SimpleButton btnSaveChanges;
    }
}