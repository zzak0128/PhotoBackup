namespace PhotoBackup.Library;

public interface IDirectoryInfo
{
    public string PhotoDirectoryPath { get; set; }

    public IEnumerable<IFileInfo> FileList { get; set; }

    public int Count();   

    public List<IFileInfo> GetFiles();
}
