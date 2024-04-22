using MyEducationCenter.Core;



namespace WoodWise.WebApi
{
    public class AppSettings
    {
        public static AppSettings Instance { get; private set; }
        public JwtSettings JwtSettings { get; set; } = new JwtSettings();
        public static void Init(AppSettings instance)
        {
            Instance = instance;
        }
    }
}
