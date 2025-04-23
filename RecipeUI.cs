using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;
using TecxRecipeAX;
using System.Data.Entity;
using System.Data.SqlClient;
using DevExpress.Xpo.Logger;
using System.Diagnostics;
using System.IO;

namespace TechRecipeAX
{
    [ComVisible(true)]
    [Guid("A855AD2E-66E5-4961-8423-E28E37EA1A25")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(ITechRecipeAX))]
    [ComSourceInterfaces(typeof(ITechRecipeAXEvents))]

    public partial class RecipeUI : UserControl, ITechRecipeAX
    {
        private RecipeDbContext _context;
        //private static readonly TraceSource _logger = new TraceSource("TechRecipeAX.Database");

        public RecipeUI()
		{
            InitializeComponent();
            // Запускаем асинхронную инициализацию
            this.Load += async (sender, e) => await InitializeAsync();
        }

        #region Init data

        private async Task InitializeAsync()
        {
            Logger.Info(@" -------------------- Начало инициализации приложения ---------------- ");

            try
            {
                await InitializeDbContextAsync();
                await LoadDataAsync(); // Теперь можно вызывать асинхронно
                Logger.Info(@"Инициализация приложения завершена успешно");
            }
            catch (Exception ex)
            {
                Logger.Error($"Ошибка при инициализации приложения: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }

        private async Task InitializeDbContextAsync()
        {
            try
            {
                string connectionString = ConfigHelper.GetConnectionString();
                Logger.Debug($"Получена строка подключения: {connectionString}");

                _context = new RecipeDbContext(connectionString);

                // Проверяем подключение асинхронно
                await _context.Database.Connection.OpenAsync();
                Logger.Info($"Версия SQL Server: {_context.Database.Connection.ServerVersion}");
                _context.Database.Connection.Close();

            }
            catch (Exception ex)
            {
                Logger.Error($"Ошибка подключения к БД: {ex.Message}");

                // Обработка ошибок подключения
                toolStripStatusLabel1.Text = $"Ошибка подключения к БД: {ex.Message}";
                toolStripStatusLabel1.ForeColor = Color.Red;
                throw; // Можно обработать иначе, в зависимости от требований
            }
        }

        private async Task LoadDataAsync()
        {
            Logger.Info("Старт асинхронной загрузки данных");

            try
            {
                if (_context == null) { await InitializeDbContextAsync(); }

                toolStripStatusLabel1.Text = "Подключение к базе данных...";
                toolStripStatusLabel1.ForeColor = Color.Black;

                progressBarControl1.Visible = true;
                progressBarControl1.EditValue = 0;

                int totalItems = await _context.BaseParameters.CountAsync();
                int currentItem = 0;

                var stages = await _context.BaseParameters.ToListAsync();
                Logger.Debug($"Загружено {stages.Count} записей");

                foreach (var stage in stages)
                {
                    currentItem++;
                    progressBarControl1.EditValue = (int)((double)currentItem / totalItems * 100);
                    await Task.Delay(10);
                }

                progressBarControl1.Visible = false;
                gridControl1.Visible = true;
                toolStripStatusLabel1.Text = "Подключение установлено.";
                toolStripStatusLabel1.ForeColor = Color.Green;
                Logger.Info("Данные успешно загружены.");
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка при загрузке данных");
                toolStripStatusLabel1.Text = $"Ошибка: {ex.Message}";
                toolStripStatusLabel1.ForeColor = Color.Red;
                progressBarControl1.Visible = false;
            }
        }

        #endregion

        #region Регистрация класса в реестре
        [ComRegisterFunction()]
        public static void RegisterClass(string key)
        {
            // Удаляем HKEY_CLASSES_ROOT\ из переданного ключа
            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // Открываем ключ CLSID\{guid} в режиме записи
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // Создаем ключ элемента управления ActiveX – это позволяет ему появиться
            //в контейнере элемента управления ActiveX
            RegistryKey ctrl = k.CreateSubKey("Control");
            ctrl.Close();

            // Создаем запись CodeBase
            RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);
            inprocServer32.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
            inprocServer32.Close();

            // И в заключении закрываем ключ реестра 
            k.Close();
        }

        [ComUnregisterFunction()]
        public static void UnregisterClass(string key)
        {
            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // Открываем ключ HKCR\CLSID\{guid} в режиме записи
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // Удаляем ключ элемента управления ActiveX 
            k.DeleteSubKey("Control", false);

            // Затем открываем ключ InprocServer32
            RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);

            // Удаляем ключ CodeBase
            k.DeleteSubKey("CodeBase", false);

            // И в заключении закрываем ключ реестра
            k.Close();
        }
        #endregion

        #region Form buttons
        // создание и редактирование параметров
        private void btnEditParameters_Click(object sender, EventArgs e)
        {
            using (var form = new FormEditParameters(_context))
            {
                form.ShowDialog();
            }
        }

        // создание и редактирование стадий
        private void btnEditStages_Click(object sender, EventArgs e)
        {
            using (var form = new FormEditStages(_context))
            {
                form.ShowDialog();
            }
        }

        // создание и редактирование рецептов
        private void btnEditRecipe_Click(object sender, EventArgs e)
        {
            using (var form = new RecipeEditForm(_context))
            {
                form.ShowDialog();
            }
        }

        #endregion

    }
}
