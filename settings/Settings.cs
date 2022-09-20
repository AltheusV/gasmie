using Microsoft.Extensions.Configuration;

namespace gasmie.settings
{
    public static class Settings
    {
        private static IConfigurationRoot Setup()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile(
                Path.Combine(Environment.CurrentDirectory, "settings", "appsettings.json")
                , false
                , true
            );

            return builder.Build();
        }

        public static (string, string, string) GetNotionConnectionStrings()
        {
            var settings = Setup();
            return (settings["ConnectionStrings:NotionUrl"]
                , settings["ConnectionStrings:NotionDatabaseId"]
                , settings["ConnectionStrings:NotionKey"]
            );
        }
    }
}