namespace PhotoBackup.Library;

public class LocalDirectoryInfo : IDirectoryInfo
{
    public string PhotoDirectoryPath { get; set; }

    public IEnumerable<IFileInfo> FileList { get; set; }

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

    public List<IFileInfo> GetFiles()
    {
        // Return list of all files in directory
        throw new NotImplementedException();
    }
}
