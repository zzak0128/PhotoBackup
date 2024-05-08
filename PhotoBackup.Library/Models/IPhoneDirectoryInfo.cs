using MediaDevices;

namespace PhotoBackup.Library;

public class IPhoneDirectoryInfo : IDirectoryInfo
{
    public string PhotoDirectoryPath { get; set; }

    public IEnumerable<IFileInfo> FileList { get; set; }

    public IPhoneDirectoryInfo(string directoryPath)
    {
        PhotoDirectoryPath = directoryPath;
        FileList = [];
    }

    public int Count()
    {
        throw new NotImplementedException();
    }

    public List<IFileInfo> GetFiles()
    {
        throw new NotImplementedException();
    }
}
