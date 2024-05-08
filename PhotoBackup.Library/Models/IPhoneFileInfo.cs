namespace PhotoBackup.Library;

public class IPhoneFileInfo : IFileInfo
{
    public string Path { get; set; }

    public string Extension { get; set; }

    public string? Directory { get; set; }

    public IPhoneFileInfo(string filePath)
    {
        FileInfo fileInfo = new(filePath);
        Path = fileInfo.FullName;
        Extension = fileInfo.Extension;
        Directory = fileInfo.DirectoryName;
    }

}
