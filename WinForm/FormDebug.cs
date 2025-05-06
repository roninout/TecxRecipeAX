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
        private string _lastReadLogLine = null;

        public FormDebug(RecipeDbContext context)
		{
            InitializeComponent();

            _context = context;
            _logFilePath = Logger.logFilePath;
            _ctApi = new CtApi();

            Shown += async (_, __) => await LoadLogsWithColorsAsync();
            InitCtApi();
        }

        #region CtApi

        private void InitCtApi()
        {
            //_ctApi.SetCtApiDirectory(@"D:\CtApi\x64");
            _ctApi.SetCtApiDirectory(@"D:\AVEVA\AVEVA Plant SCADA\Bin\Bin (x64)");
            _ctApi.OpenAsync("192.168.15.5", "Al", "kip12345");
        }

        #endregion

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

            if (string.IsNullOrWhiteSpace(sTagName))
            {
                MessageBox.Show("Выберите тег из списка.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var value = await _ctApi.TagReadAsync(sTagName);
                textEditTagValue.Text = value;
                await LoadLogsWithColorsAsync(loadAll: false);
            }
            catch (Exception)
            {

            }
        }

        private async void btnTagWrite_Click(object sender, EventArgs e)
        {
            var sTagName = textEditTagName.Text;
            var value = textEditTagValue.Text;
            try
            {
                await _ctApi.TagWriteAsync(sTagName, value);
                await LoadLogsWithColorsAsync(loadAll: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка записи тега: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                //await Task.Delay(10);
            }

            if (values.Count > 0)
                lookUpEditFindResult.EditValue = values.First().Value;

            await LoadLogsWithColorsAsync(loadAll: false);
        }


        #endregion

        #region LookUpEdit

        private void lookUpEditFindResult_EditValueChanged(object sender, EventArgs e)
        {
            tagRead = lookUpEditFindResult.EditValue as string;
            textEditTagName.EditValue = tagRead;
        }

        #endregion

        #region Logger

        private async Task LoadLogsWithColorsAsync(bool loadAll = true)
        {
            if (loadAll)
                richTextBoxLogs.Text = "ЗАГРУЗКА данных...";

            var linesWithColors = await ReadAndFormatLogLinesAsync(loadAll);
            await Task.Delay(100); // позволяет UI обновить "ЗАГРУЗКА данных"

            if (loadAll)
            {
                richTextBoxLogs.Clear();
                richTextBoxLogs.SuspendLayout();
            }

            var sb = new StringBuilder();
            Color currentColor = linesWithColors.FirstOrDefault().color;

            foreach (var (line, color) in linesWithColors)
            {
                if (color == currentColor)
                {
                    sb.AppendLine(line);
                }
                else
                {
                    AppendTextWithColor(sb.ToString(), currentColor);
                    sb.Clear();
                    sb.AppendLine(line);
                    currentColor = color;
                }
            }

            if (sb.Length > 0)
                AppendTextWithColor(sb.ToString(), currentColor);

            if (loadAll)
                richTextBoxLogs.ResumeLayout();
        }

        private Color GetLineColor(string line)
        {
            if (line.Contains("[ERROR]")) return Color.Red;
            if (line.Contains("[WARNING]")) return Color.DarkOrange;
            if (line.Contains("[DEBUG]")) return Color.Gray;
            if (line.Contains("[TRACE]")) return Color.DarkGreen;
            return Color.Black; // INFO и прочее
        }

        private async Task<List<(string line, Color color)>> ReadAndFormatLogLinesAsync(bool readAll = true)
        {
            return await Task.Run(() =>
            {
                var formattedLines = new List<(string line, Color color)>();

                if (!File.Exists(_logFilePath))
                {
                    formattedLines.Add(("Файл логов не найден.", Color.Black));
                    return formattedLines;
                }

                var lines = File.ReadLines(_logFilePath).Reverse();

                foreach (var line in lines)
                {
                    if (!readAll)
                    {
                        // Прекращаем считывание, как только нашли последнюю строку из предыдущего чтения
                        if (line == _lastReadLogLine)
                            break;
                    }

                    var color = GetLineColor(line);
                    formattedLines.Add((line, color));
                }

                // Сохраняем последнюю считанную строку (верхнюю в логе)
                if (formattedLines.Count > 0)
                {
                    _lastReadLogLine = formattedLines.First().line;
                }

                return formattedLines;
            });
        }

        private void AppendTextWithColor(string text, Color color)
        {
            richTextBoxLogs.SelectionStart = richTextBoxLogs.TextLength;
            richTextBoxLogs.SelectionLength = 0;
            richTextBoxLogs.SelectionColor = color;
            richTextBoxLogs.AppendText(text);
            richTextBoxLogs.SelectionColor = richTextBoxLogs.ForeColor; // сброс цвета
        }

        #endregion
    }
}