using System.Runtime.Versioning;
using ImageMagick;
using MediaDevices;
using PhotoBackup.Library.Interfaces;
using PhotoBackup.Library.Models;

namespace PhotoBackup.Library;

[SupportedOSPlatform("windows")]
public class IPhonePhotoBackup : DirectoryBackup
{
    public IPhonePhotoBackup() : base()
    {
        ActiveDirectory = new IPhoneDirectoryInfo(UserSettings.Default.IPhoneDirectory);
    }

    public override async Task BackupFilesAsync(IProgress<ProgressReportModel> progress, CancellationToken cancellationToken)
    {
        var destinationPath = UserSettings.Default.DestinationDirectory;
        Directory.CreateDirectory(destinationPath);

        bool isDifferent = await Task.Run(() => CompareAndUpdate(), cancellationToken).ConfigureAwait(false);

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
                ProgressReportModel report = new ProgressReportModel();
                int filesDone = 0;

                foreach (var file in ActiveDirectory.FileList)
                {
                    if (cancellationToken.IsCancellationRequested == false)
                    {
                        MediaFileInfo fileInfo = (MediaFileInfo)file;

                        string fullDownloadPath = Path.Combine(destinationPath, fileInfo.Name);
                        FileInfo deviceFileInfo = new(fileInfo.FullName);
                        if (deviceFileInfo.Extension == ".MOV" || deviceFileInfo.Extension == ".AAE")
                        {
                            var fileLength = (fileInfo.Length / 1024f) / 1024f;
                            if (fileLength >= 6)
                            {
                                report.CurrentFile = fileInfo.Name;
                                filesDone++;
                                report.PercentageComplete = (filesDone * 100) / ActiveDirectory.Count();
                                progress.Report(report);
                                await Task.Run(() => fileInfo.CopyTo(fullDownloadPath, true), cancellationToken).ConfigureAwait(false);
                                //fileInfo.CopyTo(fullDownloadPath, true);
                            }
                        }
                        else
                        {
                            filesDone++;
                            report.CurrentFile = fileInfo.Name;
                            report.PercentageComplete = (filesDone * 100) / ActiveDirectory.Count();
                            progress.Report(report);

                            // TODO: Need to check with proper extension before downloading. Currently, it downloads regardless
                            await Task.Run(() => fileInfo.CopyTo(fullDownloadPath, true), cancellationToken).ConfigureAwait(false);
                            //fileInfo.CopyTo(fullDownloadPath, true);

                            if (_ = new FileInfo(fileInfo.FullName).Extension == ".HEIC")
                            {
                                await ConvertToJpegAsync(fullDownloadPath, destinationPath, cancellationToken).ConfigureAwait(false);
                                File.Delete(fullDownloadPath);
                            }
                        }
                    }
                    else
                    {
                        device.Disconnect();
                        return;
                    }
                }

                report.PercentageComplete = 100;
                report.CurrentFile = "";
                progress.Report(report);
                device.Disconnect();
            }
        }
    }


    public override bool CompareAndUpdate()
    {
        bool hasChanges = true;

        IDirectoryInfo directoryToCompare = new LocalDirectoryInfo(UserSettings.Default.DestinationDirectory);
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

    private static async Task ConvertToJpegAsync(string fileName, string outputPath, CancellationToken cancellationToken)
    {
        try
        {
            using (var image = new MagickImage(fileName))
            {
                image.Format = MagickFormat.Jpeg;
                await image.WriteAsync(fileName: $"{outputPath}/{Path.GetFileNameWithoutExtension(fileName)}.jpg", cancellationToken: cancellationToken);
            }
        }
        catch
        {
            throw;
        }
    }
}
