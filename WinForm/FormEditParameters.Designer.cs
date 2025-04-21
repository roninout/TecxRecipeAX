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
            this.gridViewParameters = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAddParameter = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteParameter = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewParameters)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlParameters
            // 
            this.gridControlParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlParameters.Location = new System.Drawing.Point(12, 48);
            this.gridControlParameters.MainView = this.gridViewParameters;
            this.gridControlParameters.Name = "gridControlParameters";
            this.gridControlParameters.Size = new System.Drawing.Size(718, 428);
            this.gridControlParameters.TabIndex = 0;
            this.gridControlParameters.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewParameters});
            // 
            // gridViewParameters
            // 
            this.gridViewParameters.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn2,
            this.gridColumn4});
            this.gridViewParameters.GridControl = this.gridControlParameters;
            this.gridViewParameters.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "", null, "")});
            this.gridViewParameters.Name = "gridViewParameters";
            this.gridViewParameters.RowDeleting += new DevExpress.Data.RowDeletingEventHandler(this.gridViewParameters_RowDeleting);
            this.gridViewParameters.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridViewParameters_RowUpdated);
            this.gridViewParameters.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridViewParameters_KeyDown);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Id";
            this.gridColumn1.FieldName = "id";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Type";
            this.gridColumn3.FieldName = "type";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 56;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Name";
            this.gridColumn2.FieldName = "name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 223;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Description";
            this.gridColumn4.FieldName = "description";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 475;
            // 
            // btnAddParameter
            // 
            this.btnAddParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddParameter.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAddParameter.ImageOptions.SvgImage")));
            this.btnAddParameter.Location = new System.Drawing.Point(12, 1);
            this.btnAddParameter.Name = "btnAddParameter";
            this.btnAddParameter.Size = new System.Drawing.Size(100, 41);
            this.btnAddParameter.TabIndex = 1;
            this.btnAddParameter.Text = "Add";
            this.btnAddParameter.Click += new System.EventHandler(this.btnAddParameter_Click);
            // 
            // btnDeleteParameter
            // 
            this.btnDeleteParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteParameter.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDeleteParameter.ImageOptions.SvgImage")));
            this.btnDeleteParameter.Location = new System.Drawing.Point(630, 1);
            this.btnDeleteParameter.Name = "btnDeleteParameter";
            this.btnDeleteParameter.Size = new System.Drawing.Size(100, 41);
            this.btnDeleteParameter.TabIndex = 1;
            this.btnDeleteParameter.Text = "Delete";
            this.btnDeleteParameter.Click += new System.EventHandler(this.btnDeleteParameter_Click);
            // 
            // FormEditParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 488);
            this.Controls.Add(this.btnDeleteParameter);
            this.Controls.Add(this.btnAddParameter);
            this.Controls.Add(this.gridControlParameters);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("FormEditParameters.IconOptions.SvgImage")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditParameters";
            this.Text = "Edit parameters";
            ((System.ComponentModel.ISupportInitialize)(this.gridControlParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewParameters)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlParameters;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewParameters;
        private DevExpress.XtraEditors.SimpleButton btnAddParameter;
        private DevExpress.XtraEditors.SimpleButton btnDeleteParameter;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}