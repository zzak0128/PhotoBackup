using PhotoBackup.Library.SettingsModels;

namespace PhotoBackup.Library.Interfaces
{
    public interface ISettings
    {
        DirectoryPaths DirectoryPaths { get; set; }
    }
}