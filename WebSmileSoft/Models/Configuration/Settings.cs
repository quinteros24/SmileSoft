using WebSmileSoft.Interfaces;

namespace WebSmileSoft.Models
{
    public class Settings : ISettings
    {
        public string urlEndPoint { get; set; } = String.Empty;

        public int TimeOutSession { get; set; } = 0;

        public string Environment { get; set; } = null!;
    }
}