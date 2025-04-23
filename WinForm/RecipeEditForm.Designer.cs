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
            this.btnRemoveStage = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddRecipe = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteRecipe = new DevExpress.XtraEditors.SimpleButton();
            this.btnUp = new DevExpress.XtraEditors.SimpleButton();
            this.btnDown = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveRecipeName = new DevExpress.XtraEditors.SimpleButton();
            this.textBoxRecipeName = new DevExpress.XtraEditors.TextEdit();
            this.comboBoxRecipes = new DevExpress.XtraEditors.LookUpEdit();
            this.btnSyncWithBaseStages = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxRecipeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxRecipes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlStages
            // 
            this.gridControlStages.Location = new System.Drawing.Point(12, 71);
            this.gridControlStages.MainView = this.gridViewStages;
            this.gridControlStages.Name = "gridControlStages";
            this.gridControlStages.Size = new System.Drawing.Size(1084, 484);
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
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 50;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Stage";
            this.gridColumn3.FieldName = "baseRecipeStageId";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 200;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Description";
            this.gridColumn4.FieldName = "description";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 300;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Auto";
            this.gridColumn5.FieldName = "auto";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 70;
            // 
            // btnAddStageToRecipe
            // 
            this.btnAddStageToRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddStageToRecipe.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAddStageToRecipe.ImageOptions.SvgImage")));
            this.btnAddStageToRecipe.Location = new System.Drawing.Point(890, 24);
            this.btnAddStageToRecipe.Name = "btnAddStageToRecipe";
            this.btnAddStageToRecipe.Size = new System.Drawing.Size(100, 41);
            this.btnAddStageToRecipe.TabIndex = 4;
            this.btnAddStageToRecipe.Text = "Add";
            this.btnAddStageToRecipe.Click += new System.EventHandler(this.btnAddStageToRecipe_Click);
            // 
            // btnRemoveStage
            // 
            this.btnRemoveStage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveStage.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnRemoveStage.ImageOptions.SvgImage")));
            this.btnRemoveStage.Location = new System.Drawing.Point(996, 24);
            this.btnRemoveStage.Name = "btnRemoveStage";
            this.btnRemoveStage.Size = new System.Drawing.Size(100, 41);
            this.btnRemoveStage.TabIndex = 5;
            this.btnRemoveStage.Text = "Delete";
            this.btnRemoveStage.Click += new System.EventHandler(this.btnRemoveStage_Click);
            // 
            // btnAddRecipe
            // 
            this.btnAddRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRecipe.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAddRecipe.ImageOptions.SvgImage")));
            this.btnAddRecipe.Location = new System.Drawing.Point(337, 24);
            this.btnAddRecipe.Name = "btnAddRecipe";
            this.btnAddRecipe.Size = new System.Drawing.Size(100, 41);
            this.btnAddRecipe.TabIndex = 4;
            this.btnAddRecipe.Text = "Add recipe";
            this.btnAddRecipe.Click += new System.EventHandler(this.btnAddRecipe_Click);
            // 
            // btnDeleteRecipe
            // 
            this.btnDeleteRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteRecipe.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDeleteRecipe.ImageOptions.SvgImage")));
            this.btnDeleteRecipe.Location = new System.Drawing.Point(443, 24);
            this.btnDeleteRecipe.Name = "btnDeleteRecipe";
            this.btnDeleteRecipe.Size = new System.Drawing.Size(100, 41);
            this.btnDeleteRecipe.TabIndex = 5;
            this.btnDeleteRecipe.Text = "Dell recipe";
            this.btnDeleteRecipe.Click += new System.EventHandler(this.btnDeleteRecipe_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(668, 4);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(35, 23);
            this.btnUp.TabIndex = 6;
            this.btnUp.Text = "Up";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(668, 33);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(35, 23);
            this.btnDown.TabIndex = 6;
            this.btnDown.Text = "Down";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnSaveRecipeName
            // 
            this.btnSaveRecipeName.Location = new System.Drawing.Point(587, 12);
            this.btnSaveRecipeName.Name = "btnSaveRecipeName";
            this.btnSaveRecipeName.Size = new System.Drawing.Size(75, 23);
            this.btnSaveRecipeName.TabIndex = 7;
            this.btnSaveRecipeName.Text = "save name";
            this.btnSaveRecipeName.Click += new System.EventHandler(this.btnSaveRecipeName_Click);
            // 
            // textBoxRecipeName
            // 
            this.textBoxRecipeName.Location = new System.Drawing.Point(337, -2);
            this.textBoxRecipeName.Name = "textBoxRecipeName";
            this.textBoxRecipeName.Size = new System.Drawing.Size(206, 20);
            this.textBoxRecipeName.TabIndex = 9;
            // 
            // comboBoxRecipes
            // 
            this.comboBoxRecipes.Location = new System.Drawing.Point(12, 45);
            this.comboBoxRecipes.Name = "comboBoxRecipes";
            this.comboBoxRecipes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxRecipes.Size = new System.Drawing.Size(219, 20);
            this.comboBoxRecipes.TabIndex = 10;
            this.comboBoxRecipes.EditValueChanged += new System.EventHandler(this.comboBoxRecipes_EditValueChanged);
            // 
            // btnSyncWithBaseStages
            // 
            this.btnSyncWithBaseStages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSyncWithBaseStages.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage")));
            this.btnSyncWithBaseStages.Location = new System.Drawing.Point(758, 24);
            this.btnSyncWithBaseStages.Name = "btnSyncWithBaseStages";
            this.btnSyncWithBaseStages.Size = new System.Drawing.Size(100, 41);
            this.btnSyncWithBaseStages.TabIndex = 4;
            this.btnSyncWithBaseStages.Text = "Refresh";
            this.btnSyncWithBaseStages.Click += new System.EventHandler(this.btnSyncWithBaseStages_Click);
            // 
            // RecipeEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 567);
            this.Controls.Add(this.comboBoxRecipes);
            this.Controls.Add(this.textBoxRecipeName);
            this.Controls.Add(this.btnSaveRecipeName);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDeleteRecipe);
            this.Controls.Add(this.btnAddRecipe);
            this.Controls.Add(this.btnRemoveStage);
            this.Controls.Add(this.btnSyncWithBaseStages);
            this.Controls.Add(this.btnAddStageToRecipe);
            this.Controls.Add(this.gridControlStages);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("RecipeEditForm.IconOptions.LargeImage")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecipeEditForm";
            this.Text = "Edit recipe";
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxRecipeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxRecipes.Properties)).EndInit();
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
        private DevExpress.XtraEditors.SimpleButton btnDown;
        private DevExpress.XtraEditors.SimpleButton btnSaveRecipeName;
        private DevExpress.XtraEditors.TextEdit textBoxRecipeName;
        private DevExpress.XtraEditors.LookUpEdit comboBoxRecipes;
        private DevExpress.XtraEditors.SimpleButton btnSyncWithBaseStages;
    }
}