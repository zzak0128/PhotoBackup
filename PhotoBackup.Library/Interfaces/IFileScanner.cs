using System.Runtime.CompilerServices;

namespace PhotoBackup.Library.Interfaces;

public interface IFileScanner : IDisposable
{
    public void Scan(DeviceType deviceType);

    public void DownloadScannedFiles(string downloadPath);

}
