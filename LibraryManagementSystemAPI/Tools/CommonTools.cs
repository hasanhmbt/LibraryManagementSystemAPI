namespace LibraryManagementSystemAPIAPI.Tools
{
    public class CommonTools
    {
        public static string GetAppSettings(string key) => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>(key);
    }
}
