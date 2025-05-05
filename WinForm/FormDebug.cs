using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TechRecipeAX;
using System.IO;
using DevExpress.XtraPrinting.Native;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TecxRecipeAX
{
	public partial class FormDebug: XtraForm
	{
        private RecipeDbContext _context;
        private string _logFilePath;
        private Timer _refreshTimer;
        private CtApi _ctApi;

        private string tagRead;

        public FormDebug(RecipeDbContext context)
		{
            InitializeComponent();

            _context = context;
            _logFilePath = Logger.logFilePath;
            _ctApi = new CtApi();

            LoadLogs();
            InitCtApi();
            //StartAutoRefresh();
        }

        private void LoadLogs()
        {

            if (File.Exists(_logFilePath))
            {
                textBoxLogs.Text = File.ReadAllText(_logFilePath);
            }
            else
            {
                textBoxLogs.Text = "Файл логов не найден.";
            }
        }

        private void StartAutoRefresh()
        {
            _refreshTimer = new Timer();
            _refreshTimer.Interval = 3000; // каждые 3 секунды
            _refreshTimer.Tick += (s, e) => LoadLogs();
            _refreshTimer.Start();
        }

        private void InitCtApi()
        {
            //_ctApi.SetCtApiDirectory(@"D:\CtApi\x64");
            _ctApi.SetCtApiDirectory(@"D:\AVEVA\AVEVA Plant SCADA\Bin\Bin (x64)");
            _ctApi.OpenAsync("192.168.15.5", "Al", "kip12345");
        }

        #region Form

        private void FormDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
            _ctApi.Close();
        }

        #endregion

        #region Buttons
        private async void btnTagRead_Click(object sender, EventArgs e)
        {
            var sTagName = textEditTagName.Text;
            //var sTagName = tagRead;
            if (string.IsNullOrWhiteSpace(sTagName))
            {
                MessageBox.Show("Выберите тег из списка.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var value = await _ctApi.TagReadAsync(sTagName);
                textEditTagValue.Text = value;
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Ошибка чтения тега: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                var value = await _ctApi.TagReadAsync(sTagName);
                textEditTagValue.Text = value;
                throw;
            }
        }

        private async void btnTagWrite_Click(object sender, EventArgs e)
        {
            var sTagName = textEditTagName.Text;
            var value = textEditTagValue.Text;
            try
            {
                await _ctApi.TagWriteAsync(sTagName, value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка записи тега: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




            //var value = textEditTagValue.Text;
            //_ctApi.TagWriteAsync(sTagName, value);
        }

        private async void btnFind_Click(object sender, EventArgs e)
        {
            var findResult = await _ctApi.FindAsync("Tag", "Tag=*WT*;ITEM=Hmi", "", "TAG");
            //var findResult = await _ctApi.FindAsync("Equip", "Name=*TT*", "", "TAGPREFIX");

            var values = findResult
                .SelectMany(dict => dict.Values)
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct()
                .OrderBy(v => v)
                .Select(v => new { Value = v })
                .ToList();

            lookUpEditFindResult.Properties.DataSource = values;
            lookUpEditFindResult.Properties.DisplayMember = "Value";
            lookUpEditFindResult.Properties.ValueMember = "Value";
            lookUpEditFindResult.EditValue = null;

            foreach (var value in values)
            {
                await _ctApi.TagReadAsync(value.Value.ToString());
            }

            if (values.Count > 0)
                lookUpEditFindResult.EditValue = values.First().Value;
        }


        #endregion

        private void lookUpEditFindResult_EditValueChanged(object sender, EventArgs e)
        {
            tagRead = lookUpEditFindResult.EditValue as string;
            textEditTagName.EditValue = tagRead;
        }
    }
}