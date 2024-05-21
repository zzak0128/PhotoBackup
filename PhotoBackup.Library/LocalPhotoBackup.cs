using MediaDevices;
using PhotoBackup.Library.Interfaces;
using PhotoBackup.Library.Models;

namespace PhotoBackup.Library;

public class LocalPhotoBackup : DirectoryBackup
{
    public LocalPhotoBackup(ISettings settings) : base(settings)
    {
        ActiveDirectory = new LocalDirectoryInfo(_settings.DirectoryPaths.LocalDirectory);
    }

    public override void BackupFiles(IProgress<ProgressReportModel> progress)
    {
        var destinationPath = _settings.DirectoryPaths.DestinationDirectory;
        Directory.CreateDirectory(destinationPath);

        bool isDifferent = CompareAndUpdate();

        if (isDifferent)
        {
                foreach (var file in ActiveDirectory.FileList)
                {
                    FileInfo fileInfo = (FileInfo)file;

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

                        if (fileInfo.Extension == ".HEIC")
                        {
                            File.Delete(fullDownloadPath);
                        }
                    }
                }
            }
        }
    

    public override bool CompareAndUpdate()
    {
         bool hasChanges = true;

        IDirectoryInfo directoryToCompare = new LocalDirectoryInfo(_settings.DirectoryPaths.DestinationDirectory);
        IList<FileInfo> filesToRemove = [];

        foreach (var file in ActiveDirectory.FileList)
        {
            foreach (var filecomp in directoryToCompare.FileList)
            {
                FileInfo mediaFileInfo = (FileInfo)file;
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

        // Check is pointless atm
        if (filesToRemove.Count == 0 && directoryToCompare.Count() == 0)
        {
            hasChanges = false;
        }

        return hasChanges;
    }
}
