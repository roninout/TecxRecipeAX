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
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAddParameter = new DevExpress.XtraEditors.SimpleButton();
            this.FormEditParameterslayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.btnDeleteParameter = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.btnDeleteParameteritem = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnAddParameteritem = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControlParametersitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.lookUpEditBaseRecipeId = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormEditParameterslayoutControl1ConvertedLayout)).BeginInit();
            this.FormEditParameterslayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteParameteritem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddParameteritem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlParametersitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBaseRecipeId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlParameters
            // 
            this.gridControlParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlParameters.Location = new System.Drawing.Point(12, 52);
            this.gridControlParameters.MainView = this.gridViewParameters;
            this.gridControlParameters.Name = "gridControlParameters";
            this.gridControlParameters.Size = new System.Drawing.Size(784, 424);
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
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridViewParameters.GridControl = this.gridControlParameters;
            this.gridViewParameters.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "", null, "")});
            this.gridViewParameters.Name = "gridViewParameters";
            this.gridViewParameters.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewParameters_CustomDrawCell);
            this.gridViewParameters.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewParameters_CellValueChanged);
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
            this.gridColumn3.Caption = "Numb";
            this.gridColumn3.FieldName = "type";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 60;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Name";
            this.gridColumn2.FieldName = "name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 250;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Unit";
            this.gridColumn4.FieldName = "description";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 70;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Tag";
            this.gridColumn5.FieldName = "tag";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.FixedWidth = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 120;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "BaseRecipeId";
            this.gridColumn6.FieldName = "baseRecipeId";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.FixedWidth = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 90;
            // 
            // btnAddParameter
            // 
            this.btnAddParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddParameter.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAddParameter.ImageOptions.SvgImage")));
            this.btnAddParameter.Location = new System.Drawing.Point(12, 12);
            this.btnAddParameter.Name = "btnAddParameter";
            this.btnAddParameter.Size = new System.Drawing.Size(155, 36);
            this.btnAddParameter.StyleController = this.FormEditParameterslayoutControl1ConvertedLayout;
            this.btnAddParameter.TabIndex = 1;
            this.btnAddParameter.Text = "Add";
            this.btnAddParameter.Click += new System.EventHandler(this.btnAddParameter_Click);
            // 
            // FormEditParameterslayoutControl1ConvertedLayout
            // 
            this.FormEditParameterslayoutControl1ConvertedLayout.Controls.Add(this.btnDeleteParameter);
            this.FormEditParameterslayoutControl1ConvertedLayout.Controls.Add(this.btnAddParameter);
            this.FormEditParameterslayoutControl1ConvertedLayout.Controls.Add(this.gridControlParameters);
            this.FormEditParameterslayoutControl1ConvertedLayout.Controls.Add(this.comboBoxEdit1);
            this.FormEditParameterslayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormEditParameterslayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.FormEditParameterslayoutControl1ConvertedLayout.Name = "FormEditParameterslayoutControl1ConvertedLayout";
            this.FormEditParameterslayoutControl1ConvertedLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(3428, 352, 650, 556);
            this.FormEditParameterslayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.FormEditParameterslayoutControl1ConvertedLayout.Size = new System.Drawing.Size(808, 488);
            this.FormEditParameterslayoutControl1ConvertedLayout.TabIndex = 2;
            // 
            // btnDeleteParameter
            // 
            this.btnDeleteParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteParameter.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDeleteParameter.ImageOptions.SvgImage")));
            this.btnDeleteParameter.Location = new System.Drawing.Point(641, 12);
            this.btnDeleteParameter.Name = "btnDeleteParameter";
            this.btnDeleteParameter.Size = new System.Drawing.Size(155, 36);
            this.btnDeleteParameter.StyleController = this.FormEditParameterslayoutControl1ConvertedLayout;
            this.btnDeleteParameter.TabIndex = 1;
            this.btnDeleteParameter.Text = "Remove";
            this.btnDeleteParameter.Click += new System.EventHandler(this.btnDeleteParameter_Click);
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(298, 28);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.NullText = "";
            this.comboBoxEdit1.Properties.PopupSizeable = false;
            this.comboBoxEdit1.Size = new System.Drawing.Size(212, 20);
            this.comboBoxEdit1.StyleController = this.FormEditParameterslayoutControl1ConvertedLayout;
            this.comboBoxEdit1.TabIndex = 4;
            this.comboBoxEdit1.EditValueChanged += new System.EventHandler(this.comboBoxEdit1_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.btnDeleteParameteritem,
            this.btnAddParameteritem,
            this.gridControlParametersitem,
            this.lookUpEditBaseRecipeId,
            this.emptySpaceItem2,
            this.emptySpaceItem3});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(808, 488);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // btnDeleteParameteritem
            // 
            this.btnDeleteParameteritem.Control = this.btnDeleteParameter;
            this.btnDeleteParameteritem.Location = new System.Drawing.Point(629, 0);
            this.btnDeleteParameteritem.Name = "btnDeleteParameteritem";
            this.btnDeleteParameteritem.Size = new System.Drawing.Size(159, 40);
            this.btnDeleteParameteritem.TextSize = new System.Drawing.Size(0, 0);
            this.btnDeleteParameteritem.TextVisible = false;
            // 
            // btnAddParameteritem
            // 
            this.btnAddParameteritem.Control = this.btnAddParameter;
            this.btnAddParameteritem.Location = new System.Drawing.Point(0, 0);
            this.btnAddParameteritem.Name = "btnAddParameteritem";
            this.btnAddParameteritem.Size = new System.Drawing.Size(159, 40);
            this.btnAddParameteritem.TextSize = new System.Drawing.Size(0, 0);
            this.btnAddParameteritem.TextVisible = false;
            // 
            // gridControlParametersitem
            // 
            this.gridControlParametersitem.Control = this.gridControlParameters;
            this.gridControlParametersitem.Location = new System.Drawing.Point(0, 40);
            this.gridControlParametersitem.Name = "gridControlParametersitem";
            this.gridControlParametersitem.Size = new System.Drawing.Size(788, 428);
            this.gridControlParametersitem.TextSize = new System.Drawing.Size(0, 0);
            this.gridControlParametersitem.TextVisible = false;
            // 
            // lookUpEditBaseRecipeId
            // 
            this.lookUpEditBaseRecipeId.Control = this.comboBoxEdit1;
            this.lookUpEditBaseRecipeId.Location = new System.Drawing.Point(286, 0);
            this.lookUpEditBaseRecipeId.Name = "lookUpEditBaseRecipeId";
            this.lookUpEditBaseRecipeId.Size = new System.Drawing.Size(216, 40);
            this.lookUpEditBaseRecipeId.Text = "Filter:";
            this.lookUpEditBaseRecipeId.TextLocation = DevExpress.Utils.Locations.Top;
            this.lookUpEditBaseRecipeId.TextSize = new System.Drawing.Size(28, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(159, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(127, 40);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(502, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(127, 40);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FormEditParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 488);
            this.Controls.Add(this.FormEditParameterslayoutControl1ConvertedLayout);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("FormEditParameters.IconOptions.LargeImage")));
            this.MinimizeBox = false;
            this.Name = "FormEditParameters";
            this.Text = "Edit base parameters";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditParameters_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormEditParameterslayoutControl1ConvertedLayout)).EndInit();
            this.FormEditParameterslayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteParameteritem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddParameteritem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlParametersitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditBaseRecipeId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
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
        private DevExpress.XtraLayout.LayoutControl FormEditParameterslayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem btnDeleteParameteritem;
        private DevExpress.XtraLayout.LayoutControlItem btnAddParameteritem;
        private DevExpress.XtraLayout.LayoutControlItem gridControlParametersitem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraLayout.LayoutControlItem lookUpEditBaseRecipeId;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.LookUpEdit comboBoxEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}