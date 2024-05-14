using System.Collections;

namespace PhotoBackup.Library;

public interface IDirectoryInfo
{
    string PhotoDirectoryPath { get; set; }

    IList FileList { get; set; }

    IList GetFiles();

    int Count();
}
