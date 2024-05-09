namespace PhotoBackup.Library;

public class LocalDirectoryInfo : IDirectoryInfo<FileInfo>
{
    public string PhotoDirectoryPath { get; set; }

    public IList<FileInfo> FileList { get; set; }

    public LocalDirectoryInfo(string directoryPath)
    {
        PhotoDirectoryPath = directoryPath;
        FileList = [];
    }

    public int Count()
    {
        //Get valid files in given directory and return count of files
        throw new NotImplementedException();
    }

}
