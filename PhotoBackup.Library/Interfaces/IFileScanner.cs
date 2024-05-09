namespace PhotoBackup.Library.Interfaces;

public interface IFileScanner : IDisposable
{
    public void Scan(DeviceType deviceType);

    public void Download(string downloadPath);

}
