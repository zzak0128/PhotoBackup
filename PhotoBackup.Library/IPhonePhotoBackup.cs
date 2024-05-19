using System.Runtime.Versioning;
using ImageMagick;
using MediaDevices;
using PhotoBackup.Library.Interfaces;
using PhotoBackup.Library.Models;

namespace PhotoBackup.Library;

[SupportedOSPlatform("windows")]
public class IPhonePhotoBackup : DirectoryBackup
{
    public IPhonePhotoBackup(ISettings settings) : base(settings)
    {
        ActiveDirectory = new IPhoneDirectoryInfo(_settings.DirectoryPaths.IPhoneDirectory);
    }

    public override void BackupFiles()
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
                    MediaFileInfo fileInfo = (MediaFileInfo)file;

                    string fullDownloadPath = Path.Combine(destinationPath, fileInfo.Name);
                    FileInfo deviceFileInfo = new(fileInfo.FullName);
                    if (deviceFileInfo.Extension == ".MOV" || deviceFileInfo.Extension == ".AAE")
                    {
                        var fileLength = (fileInfo.Length / 1024f) / 1024f;
                        if (fileLength >= 6)
                        {
                            fileInfo.CopyTo(fullDownloadPath, true);
                        }
                    }
                    else
                    {
                        // Need to check with proper extension before downloading. Currently, it downloads regardless
                        fileInfo.CopyTo(fullDownloadPath, true);

                        if (_ = new FileInfo(fileInfo.FullName).Extension == ".HEIC")
                        {
                            ConvertToJpeg(fullDownloadPath, destinationPath);
                            File.Delete(fullDownloadPath);
                        }
                    }
                }
                device.Disconnect();
            }
        }
    }

    public override bool CompareAndUpdate()
    {
        bool hasChanges = true;

        IDirectoryInfo directoryToCompare = new LocalDirectoryInfo(_settings.DirectoryPaths.DestinationDirectory);
        IList<MediaFileInfo> filesToRemove = [];

        foreach (var file in ActiveDirectory.FileList)
        {
            foreach (var filecomp in directoryToCompare.FileList)
            {
                MediaFileInfo mediaFileInfo = (MediaFileInfo)file;
                FileInfo fileInfo = (FileInfo)filecomp;
                if (mediaFileInfo.Name == fileInfo.Name)
                {
                    filesToRemove.Add(mediaFileInfo);
                }
            }
        }

        foreach (var file in filesToRemove)
        {
            ActiveDirectory.FileList.Remove(file);
        }

        if (filesToRemove.Count == 0 && directoryToCompare.Count() > 0)
        {
            hasChanges = false;
        }

        return hasChanges;
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
        catch
        {
            throw;
        }
    }
}
