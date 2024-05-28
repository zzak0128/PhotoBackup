using MediaDevices;
using PhotoBackup.Library.Interfaces;

namespace PhotoBackup.Library.Models;

public abstract class DirectoryBackup
{
    public virtual IDirectoryInfo ActiveDirectory { get; set; }

    public DirectoryBackup()
    {
    }

    public abstract Task BackupFilesAsync(IProgress<ProgressReportModel> progress, CancellationToken cancellationToken);

    public abstract bool CompareAndUpdate();
    //{
    //    bool hasChanges = true;

    //    IList filesToRemove;

    //    foreach (var file in ActiveDirectory.FileList)
    //    {
    //        foreach (var filecomp in directoryToCompare.FileList)
    //        {
    //            if (file.Name == filecomp.Name)
    //            {
    //                filesToRemove.Add(file);
    //                //Console.WriteLine($"{file.Name} already exists at destination");
    //            }
    //        }
    //    }

    //    foreach (var file in filesToRemove)
    //    {
    //        ActiveDirectory.FileList.Remove(file);
    //    }

    //    if (filesToRemove.Count == 0)
    //    {
    //        output = false;
    //    }

    //    return hasChanges;
    //}
}
