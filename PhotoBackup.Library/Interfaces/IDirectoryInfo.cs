namespace PhotoBackup.Library;

public interface IDirectoryInfo<T>
{
    string PhotoDirectoryPath { get; set; }

    IList<T> FileList { get; set; }

    IList<T> GetFiles();

    int Count();
}
