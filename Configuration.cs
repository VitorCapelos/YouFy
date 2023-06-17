using Google.Apis.YouTube.v3.Data;

namespace YouFy
{
    public class Configuration
    {
        private static IConfigurationSection configurationSettingsSP = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Spotify");
        private static IConfigurationSection configurationSettingsYT = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Youtube");

        public static string CLIENT_ID
        {
            get
            {
                return configurationSettingsSP["CLIENT_ID"];
            }
        }
        public static string CLIENT_SECRET
        {
            get
            {
                return configurationSettingsSP["CLIENT_SECRET"];
            }
        }

        public static string YT_API_KEY
        {
            get
            {
                return configurationSettingsYT["API_KEY"];
            }
        }
    }
}
