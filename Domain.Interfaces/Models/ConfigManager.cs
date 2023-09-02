namespace Domain.Interfaces
{
    public class ConfigManager : IConfigManager
    {
        public string SmileSoftConnection { get; set; } = String.Empty;

        public string GetConnectionDisTriConn()
        {
            return SmileSoftConnection;
        }
    }
}
