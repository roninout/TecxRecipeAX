using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace TecxRecipeAX
{
    public static class ConfigHelper
    {
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

        //public static string GetConnectionString()
        //{
        //    return ConfigurationManager.ConnectionStrings["DatabaseConnection"]?.ConnectionString
        //           ?? throw new InvalidOperationException("Строка подключения не найдена в конфигурации.");
        //}
    }


}
