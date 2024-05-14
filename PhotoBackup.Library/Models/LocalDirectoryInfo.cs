using System.Collections;

namespace PhotoBackup.Library;

public class LocalDirectoryInfo : IDirectoryInfo
{
    public string PhotoDirectoryPath { get; set; }

    public IList FileList { get; set; }


    public LocalDirectoryInfo(string directoryPath)
    {
        PhotoDirectoryPath = directoryPath;
        FileList = new List<FileInfo>();
        FileList = GetFiles();
    }

    public IList GetFiles()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(PhotoDirectoryPath);
        FileList = directoryInfo.GetFiles();

        return FileList;
    }

    public int Count()
    {
        int count = FileList.Count;

        if (count > 0)
        {
            return count;
        }

        throw new Exception("No files found in local directory");
    }
}
