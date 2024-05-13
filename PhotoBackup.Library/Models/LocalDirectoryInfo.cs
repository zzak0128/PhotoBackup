namespace PhotoBackup.Library;

public class LocalDirectoryInfo : IDirectoryInfo<FileInfo>
{
    public string PhotoDirectoryPath { get; set; }

    public IList<FileInfo> FileList { get; set; }

    public LocalDirectoryInfo(string directoryPath)
    {
        PhotoDirectoryPath = directoryPath;

        FileList = GetFiles();
    }

    public IList<FileInfo> GetFiles()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(PhotoDirectoryPath);
        FileList = directoryInfo.GetFiles();

        return FileList;
    }

    public int Count()
    {
        return FileList.Count;
    }
}
