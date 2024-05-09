using MediaDevices;

namespace PhotoBackup.Library;

public interface IDirectoryInfo<T>
{
    public string PhotoDirectoryPath { get; set; }

    public IList<T> FileList { get; set; }

    public int Count();   
}
