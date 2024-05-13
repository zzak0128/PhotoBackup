namespace PhotoBackup.Library.SettingsModels;

public class Settings : ISettings
{
    public required DirectoryPaths DirectoryPaths { get; set; }
}
