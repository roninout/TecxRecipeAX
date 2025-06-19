namespace TechRecipeAX
{
    partial class RecipeUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecipeUI));
            this.btnEditParameters = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlStages = new DevExpress.XtraGrid.GridControl();
            this.gridViewStages = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.plcStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnEditStages = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditRecipe = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefreshRecipe = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEditRecipe = new DevExpress.XtraEditors.LookUpEdit();
            this.btnDebug = new DevExpress.XtraEditors.SimpleButton();
            this.btnDataToPlc = new DevExpress.XtraEditors.SimpleButton();
            this.checkBoxShowPLC = new DevExpress.XtraEditors.CheckButton();
            this.btnSaveAs = new DevExpress.XtraEditors.SimpleButton();
            this.textEditRecipeName = new DevExpress.XtraEditors.TextEdit();
            this.checkButtonBaseStage = new DevExpress.XtraEditors.CheckButton();
            this.btnSaveFromPLC = new DevExpress.XtraEditors.SimpleButton();
            this.btnAssignRecipeToPages = new DevExpress.XtraEditors.SimpleButton();
            this.lbSelectRecipe = new DevExpress.XtraEditors.LabelControl();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRecipe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditRecipeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEditParameters
            // 
            this.btnEditParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditParameters.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEditParameters.ImageOptions.Image")));
            this.btnEditParameters.Location = new System.Drawing.Point(1343, 8);
            this.btnEditParameters.Name = "btnEditParameters";
            this.btnEditParameters.Size = new System.Drawing.Size(136, 40);
            this.btnEditParameters.TabIndex = 0;
            this.btnEditParameters.Text = "Edit parameters";
            this.btnEditParameters.Click += new System.EventHandler(this.btnEditParameters_Click);
            // 
            // gridControlStages
            // 
            this.gridControlStages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlStages.Location = new System.Drawing.Point(16, 54);
            this.gridControlStages.MainView = this.gridViewStages;
            this.gridControlStages.Name = "gridControlStages";
            this.gridControlStages.Size = new System.Drawing.Size(1889, 860);
            this.gridControlStages.TabIndex = 1;
            this.gridControlStages.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewStages});
            // 
            // gridViewStages
            // 
            this.gridViewStages.GridControl = this.gridControlStages;
            this.gridViewStages.Name = "gridViewStages";
            this.gridViewStages.OptionsView.ShowGroupPanel = false;
            this.gridViewStages.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewStages_RowCellStyle);
            this.gridViewStages.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewStages_RowStyle);
            this.gridViewStages.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridViewStages_ShowingEditor);
            this.gridViewStages.HiddenEditor += new System.EventHandler(this.gridViewStages_HiddenEditor);
            this.gridViewStages.ShownEditor += new System.EventHandler(this.gridViewStages_ShownEditor);
            this.gridViewStages.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewStages_CellValueChanged);
            this.gridViewStages.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.GridViewStages_CustomUnboundColumnData);
            this.gridViewStages.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.gridViewStages_ValidatingEditor);
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl1.Location = new System.Drawing.Point(16, 917);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Properties.ShowTitle = true;
            this.progressBarControl1.Properties.Step = 1;
            this.progressBarControl1.Size = new System.Drawing.Size(1889, 18);
            this.progressBarControl1.TabIndex = 2;
            this.progressBarControl1.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.plcStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 936);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1921, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "\"Подключение к базе данных...\"";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(500, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // plcStatusLabel
            // 
            this.plcStatusLabel.Name = "plcStatusLabel";
            this.plcStatusLabel.Size = new System.Drawing.Size(56, 17);
            this.plcStatusLabel.Text = "\"PLC: —\"";
            this.plcStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnEditStages
            // 
            this.btnEditStages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditStages.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEditStages.ImageOptions.Image")));
            this.btnEditStages.Location = new System.Drawing.Point(1485, 8);
            this.btnEditStages.Name = "btnEditStages";
            this.btnEditStages.Size = new System.Drawing.Size(136, 40);
            this.btnEditStages.TabIndex = 0;
            this.btnEditStages.Text = "Edit stages";
            this.btnEditStages.Click += new System.EventHandler(this.btnEditStages_Click);
            // 
            // btnEditRecipe
            // 
            this.btnEditRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditRecipe.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEditRecipe.ImageOptions.Image")));
            this.btnEditRecipe.Location = new System.Drawing.Point(1627, 8);
            this.btnEditRecipe.Name = "btnEditRecipe";
            this.btnEditRecipe.Size = new System.Drawing.Size(136, 40);
            this.btnEditRecipe.TabIndex = 0;
            this.btnEditRecipe.Text = "Edit recipe";
            this.btnEditRecipe.Click += new System.EventHandler(this.btnEditRecipe_Click);
            // 
            // btnRefreshRecipe
            // 
            this.btnRefreshRecipe.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnRefreshRecipe.ImageOptions.SvgImage")));
            this.btnRefreshRecipe.Location = new System.Drawing.Point(1251, 8);
            this.btnRefreshRecipe.Name = "btnRefreshRecipe";
            this.btnRefreshRecipe.Size = new System.Drawing.Size(40, 40);
            this.btnRefreshRecipe.TabIndex = 0;
            this.btnRefreshRecipe.ToolTip = "Перечитати поточний рецепт з БД";
            this.btnRefreshRecipe.Click += new System.EventHandler(this.btnRefreshRecipe_Click);
            // 
            // lookUpEditRecipe
            // 
            this.lookUpEditRecipe.Location = new System.Drawing.Point(16, 28);
            this.lookUpEditRecipe.Name = "lookUpEditRecipe";
            this.lookUpEditRecipe.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditRecipe.Size = new System.Drawing.Size(278, 20);
            this.lookUpEditRecipe.TabIndex = 4;
            this.lookUpEditRecipe.EditValueChanged += new System.EventHandler(this.lookUpEditRecipe_EditValueChanged);
            // 
            // btnDebug
            // 
            this.btnDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDebug.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDebug.ImageOptions.SvgImage")));
            this.btnDebug.Location = new System.Drawing.Point(1251, 8);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(40, 40);
            this.btnDebug.TabIndex = 0;
            this.btnDebug.Text = "Debug";
            this.btnDebug.Visible = false;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // btnDataToPlc
            // 
            this.btnDataToPlc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDataToPlc.ImageOptions.SvgImage = global::TecxRecipeAX.Properties.Resources.savedialog;
            this.btnDataToPlc.Location = new System.Drawing.Point(1769, 8);
            this.btnDataToPlc.Name = "btnDataToPlc";
            this.btnDataToPlc.Size = new System.Drawing.Size(136, 40);
            this.btnDataToPlc.TabIndex = 0;
            this.btnDataToPlc.Text = "Save to PLC";
            this.btnDataToPlc.ToolTip = "Зберегти поточний рецепт";
            this.btnDataToPlc.Visible = false;
            this.btnDataToPlc.Click += new System.EventHandler(this.btnDataToPlc_Click);
            // 
            // checkBoxShowPLC
            // 
            this.checkBoxShowPLC.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.checkBoxShowPLC.Location = new System.Drawing.Point(1205, 8);
            this.checkBoxShowPLC.Name = "checkBoxShowPLC";
            this.checkBoxShowPLC.Size = new System.Drawing.Size(40, 40);
            this.checkBoxShowPLC.TabIndex = 5;
            this.checkBoxShowPLC.CheckedChanged += new System.EventHandler(this.checkBoxShowPLC_CheckedChanged);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAs.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSaveAs.ImageOptions.SvgImage")));
            this.btnSaveAs.Location = new System.Drawing.Point(1251, 8);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(40, 40);
            this.btnSaveAs.TabIndex = 0;
            this.btnSaveAs.ToolTip = "Зберегти поточний рецепт";
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // textEditRecipeName
            // 
            this.textEditRecipeName.EditValue = "Завантаження рецепту...";
            this.textEditRecipeName.Enabled = false;
            this.textEditRecipeName.Location = new System.Drawing.Point(300, 8);
            this.textEditRecipeName.Name = "textEditRecipeName";
            this.textEditRecipeName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.textEditRecipeName.Properties.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.textEditRecipeName.Properties.Appearance.Options.UseFont = true;
            this.textEditRecipeName.Properties.Appearance.Options.UseForeColor = true;
            this.textEditRecipeName.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditRecipeName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.textEditRecipeName.Size = new System.Drawing.Size(945, 40);
            this.textEditRecipeName.TabIndex = 6;
            // 
            // checkButtonBaseStage
            // 
            this.checkButtonBaseStage.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.checkButtonBaseStage.Location = new System.Drawing.Point(1166, 8);
            this.checkButtonBaseStage.Name = "checkButtonBaseStage";
            this.checkButtonBaseStage.Size = new System.Drawing.Size(40, 40);
            this.checkButtonBaseStage.TabIndex = 7;
            this.checkButtonBaseStage.ToolTip = "Відображення базового рецепта";
            this.checkButtonBaseStage.CheckedChanged += new System.EventHandler(this.checkButtonBaseStage_CheckedChanged);
            // 
            // btnSaveFromPLC
            // 
            this.btnSaveFromPLC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveFromPLC.ImageOptions.SvgImage = global::TecxRecipeAX.Properties.Resources.save;
            this.btnSaveFromPLC.Location = new System.Drawing.Point(1769, 8);
            this.btnSaveFromPLC.Name = "btnSaveFromPLC";
            this.btnSaveFromPLC.Size = new System.Drawing.Size(136, 40);
            this.btnSaveFromPLC.TabIndex = 8;
            this.btnSaveFromPLC.Text = "Save to DB";
            this.btnSaveFromPLC.ToolTip = "Зберегти поточний рецепт";
            this.btnSaveFromPLC.Click += new System.EventHandler(this.btnSaveFromPLC_Click);
            // 
            // btnAssignRecipeToPages
            // 
            this.btnAssignRecipeToPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAssignRecipeToPages.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAssignRecipeToPages.ImageOptions.SvgImage = global::TecxRecipeAX.Properties.Resources.applychanges;
            this.btnAssignRecipeToPages.Location = new System.Drawing.Point(300, 8);
            this.btnAssignRecipeToPages.Name = "btnAssignRecipeToPages";
            this.btnAssignRecipeToPages.Size = new System.Drawing.Size(40, 40);
            this.btnAssignRecipeToPages.TabIndex = 9;
            this.btnAssignRecipeToPages.ToolTip = "Назначити поточним рецептом";
            this.btnAssignRecipeToPages.Click += new System.EventHandler(this.btnAssignRecipeToPages_Click);
            // 
            // lbSelectRecipe
            // 
            this.lbSelectRecipe.Location = new System.Drawing.Point(16, 8);
            this.lbSelectRecipe.Name = "lbSelectRecipe";
            this.lbSelectRecipe.Size = new System.Drawing.Size(75, 13);
            this.lbSelectRecipe.TabIndex = 10;
            this.lbSelectRecipe.Text = "Вибір рецепта:";
            // 
            // RecipeUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEditStages);
            this.Controls.Add(this.btnEditParameters);
            this.Controls.Add(this.lbSelectRecipe);
            this.Controls.Add(this.btnAssignRecipeToPages);
            this.Controls.Add(this.btnSaveFromPLC);
            this.Controls.Add(this.checkButtonBaseStage);
            this.Controls.Add(this.checkBoxShowPLC);
            this.Controls.Add(this.textEditRecipeName);
            this.Controls.Add(this.lookUpEditRecipe);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.progressBarControl1);
            this.Controls.Add(this.gridControlStages);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnDataToPlc);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.btnRefreshRecipe);
            this.Controls.Add(this.btnEditRecipe);
            this.Name = "RecipeUI";
            this.Size = new System.Drawing.Size(1921, 958);
            this.Load += new System.EventHandler(this.RecipeUI_Load);
            this.VisibleChanged += new System.EventHandler(this.RecipeUI_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRecipe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditRecipeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnEditParameters;
        private DevExpress.XtraGrid.GridControl gridControlStages;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewStages;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DevExpress.XtraEditors.SimpleButton btnEditStages;
        private DevExpress.XtraEditors.SimpleButton btnEditRecipe;
        private DevExpress.XtraEditors.SimpleButton btnRefreshRecipe;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditRecipe;
        private DevExpress.XtraEditors.SimpleButton btnDebug;
        private DevExpress.XtraEditors.SimpleButton btnDataToPlc;
        private DevExpress.XtraEditors.CheckButton checkBoxShowPLC;
        private System.Windows.Forms.ToolStripStatusLabel plcStatusLabel;
        private DevExpress.XtraEditors.SimpleButton btnSaveAs;
        private DevExpress.XtraEditors.TextEdit textEditRecipeName;
        private DevExpress.XtraEditors.CheckButton checkButtonBaseStage;
        private DevExpress.XtraEditors.SimpleButton btnSaveFromPLC;
        private DevExpress.XtraEditors.SimpleButton btnAssignRecipeToPages;
        private DevExpress.XtraEditors.LabelControl lbSelectRecipe;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
    }
}
