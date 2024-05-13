using MediaDevices;

namespace PhotoBackup.Library;

public class IPhoneDirectoryInfo : IDirectoryInfo<MediaFileInfo>
{
    public string PhotoDirectoryPath { get; set; }

    public IList<MediaFileInfo> FileList { get; set; } = [];

    public IPhoneDirectoryInfo()
    {
        PhotoDirectoryPath = @"/Internal Storage/";
    }

    public IPhoneDirectoryInfo(string directoryPath)
    {
        PhotoDirectoryPath = directoryPath;

        FileList = GetFiles();
    }

    public IList<MediaFileInfo> GetFiles()
    {
        FileList.Clear();

        MediaDevice device = MediaDevice.GetDevices().FirstOrDefault(x => x.FriendlyName.Contains("iPhone"));
        
            if (device == null)
            {
                throw new Exception("No Device Detected, unable to fetch directory contents");
            }

            device.Connect();

            if (device.DirectoryExists(PhotoDirectoryPath))
            {
                try
                {
                    var deviceDirectories = device.GetDirectories(PhotoDirectoryPath);

                    foreach (var directory in deviceDirectories)
                    {
                        MediaDirectoryInfo directoryInfo = device.GetDirectoryInfo(directory);
                        foreach (var file in directoryInfo.EnumerateFiles())
                        {
                            FileList.Add(file);
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }

            device.Disconnect();


        return FileList;
    }

    public int Count()
    {
        if (FileList.Any())
        {
            return FileList.Count;
        }

        throw new Exception("FileList has not been populated");
    }
}
