using MediaDevices;

namespace PhotoBackup.Library;

public class PhotoScanner
{
    public IDirectoryInfo Scan(string? directoryPath, DeviceType deviceType)
    {
        IDirectoryInfo directoryInfo = new LocalDirectoryInfo("");

        if (directoryPath is null)
        {
            switch (deviceType)
            {
#if Windows
                case DeviceType.iPhone:
                directoryInfo = ScanIPhoneDirectory();
                    break;
#endif
                case DeviceType.Android:
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (deviceType)
            {
                case DeviceType.Local:
                    directoryInfo = ScanLocalDirectory(directoryPath);
                    break;
                case DeviceType.SFTP:
                    break;
                default:
                    break;
            }
        }

        return directoryInfo;
    }

    public LocalDirectoryInfo ScanLocalDirectory(string directoryPath)
    {
        LocalDirectoryInfo output = new LocalDirectoryInfo(directoryPath);

        // Use PhotoDirInfo to scan the gieven directory
        // Get related information needed

        return output;
    }

#if Windows
    public IPhoneDirectoryInfo ScanIPhoneDirectory()
    {
        IPhoneDirectoryInfo output = new();
        // check device and scan
        var devices = MediaDevice.GetDevices();
    
    try
    {
        using (var device = devices.First(d => d.FriendlyName.Contains("iPhone")))
        {
            device.Connect();

            string photoPath = @"/Internal Storage/DCIM";

            if (device.DirectoryExists(photoPath))
            {
                var deviceDirectories = device.GetDirectories(photoPath);

                foreach (var directory in deviceDirectories)
                {
                    output.FileList.Add(new IPhoneFileInfo(directory));
                    //device.DownloadFolder(directory, filePath, true);
                }
            }

            device.Disconnect();

            return output;
        }
    }
    catch
    {
        throw;
    }

    }
#endif

}
