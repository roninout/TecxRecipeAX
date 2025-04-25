namespace TecxRecipeAX
{
    partial class FormEditStages
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditStages));
            this.gridControlStages = new DevExpress.XtraGrid.GridControl();
            this.gridViewStages = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDeleteStage = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddStage = new DevExpress.XtraEditors.SimpleButton();
            this.FormEditStageslayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.btnDeleteStageitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnAddStageitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControlStagesitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormEditStageslayoutControl1ConvertedLayout)).BeginInit();
            this.FormEditStageslayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteStageitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddStageitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStagesitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlStages
            // 
            this.gridControlStages.Location = new System.Drawing.Point(12, 52);
            this.gridControlStages.MainView = this.gridViewStages;
            this.gridControlStages.Name = "gridControlStages";
            this.gridControlStages.Size = new System.Drawing.Size(1214, 537);
            this.gridControlStages.TabIndex = 0;
            this.gridControlStages.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewStages});
            // 
            // gridViewStages
            // 
            this.gridViewStages.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridViewStages.GridControl = this.gridControlStages;
            this.gridViewStages.Name = "gridViewStages";
            this.gridViewStages.OptionsView.ColumnAutoWidth = false;
            this.gridViewStages.OptionsView.ShowGroupPanel = false;
            this.gridViewStages.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridViewStages_RowUpdated);
            this.gridViewStages.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridViewStages_KeyDown);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "id";
            this.gridColumn1.FieldName = "id";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Name";
            this.gridColumn2.FieldName = "name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 160;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Description";
            this.gridColumn3.FieldName = "description";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 200;
            // 
            // btnDeleteStage
            // 
            this.btnDeleteStage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteStage.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDeleteStage.ImageOptions.SvgImage")));
            this.btnDeleteStage.Location = new System.Drawing.Point(1110, 12);
            this.btnDeleteStage.Name = "btnDeleteStage";
            this.btnDeleteStage.Size = new System.Drawing.Size(116, 36);
            this.btnDeleteStage.StyleController = this.FormEditStageslayoutControl1ConvertedLayout;
            this.btnDeleteStage.TabIndex = 2;
            this.btnDeleteStage.Text = "Remove";
            this.btnDeleteStage.Click += new System.EventHandler(this.btnDeleteStage_Click);
            // 
            // btnAddStage
            // 
            this.btnAddStage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddStage.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAddStage.ImageOptions.SvgImage")));
            this.btnAddStage.Location = new System.Drawing.Point(12, 12);
            this.btnAddStage.Name = "btnAddStage";
            this.btnAddStage.Size = new System.Drawing.Size(116, 36);
            this.btnAddStage.StyleController = this.FormEditStageslayoutControl1ConvertedLayout;
            this.btnAddStage.TabIndex = 3;
            this.btnAddStage.Text = "Add";
            this.btnAddStage.Click += new System.EventHandler(this.btnAddStage_Click);
            // 
            // FormEditStageslayoutControl1ConvertedLayout
            // 
            this.FormEditStageslayoutControl1ConvertedLayout.Controls.Add(this.btnDeleteStage);
            this.FormEditStageslayoutControl1ConvertedLayout.Controls.Add(this.btnAddStage);
            this.FormEditStageslayoutControl1ConvertedLayout.Controls.Add(this.gridControlStages);
            this.FormEditStageslayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormEditStageslayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.FormEditStageslayoutControl1ConvertedLayout.Name = "FormEditStageslayoutControl1ConvertedLayout";
            this.FormEditStageslayoutControl1ConvertedLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2830, 408, 650, 400);
            this.FormEditStageslayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.FormEditStageslayoutControl1ConvertedLayout.Size = new System.Drawing.Size(1238, 601);
            this.FormEditStageslayoutControl1ConvertedLayout.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.btnDeleteStageitem,
            this.btnAddStageitem,
            this.gridControlStagesitem,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1238, 601);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // btnDeleteStageitem
            // 
            this.btnDeleteStageitem.Control = this.btnDeleteStage;
            this.btnDeleteStageitem.Location = new System.Drawing.Point(1098, 0);
            this.btnDeleteStageitem.Name = "btnDeleteStageitem";
            this.btnDeleteStageitem.Size = new System.Drawing.Size(120, 40);
            this.btnDeleteStageitem.TextSize = new System.Drawing.Size(0, 0);
            this.btnDeleteStageitem.TextVisible = false;
            // 
            // btnAddStageitem
            // 
            this.btnAddStageitem.Control = this.btnAddStage;
            this.btnAddStageitem.Location = new System.Drawing.Point(0, 0);
            this.btnAddStageitem.Name = "btnAddStageitem";
            this.btnAddStageitem.Size = new System.Drawing.Size(120, 40);
            this.btnAddStageitem.TextSize = new System.Drawing.Size(0, 0);
            this.btnAddStageitem.TextVisible = false;
            // 
            // gridControlStagesitem
            // 
            this.gridControlStagesitem.Control = this.gridControlStages;
            this.gridControlStagesitem.Location = new System.Drawing.Point(0, 40);
            this.gridControlStagesitem.Name = "gridControlStagesitem";
            this.gridControlStagesitem.Size = new System.Drawing.Size(1218, 541);
            this.gridControlStagesitem.TextSize = new System.Drawing.Size(0, 0);
            this.gridControlStagesitem.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(120, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(978, 40);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FormEditStages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 601);
            this.Controls.Add(this.FormEditStageslayoutControl1ConvertedLayout);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("FormEditStages.IconOptions.LargeImage")));
            this.MinimizeBox = false;
            this.Name = "FormEditStages";
            this.Text = "Edit stages";
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormEditStageslayoutControl1ConvertedLayout)).EndInit();
            this.FormEditStageslayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteStageitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddStageitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStagesitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlStages;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewStages;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.SimpleButton btnDeleteStage;
        private DevExpress.XtraEditors.SimpleButton btnAddStage;
        private DevExpress.XtraLayout.LayoutControl FormEditStageslayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem btnDeleteStageitem;
        private DevExpress.XtraLayout.LayoutControlItem btnAddStageitem;
        private DevExpress.XtraLayout.LayoutControlItem gridControlStagesitem;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}