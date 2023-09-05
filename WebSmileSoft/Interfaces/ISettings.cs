namespace WebSmileSoft.Interfaces
{
    public interface ISettings
    {
        string urlEndPoint { get; }
        int TimeOutSession { get; }
        string Environment { get; }
    }
}
