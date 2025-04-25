namespace TecxRecipeAX
{
    partial class RecipeEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecipeEditForm));
            this.gridControlStages = new DevExpress.XtraGrid.GridControl();
            this.gridViewStages = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAddStageToRecipe = new DevExpress.XtraEditors.SimpleButton();
            this.RecipeEditFormlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.btnToggleEditName = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxRecipes = new DevExpress.XtraEditors.LookUpEdit();
            this.btnDown = new DevExpress.XtraEditors.SimpleButton();
            this.btnUp = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteRecipe = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddRecipe = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemoveStage = new DevExpress.XtraEditors.SimpleButton();
            this.btnSyncWithBaseStages = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.btnToggleEditNameitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.comboBoxRecipesitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnDownitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnUpitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnDeleteRecipeitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnAddRecipeitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnRemoveStageitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnSyncWithBaseStagesitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnAddStageToRecipeitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridControlStagesitem = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeEditFormlayoutControl1ConvertedLayout)).BeginInit();
            this.RecipeEditFormlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxRecipes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnToggleEditNameitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxRecipesitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDownitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteRecipeitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddRecipeitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRemoveStageitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSyncWithBaseStagesitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddStageToRecipeitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStagesitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlStages
            // 
            this.gridControlStages.Location = new System.Drawing.Point(12, 52);
            this.gridControlStages.MainView = this.gridViewStages;
            this.gridControlStages.Name = "gridControlStages";
            this.gridControlStages.Size = new System.Drawing.Size(624, 537);
            this.gridControlStages.TabIndex = 0;
            this.gridControlStages.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewStages});
            // 
            // gridViewStages
            // 
            this.gridViewStages.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridViewStages.GridControl = this.gridControlStages;
            this.gridViewStages.Name = "gridViewStages";
            this.gridViewStages.OptionsView.ShowGroupPanel = false;
            this.gridViewStages.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GridViewStages_FocusedRowChanged);
            this.gridViewStages.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GridViewStages_CellValueChanged);
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
            this.gridColumn2.Caption = "Number";
            this.gridColumn2.FieldName = "stageNumb";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 60;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Stage";
            this.gridColumn3.FieldName = "baseRecipeStageId";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 150;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Description";
            this.gridColumn4.FieldName = "description";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 200;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Auto";
            this.gridColumn5.FieldName = "auto";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.FixedWidth = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 60;
            // 
            // btnAddStageToRecipe
            // 
            this.btnAddStageToRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddStageToRecipe.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAddStageToRecipe.ImageOptions.SvgImage")));
            this.btnAddStageToRecipe.Location = new System.Drawing.Point(472, 12);
            this.btnAddStageToRecipe.Name = "btnAddStageToRecipe";
            this.btnAddStageToRecipe.Size = new System.Drawing.Size(38, 36);
            this.btnAddStageToRecipe.StyleController = this.RecipeEditFormlayoutControl1ConvertedLayout;
            this.btnAddStageToRecipe.TabIndex = 4;
            this.btnAddStageToRecipe.ToolTip = "Добавити стадію";
            this.btnAddStageToRecipe.Click += new System.EventHandler(this.btnAddStageToRecipe_Click);
            // 
            // RecipeEditFormlayoutControl1ConvertedLayout
            // 
            this.RecipeEditFormlayoutControl1ConvertedLayout.Controls.Add(this.btnToggleEditName);
            this.RecipeEditFormlayoutControl1ConvertedLayout.Controls.Add(this.comboBoxRecipes);
            this.RecipeEditFormlayoutControl1ConvertedLayout.Controls.Add(this.btnDown);
            this.RecipeEditFormlayoutControl1ConvertedLayout.Controls.Add(this.btnUp);
            this.RecipeEditFormlayoutControl1ConvertedLayout.Controls.Add(this.btnDeleteRecipe);
            this.RecipeEditFormlayoutControl1ConvertedLayout.Controls.Add(this.btnAddRecipe);
            this.RecipeEditFormlayoutControl1ConvertedLayout.Controls.Add(this.btnRemoveStage);
            this.RecipeEditFormlayoutControl1ConvertedLayout.Controls.Add(this.btnSyncWithBaseStages);
            this.RecipeEditFormlayoutControl1ConvertedLayout.Controls.Add(this.btnAddStageToRecipe);
            this.RecipeEditFormlayoutControl1ConvertedLayout.Controls.Add(this.gridControlStages);
            this.RecipeEditFormlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RecipeEditFormlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.RecipeEditFormlayoutControl1ConvertedLayout.Name = "RecipeEditFormlayoutControl1ConvertedLayout";
            this.RecipeEditFormlayoutControl1ConvertedLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(3256, 340, 650, 400);
            this.RecipeEditFormlayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.RecipeEditFormlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(648, 601);
            this.RecipeEditFormlayoutControl1ConvertedLayout.TabIndex = 12;
            // 
            // btnToggleEditName
            // 
            this.btnToggleEditName.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnToggleEditName.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnToggleEditName.ImageOptions.SvgImage")));
            this.btnToggleEditName.Location = new System.Drawing.Point(12, 12);
            this.btnToggleEditName.Name = "btnToggleEditName";
            this.btnToggleEditName.Size = new System.Drawing.Size(38, 36);
            this.btnToggleEditName.StyleController = this.RecipeEditFormlayoutControl1ConvertedLayout;
            this.btnToggleEditName.TabIndex = 11;
            this.btnToggleEditName.ToolTip = "Редагування назви рацепта";
            this.btnToggleEditName.Click += new System.EventHandler(this.btnToggleEditName_Click);
            // 
            // comboBoxRecipes
            // 
            this.comboBoxRecipes.Location = new System.Drawing.Point(54, 12);
            this.comboBoxRecipes.Name = "comboBoxRecipes";
            this.comboBoxRecipes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxRecipes.Size = new System.Drawing.Size(193, 20);
            this.comboBoxRecipes.StyleController = this.RecipeEditFormlayoutControl1ConvertedLayout;
            this.comboBoxRecipes.TabIndex = 10;
            this.comboBoxRecipes.EditValueChanged += new System.EventHandler(this.comboBoxRecipes_EditValueChanged);
            this.comboBoxRecipes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxRecipes_KeyDown);
            // 
            // btnDown
            // 
            this.btnDown.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnDown.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDown.ImageOptions.SvgImage")));
            this.btnDown.Location = new System.Drawing.Point(556, 12);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(38, 36);
            this.btnDown.StyleController = this.RecipeEditFormlayoutControl1ConvertedLayout;
            this.btnDown.TabIndex = 6;
            this.btnDown.ToolTip = "Перемістити стадію вниз";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnUp.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnUp.ImageOptions.SvgImage")));
            this.btnUp.Location = new System.Drawing.Point(514, 12);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(38, 36);
            this.btnUp.StyleController = this.RecipeEditFormlayoutControl1ConvertedLayout;
            this.btnUp.TabIndex = 6;
            this.btnUp.ToolTip = "Перемістити стадію вгору";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDeleteRecipe
            // 
            this.btnDeleteRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteRecipe.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDeleteRecipe.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDeleteRecipe.ImageOptions.SvgImage")));
            this.btnDeleteRecipe.Location = new System.Drawing.Point(293, 12);
            this.btnDeleteRecipe.Name = "btnDeleteRecipe";
            this.btnDeleteRecipe.Size = new System.Drawing.Size(38, 36);
            this.btnDeleteRecipe.StyleController = this.RecipeEditFormlayoutControl1ConvertedLayout;
            this.btnDeleteRecipe.TabIndex = 5;
            this.btnDeleteRecipe.ToolTip = "Видалити рецепт";
            this.btnDeleteRecipe.Click += new System.EventHandler(this.btnDeleteRecipe_Click);
            // 
            // btnAddRecipe
            // 
            this.btnAddRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRecipe.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAddRecipe.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAddRecipe.ImageOptions.SvgImage")));
            this.btnAddRecipe.Location = new System.Drawing.Point(251, 12);
            this.btnAddRecipe.Name = "btnAddRecipe";
            this.btnAddRecipe.Size = new System.Drawing.Size(38, 36);
            this.btnAddRecipe.StyleController = this.RecipeEditFormlayoutControl1ConvertedLayout;
            this.btnAddRecipe.TabIndex = 4;
            this.btnAddRecipe.ToolTip = "Новий рецепт";
            this.btnAddRecipe.Click += new System.EventHandler(this.btnAddRecipe_Click);
            // 
            // btnRemoveStage
            // 
            this.btnRemoveStage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveStage.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnRemoveStage.ImageOptions.SvgImage")));
            this.btnRemoveStage.Location = new System.Drawing.Point(598, 12);
            this.btnRemoveStage.Name = "btnRemoveStage";
            this.btnRemoveStage.Size = new System.Drawing.Size(38, 36);
            this.btnRemoveStage.StyleController = this.RecipeEditFormlayoutControl1ConvertedLayout;
            this.btnRemoveStage.TabIndex = 5;
            this.btnRemoveStage.ToolTip = "Видалити стадію з параметрами";
            this.btnRemoveStage.Click += new System.EventHandler(this.btnRemoveStage_Click);
            // 
            // btnSyncWithBaseStages
            // 
            this.btnSyncWithBaseStages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSyncWithBaseStages.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSyncWithBaseStages.ImageOptions.SvgImage")));
            this.btnSyncWithBaseStages.Location = new System.Drawing.Point(335, 12);
            this.btnSyncWithBaseStages.Name = "btnSyncWithBaseStages";
            this.btnSyncWithBaseStages.Size = new System.Drawing.Size(38, 36);
            this.btnSyncWithBaseStages.StyleController = this.RecipeEditFormlayoutControl1ConvertedLayout;
            this.btnSyncWithBaseStages.TabIndex = 4;
            this.btnSyncWithBaseStages.ToolTip = "Оновити параметри поточного рецепту";
            this.btnSyncWithBaseStages.Click += new System.EventHandler(this.btnSyncWithBaseStages_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.btnToggleEditNameitem,
            this.comboBoxRecipesitem,
            this.btnDownitem,
            this.btnUpitem,
            this.btnDeleteRecipeitem,
            this.btnAddRecipeitem,
            this.btnRemoveStageitem,
            this.btnSyncWithBaseStagesitem,
            this.btnAddStageToRecipeitem,
            this.gridControlStagesitem,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(648, 601);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // btnToggleEditNameitem
            // 
            this.btnToggleEditNameitem.Control = this.btnToggleEditName;
            this.btnToggleEditNameitem.Location = new System.Drawing.Point(0, 0);
            this.btnToggleEditNameitem.MaxSize = new System.Drawing.Size(42, 40);
            this.btnToggleEditNameitem.MinSize = new System.Drawing.Size(42, 40);
            this.btnToggleEditNameitem.Name = "btnToggleEditNameitem";
            this.btnToggleEditNameitem.Size = new System.Drawing.Size(42, 40);
            this.btnToggleEditNameitem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.btnToggleEditNameitem.TextSize = new System.Drawing.Size(0, 0);
            this.btnToggleEditNameitem.TextVisible = false;
            // 
            // comboBoxRecipesitem
            // 
            this.comboBoxRecipesitem.Control = this.comboBoxRecipes;
            this.comboBoxRecipesitem.Location = new System.Drawing.Point(42, 0);
            this.comboBoxRecipesitem.Name = "comboBoxRecipesitem";
            this.comboBoxRecipesitem.Size = new System.Drawing.Size(197, 40);
            this.comboBoxRecipesitem.TextSize = new System.Drawing.Size(0, 0);
            this.comboBoxRecipesitem.TextVisible = false;
            // 
            // btnDownitem
            // 
            this.btnDownitem.Control = this.btnDown;
            this.btnDownitem.Location = new System.Drawing.Point(544, 0);
            this.btnDownitem.MaxSize = new System.Drawing.Size(42, 40);
            this.btnDownitem.MinSize = new System.Drawing.Size(42, 40);
            this.btnDownitem.Name = "btnDownitem";
            this.btnDownitem.Size = new System.Drawing.Size(42, 40);
            this.btnDownitem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.btnDownitem.TextSize = new System.Drawing.Size(0, 0);
            this.btnDownitem.TextVisible = false;
            // 
            // btnUpitem
            // 
            this.btnUpitem.Control = this.btnUp;
            this.btnUpitem.Location = new System.Drawing.Point(502, 0);
            this.btnUpitem.MaxSize = new System.Drawing.Size(42, 40);
            this.btnUpitem.MinSize = new System.Drawing.Size(42, 40);
            this.btnUpitem.Name = "btnUpitem";
            this.btnUpitem.Size = new System.Drawing.Size(42, 40);
            this.btnUpitem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.btnUpitem.TextSize = new System.Drawing.Size(0, 0);
            this.btnUpitem.TextVisible = false;
            // 
            // btnDeleteRecipeitem
            // 
            this.btnDeleteRecipeitem.Control = this.btnDeleteRecipe;
            this.btnDeleteRecipeitem.Location = new System.Drawing.Point(281, 0);
            this.btnDeleteRecipeitem.MaxSize = new System.Drawing.Size(42, 40);
            this.btnDeleteRecipeitem.MinSize = new System.Drawing.Size(42, 40);
            this.btnDeleteRecipeitem.Name = "btnDeleteRecipeitem";
            this.btnDeleteRecipeitem.Size = new System.Drawing.Size(42, 40);
            this.btnDeleteRecipeitem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.btnDeleteRecipeitem.TextSize = new System.Drawing.Size(0, 0);
            this.btnDeleteRecipeitem.TextVisible = false;
            // 
            // btnAddRecipeitem
            // 
            this.btnAddRecipeitem.Control = this.btnAddRecipe;
            this.btnAddRecipeitem.Location = new System.Drawing.Point(239, 0);
            this.btnAddRecipeitem.MaxSize = new System.Drawing.Size(42, 40);
            this.btnAddRecipeitem.MinSize = new System.Drawing.Size(42, 40);
            this.btnAddRecipeitem.Name = "btnAddRecipeitem";
            this.btnAddRecipeitem.Size = new System.Drawing.Size(42, 40);
            this.btnAddRecipeitem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.btnAddRecipeitem.TextSize = new System.Drawing.Size(0, 0);
            this.btnAddRecipeitem.TextVisible = false;
            // 
            // btnRemoveStageitem
            // 
            this.btnRemoveStageitem.Control = this.btnRemoveStage;
            this.btnRemoveStageitem.Location = new System.Drawing.Point(586, 0);
            this.btnRemoveStageitem.MaxSize = new System.Drawing.Size(42, 40);
            this.btnRemoveStageitem.MinSize = new System.Drawing.Size(42, 40);
            this.btnRemoveStageitem.Name = "btnRemoveStageitem";
            this.btnRemoveStageitem.Size = new System.Drawing.Size(42, 40);
            this.btnRemoveStageitem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.btnRemoveStageitem.TextSize = new System.Drawing.Size(0, 0);
            this.btnRemoveStageitem.TextVisible = false;
            // 
            // btnSyncWithBaseStagesitem
            // 
            this.btnSyncWithBaseStagesitem.Control = this.btnSyncWithBaseStages;
            this.btnSyncWithBaseStagesitem.Location = new System.Drawing.Point(323, 0);
            this.btnSyncWithBaseStagesitem.MaxSize = new System.Drawing.Size(42, 40);
            this.btnSyncWithBaseStagesitem.MinSize = new System.Drawing.Size(42, 40);
            this.btnSyncWithBaseStagesitem.Name = "btnSyncWithBaseStagesitem";
            this.btnSyncWithBaseStagesitem.Size = new System.Drawing.Size(42, 40);
            this.btnSyncWithBaseStagesitem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.btnSyncWithBaseStagesitem.TextSize = new System.Drawing.Size(0, 0);
            this.btnSyncWithBaseStagesitem.TextVisible = false;
            // 
            // btnAddStageToRecipeitem
            // 
            this.btnAddStageToRecipeitem.Control = this.btnAddStageToRecipe;
            this.btnAddStageToRecipeitem.Location = new System.Drawing.Point(460, 0);
            this.btnAddStageToRecipeitem.MaxSize = new System.Drawing.Size(42, 40);
            this.btnAddStageToRecipeitem.MinSize = new System.Drawing.Size(42, 40);
            this.btnAddStageToRecipeitem.Name = "btnAddStageToRecipeitem";
            this.btnAddStageToRecipeitem.Size = new System.Drawing.Size(42, 40);
            this.btnAddStageToRecipeitem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.btnAddStageToRecipeitem.TextSize = new System.Drawing.Size(0, 0);
            this.btnAddStageToRecipeitem.TextVisible = false;
            // 
            // gridControlStagesitem
            // 
            this.gridControlStagesitem.Control = this.gridControlStages;
            this.gridControlStagesitem.Location = new System.Drawing.Point(0, 40);
            this.gridControlStagesitem.Name = "gridControlStagesitem";
            this.gridControlStagesitem.Size = new System.Drawing.Size(628, 541);
            this.gridControlStagesitem.TextSize = new System.Drawing.Size(0, 0);
            this.gridControlStagesitem.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(365, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(95, 40);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // RecipeEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 601);
            this.Controls.Add(this.RecipeEditFormlayoutControl1ConvertedLayout);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("RecipeEditForm.IconOptions.LargeImage")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecipeEditForm";
            this.Text = "Edit recipe";
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeEditFormlayoutControl1ConvertedLayout)).EndInit();
            this.RecipeEditFormlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxRecipes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnToggleEditNameitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxRecipesitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDownitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteRecipeitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddRecipeitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRemoveStageitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSyncWithBaseStagesitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddStageToRecipeitem)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.SimpleButton btnAddStageToRecipe;
        private DevExpress.XtraEditors.SimpleButton btnRemoveStage;
        private DevExpress.XtraEditors.SimpleButton btnAddRecipe;
        private DevExpress.XtraEditors.SimpleButton btnDeleteRecipe;
        private DevExpress.XtraEditors.SimpleButton btnUp;
        private DevExpress.XtraEditors.LookUpEdit comboBoxRecipes;
        private DevExpress.XtraEditors.SimpleButton btnSyncWithBaseStages;
        private DevExpress.XtraEditors.SimpleButton btnToggleEditName;
        private DevExpress.XtraEditors.SimpleButton btnDown;
        private DevExpress.XtraLayout.LayoutControl RecipeEditFormlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem btnToggleEditNameitem;
        private DevExpress.XtraLayout.LayoutControlItem comboBoxRecipesitem;
        private DevExpress.XtraLayout.LayoutControlItem btnDownitem;
        private DevExpress.XtraLayout.LayoutControlItem btnUpitem;
        private DevExpress.XtraLayout.LayoutControlItem btnDeleteRecipeitem;
        private DevExpress.XtraLayout.LayoutControlItem btnAddRecipeitem;
        private DevExpress.XtraLayout.LayoutControlItem btnRemoveStageitem;
        private DevExpress.XtraLayout.LayoutControlItem btnSyncWithBaseStagesitem;
        private DevExpress.XtraLayout.LayoutControlItem btnAddStageToRecipeitem;
        private DevExpress.XtraLayout.LayoutControlItem gridControlStagesitem;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}