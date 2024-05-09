using MediaDevices;
using PhotoBackup.Library.Interfaces;

namespace PhotoBackup.Library
{
    public class IPhonePhotoBackup : IFileScanner
    {
        public IDirectoryInfo<MediaFileInfo> ActiveDirectory { get; set; } = new IPhoneDirectoryInfo();

        public MediaDevice Device { get; set; }

        public IPhonePhotoBackup()
        {
            try
            {
                var devices = MediaDevice.GetDevices();
                Device = devices.First(d => d.FriendlyName.Contains("iPhone"));
                Device.Connect();
            }
            catch (Exception e)
            {
                Dispose();
                throw;
            }
        }



        public void Scan(DeviceType deviceType)
        {
            if(Device.IsConnected == false)
            {
                throw new Exception("Device is not connected");
            }

            ActiveDirectory = new IPhoneDirectoryInfo();

            if (Device.DirectoryExists(ActiveDirectory.PhotoDirectoryPath))
            {
                try
                {
                    var deviceDirectories = Device.GetDirectories(ActiveDirectory.PhotoDirectoryPath);

                    foreach (var directory in deviceDirectories)
                    {
                        MediaDirectoryInfo directoryInfo = Device.GetDirectoryInfo(directory);
                        foreach (var file in directoryInfo.EnumerateFiles())
                        {
                            ActiveDirectory.FileList.Add(file);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            
        }


        public void Download(string downloadPath)
        {
            if (Device.IsConnected == false)
            {
                throw new Exception("Device is not connected");
            }

            foreach(var file in  ActiveDirectory.FileList)
            {
                file.CopyTo(downloadPath);
            }

        }

        public void Dispose()
        {
            Device.Disconnect();
            Device.Dispose();
        }
    }
}
