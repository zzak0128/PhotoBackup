using CommandLine;

namespace PhotoBackup.CLI;

internal class CLIOptions
{
    [Option('v', Default = false, HelpText = "Prints all messages to standard output.")]
    public bool Verbose { get; set; }

    [Option('i', "iphone", HelpText = "Backup photos from an iPhone")]
    public bool IsIphone { get; set; }

    [Option('l', "local-dir", HelpText = "Path to local folder to backup")]
    public string LocalDirectory { get; set; }

    [Option('d', "destination", HelpText = "Path to destination where photos will be backed up to")]
    public string DestinationDirectory { get; set; }
}