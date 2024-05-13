using ImageMagick;
using MediaDevices;
using PhotoBackup.Library.Interfaces;
using PhotoBackup.Library.SettingsModels;

namespace PhotoBackup.Library
{
    public class IPhonePhotoBackup : IFileScanner
    {
        private readonly ISettings _settings;

        public IDirectoryInfo<MediaFileInfo> ActiveDirectory { get; set; }

        public IPhonePhotoBackup(ISettings settings)
        {
            _settings = settings;

            ActiveDirectory = new IPhoneDirectoryInfo(_settings.DirectoryPaths.IPhoneDirectory);
        }

        public void DownloadScannedFiles()
        {
            var destinationPath = _settings.DirectoryPaths.DestinationDirectory;
            Directory.CreateDirectory(destinationPath);

            bool isDifferent = CompareAndUpdate();

            if (isDifferent)
            {
                using (MediaDevice? device = MediaDevice.GetDevices().FirstOrDefault(x => x.FriendlyName.Contains("iPhone")))
                {
                    if (device == null)
                    {
                        throw new Exception("No Device Detected, unable to fetch directory contents");
                    }

                    if (device.IsConnected == false)
                    {
                        device.Connect();
                    }

                    device.Connect();

                    foreach (var file in ActiveDirectory.FileList)
                    {
                        string fullDownloadPath = Path.Combine(destinationPath, file.Name);
                        FileInfo deviceFileInfo = new(file.FullName);
                        if (deviceFileInfo.Extension == ".MOV" || deviceFileInfo.Extension == ".AAE")
                        {
                            var fileLength = (file.Length / 1024f) / 1024f;
                            if (fileLength >= 6)
                            {
                                file.CopyTo(fullDownloadPath, true);
                                Console.WriteLine(file.FullName);
                            }
                        }
                        else
                        {
                            // Need to check with proper extension before downloading. Currently, it downloads regardless
                            file.CopyTo(fullDownloadPath, true);
                            Console.WriteLine(file.FullName);

                            if (_ = new FileInfo(file.FullName).Extension == ".HEIC")
                            {
                                ConvertToJpeg(fullDownloadPath, destinationPath);
                                File.Delete(fullDownloadPath);
                            }
                        }
                        Console.WriteLine(file.FullName);
                    }
                    device.Disconnect();
                }
            }
        }

        private bool CompareAndUpdate()
        {
            bool output = true;

            IDirectoryInfo<FileInfo> directoryToCompare = new LocalDirectoryInfo(_settings.DirectoryPaths.DestinationDirectory);
            List<MediaFileInfo> filesToRemove = new List<MediaFileInfo>();

            foreach (var file in ActiveDirectory.FileList)
            {
                foreach (var filecomp in directoryToCompare.FileList)
                {
                    if (file.Name == filecomp.Name)
                    {
                        filesToRemove.Add(file);
                        //Console.WriteLine($"{file.Name} already exists at destination");
                    }
                }
            }

            foreach (var file in filesToRemove)
            {
                ActiveDirectory.FileList.Remove(file);
            }

            if (filesToRemove.Count == 0)
            {
                output = false;
            }

            return output;
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
    }
}
