namespace WebSmileSoft.Interfaces
{
    public interface ISettings
    {
        string UrlEndPoint { get; }
        int TimeOutSession { get; }
        string Environment { get; }
    }
}
