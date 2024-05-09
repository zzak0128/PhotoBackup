using MediaDevices;

namespace PhotoBackup.Library;

public class IPhoneDirectoryInfo : IDirectoryInfo<MediaFileInfo>
{
    public string PhotoDirectoryPath { get; set; }

    public IList<MediaFileInfo> FileList { get; set; }

    public IPhoneDirectoryInfo()
    {
        PhotoDirectoryPath = @"/Internal Storage/";
        FileList = [];
    }

    public IPhoneDirectoryInfo(string directoryPath)
    {
        
    }

    public int Count()
    {
        throw new NotImplementedException();
    }
}
