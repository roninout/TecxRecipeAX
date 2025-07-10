using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace TecxRecipeAX
{
    public static class ConfigHelper
    {
        private static readonly string XmlSettingsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"AppSettings.xml");

        // настройки для PostgreSql
        public static string GetConnectionString()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string configPath = assemblyLocation + ".config";

            if (!File.Exists(configPath))
                throw new FileNotFoundException("Файл конфигурации не найден", configPath);

            var configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configPath };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            ConnectionStringSettings connection = config.ConnectionStrings.ConnectionStrings["DatabaseConnection"];

            if (connection == null || string.IsNullOrWhiteSpace(connection.ConnectionString))
                throw new InvalidOperationException("Строка подключения 'DatabaseConnection' не найдена или пуста.");

            return connection.ConnectionString;
        }

        // настройки для CtApi в файле AppSettings.xml
        private static XElement LoadSettings()
        {
            if (!File.Exists(XmlSettingsPath))
                throw new FileNotFoundException("Файл AppSettings.xml не найден", XmlSettingsPath);

            return XElement.Load(XmlSettingsPath);
        }

        private static string GetSetting(string name)
        {
            var settings = LoadSettings();
            var element = settings.Element(name);
            //if (element == null || string.IsNullOrWhiteSpace(element.Value))
            //    throw new InvalidOperationException($"Параметр '{name}' не найден или пуст в AppSettings.xml");

            return element.Value.Trim();
        }

        // CtApi
        public static string CtApiPath => GetSetting("CtApiPath");
        public static string CtApiIp => GetSetting("CtApiIp");
        public static string CtApiUser => GetSetting("CtApiUser");
        public static string CtApiPassword => GetSetting("CtApiPassword");
        public static string CtApiRefreshData => GetSetting("CtApiRefreshData");

        // Set tags for PLC
        public static string RecipeStageBase1Id => GetSetting("RecipeStageBase1Id");
        public static string RecipeStageBase2Id => GetSetting("RecipeStageBase2Id");

        public static string RecipeStageBase1Tag => GetSetting("RecipeStageBase1Tag");
        public static string RecipeStageBase2Tag => GetSetting("RecipeStageBase2Tag");

        public static string RecipeStageR01Tag => GetSetting("RecipeStageR01Tag");
        public static string RecipeStageT08Tag => GetSetting("RecipeStageT08Tag");
        public static string RecipeStageT09Tag => GetSetting("RecipeStageT09Tag");
        public static string RecipeStageT10Tag => GetSetting("RecipeStageT10Tag");

        // Table config
        public static string ColumnParameterStageWidth => GetSetting("ColumnParameterStageWidth");
        public static string ColumnParameterInputFieldWidth => GetSetting("ColumnParameterInputFieldWidth");
        public static string ColumnParameterCheckBoxWidth => GetSetting("ColumnParameterCheckBoxWidth");
    }


}
