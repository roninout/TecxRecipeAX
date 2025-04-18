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

        public RecipeUI()
		{
            InitializeComponent();
            _context = new RecipeDbContext(ConfigHelper.GetConnectionString());
            LoadDataAsync();
        }

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

        #endregion

        private async void LoadDataAsync()
        {
            //// Показываем прогресс-бар
            //progressBarControl1.Visible = true;
            //progressBarControl1.EditValue = 0;

            //// Задаем прогресс-бар на 0 в начале
            //int totalItems = await _context.BaseParameters.CountAsync(); // Пример для количества элементов
            //int currentItem = 0;

            //// Асинхронно загружаем данные (можно адаптировать для вашего случая)
            //var stages = await _context.BaseParameters.ToListAsync();

            //// Обновление прогресс-бара
            //foreach (var stage in stages)
            //{
            //    currentItem++;
            //    progressBarControl1.EditValue = (int)((double)currentItem / totalItems * 100);
            //    await Task.Delay(10); // Имитация времени загрузки
            //}

            //// Данные загружены, скрываем прогресс-бар и показываем gridControl
            //progressBarControl1.Visible = false;

            try
            {
                // Обновляем строку состояния о процессе подключения
                toolStripStatusLabel1.Text = "Подключение к базе данных...";
                toolStripStatusLabel1.ForeColor = Color.Black;

                // Проверка подключения к БД
                if (!await TestDatabaseConnectionAsync())
                {
                    throw new Exception("Не удалось подключиться к базе данных.");
                }

                // Обновляем строку состояния на "Подключено"
                toolStripStatusLabel1.Text = "Подключение установлено.";
                toolStripStatusLabel1.ForeColor = Color.Green;

                // Показываем прогресс-бар
                progressBarControl1.Visible = true;
                progressBarControl1.EditValue = 0;

                // Получаем количество элементов для прогресса
                int totalItems = await _context.BaseParameters.CountAsync();
                int currentItem = 0;

                // Асинхронно загружаем данные
                var stages = await _context.BaseParameters.ToListAsync();

                // Обновление прогресс-бара
                foreach (var stage in stages)
                {
                    currentItem++;
                    progressBarControl1.EditValue = (int)((double)currentItem / totalItems * 100);
                    await Task.Delay(10); // Имитация времени загрузки
                }

                // Данные загружены, скрываем прогресс-бар и показываем gridControl
                progressBarControl1.Visible = false;
                gridControl1.Visible = true; // Показываем таблицу
            }
            catch (Exception ex)
            {
                // В случае ошибки обновляем строку состояния
                toolStripStatusLabel1.Text = $"Ошибка подключения: {ex.Message}";
                toolStripStatusLabel1.ForeColor = Color.Red;

                // Скрываем прогресс-бар, если возникла ошибка
                progressBarControl1.Visible = false;
            }
        }

        // Метод для проверки подключения
        private async Task<bool> TestDatabaseConnectionAsync()
        {
            try
            {
                // Используем SqlConnection для проверки подключения
                using (var connection = new SqlConnection(ConfigHelper.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    return connection.State == System.Data.ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
