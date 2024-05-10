using ImageMagick;
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
                throw new Exception("No devices are connected");
            }
        }

        public void Scan(DeviceType deviceType)
        {
            if (Device.IsConnected == false)
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
                catch
                {
                    throw;
                }
            }

        }

        public void DownloadScannedFiles(string downloadPath)
        {
            Directory.CreateDirectory(downloadPath);

            if (Device.IsConnected == false)
            {
                throw new Exception("Device is not connected");
            }

            foreach (var file in ActiveDirectory.FileList)
            {
                string fullDownloadPath = Path.Combine(downloadPath, file.Name);
                FileInfo deviceFileInfo = new(file.FullName);
                if (deviceFileInfo.Extension == ".MOV" || deviceFileInfo.Extension == ".AAE")
                {
                    var fileLength = (file.Length / 1024f) / 1024f;
                    if (fileLength >= 6)
                    {
                        file.CopyTo(fullDownloadPath, true);
                    }
                }
                else
                {
                    file.CopyTo(fullDownloadPath, true);

                    if (_ = new FileInfo(file.FullName).Extension == ".HEIC")
                    {
                        ConvertToJpeg(fullDownloadPath, downloadPath);
                        File.Delete(fullDownloadPath);
                    }
                }
            }
        }

        private void ConvertToJpeg(string fileName, string outputPath)
        {
            try
            {
                using (var image = new MagickImage(fileName))
                {
                    image.Format = MagickFormat.Jpeg;
                    image.Write($"{outputPath}/{Path.GetFileNameWithoutExtension(fileName)}.jpg");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            if (Device is null)
            {
                return;
            }

            Device.Disconnect();
            Device.Dispose();
        }
    }
}
