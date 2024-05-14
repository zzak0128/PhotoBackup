using MediaDevices;
using System.Collections;

namespace PhotoBackup.Library;

public class IPhoneDirectoryInfo : IDirectoryInfo
{
    public string PhotoDirectoryPath { get; set; }

    public IList FileList { get; set; }

    public IPhoneDirectoryInfo()
    {
        PhotoDirectoryPath = @"/Internal Storage/";
    }

    public IPhoneDirectoryInfo(string directoryPath)
    {
        PhotoDirectoryPath = directoryPath;

        FileList = GetFiles();
    }

    public IList GetFiles()
    {
        FileList = new List<MediaFileInfo>();

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
        int count = FileList.Count;

        if (count > 0)
        {
            return count;
        }

        throw new Exception("FileList has not been populated");
    }
}
