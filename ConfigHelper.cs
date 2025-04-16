using System;
using System.Configuration;

namespace TecxRecipeAX
{
    public static class ConfigHelper
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DatabaseConnection"]?.ConnectionString
                   ?? throw new InvalidOperationException("Строка подключения не найдена в конфигурации.");
        }
    }
}
