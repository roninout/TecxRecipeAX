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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecipeUI));
            this.btnEditParameters = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlStages = new DevExpress.XtraGrid.GridControl();
            this.gridViewStages = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnEditStages = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditRecipe = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefreshRecipe = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEditRecipe = new DevExpress.XtraEditors.LookUpEdit();
            this.btnDebug = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRecipe.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEditParameters
            // 
            this.btnEditParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditParameters.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEditParameters.ImageOptions.Image")));
            this.btnEditParameters.Location = new System.Drawing.Point(665, 18);
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
            this.gridControlStages.Location = new System.Drawing.Point(16, 65);
            this.gridControlStages.MainView = this.gridViewStages;
            this.gridControlStages.Name = "gridControlStages";
            this.gridControlStages.Size = new System.Drawing.Size(1069, 507);
            this.gridControlStages.TabIndex = 1;
            this.gridControlStages.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewStages});
            // 
            // gridViewStages
            // 
            this.gridViewStages.GridControl = this.gridControlStages;
            this.gridViewStages.Name = "gridViewStages";
            this.gridViewStages.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewStages_RowCellStyle);
            this.gridViewStages.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridViewStages_ShowingEditor);
            this.gridViewStages.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewStages_CellValueChanged);
            this.gridViewStages.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.GridViewStages_CustomUnboundColumnData);
            this.gridViewStages.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.gridViewStages_ValidatingEditor);
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl1.Location = new System.Drawing.Point(16, 575);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Properties.ShowTitle = true;
            this.progressBarControl1.Properties.Step = 1;
            this.progressBarControl1.Size = new System.Drawing.Size(1069, 18);
            this.progressBarControl1.TabIndex = 2;
            this.progressBarControl1.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 594);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1101, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "\"Подключение к базе данных...\"";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // btnEditStages
            // 
            this.btnEditStages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditStages.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEditStages.ImageOptions.Image")));
            this.btnEditStages.Location = new System.Drawing.Point(807, 18);
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
            this.btnEditRecipe.Location = new System.Drawing.Point(949, 18);
            this.btnEditRecipe.Name = "btnEditRecipe";
            this.btnEditRecipe.Size = new System.Drawing.Size(136, 40);
            this.btnEditRecipe.TabIndex = 0;
            this.btnEditRecipe.Text = "Edit recipe";
            this.btnEditRecipe.Click += new System.EventHandler(this.btnEditRecipe_Click);
            // 
            // btnRefreshRecipe
            // 
            this.btnRefreshRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshRecipe.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnRefreshRecipe.ImageOptions.SvgImage")));
            this.btnRefreshRecipe.Location = new System.Drawing.Point(16, 18);
            this.btnRefreshRecipe.Name = "btnRefreshRecipe";
            this.btnRefreshRecipe.Size = new System.Drawing.Size(40, 40);
            this.btnRefreshRecipe.TabIndex = 0;
            this.btnRefreshRecipe.ToolTip = "Перечитати поточний рецепт з БД";
            this.btnRefreshRecipe.Click += new System.EventHandler(this.btnRefreshRecipe_Click);
            // 
            // lookUpEditRecipe
            // 
            this.lookUpEditRecipe.Location = new System.Drawing.Point(124, 18);
            this.lookUpEditRecipe.Name = "lookUpEditRecipe";
            this.lookUpEditRecipe.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditRecipe.Size = new System.Drawing.Size(216, 20);
            this.lookUpEditRecipe.TabIndex = 4;
            this.lookUpEditRecipe.EditValueChanged += new System.EventHandler(this.lookUpEditRecipe_EditValueChanged);
            // 
            // btnDebug
            // 
            this.btnDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDebug.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage")));
            this.btnDebug.Location = new System.Drawing.Point(450, 18);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(136, 40);
            this.btnDebug.TabIndex = 0;
            this.btnDebug.Text = "Debug";
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // RecipeUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lookUpEditRecipe);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.progressBarControl1);
            this.Controls.Add(this.gridControlStages);
            this.Controls.Add(this.btnEditRecipe);
            this.Controls.Add(this.btnEditStages);
            this.Controls.Add(this.btnRefreshRecipe);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.btnEditParameters);
            this.Name = "RecipeUI";
            this.Size = new System.Drawing.Size(1101, 616);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlStages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewStages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditRecipe.Properties)).EndInit();
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
    }
}
