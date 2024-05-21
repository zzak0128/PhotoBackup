using PhotoBackup.Library;
using PhotoBackup.Library.Interfaces;

namespace PhotoBackup.CLI.CLIWorkflows;

internal static class LocalWorkflow
{
    internal static void Run(ISettings settings)
    {
        Console.WriteLine($"Scanning given directory for photos ({settings.DirectoryPaths.LocalDirectory})...");
        LocalPhotoBackup backup = new LocalPhotoBackup(settings);

        Console.WriteLine($"Found {backup.ActiveDirectory.Count()} files in local directory");
        Console.WriteLine($"Beginning photo backup to {settings.DirectoryPaths.DestinationDirectory}");

        //backup.BackupFiles();

        Console.WriteLine(Environment.NewLine);
        Console.WriteLine("Backup Completed");
        Console.WriteLine($"{backup.ActiveDirectory.Count()} photos were backed up.");

        Console.WriteLine("Would you like to organize the destination folder by date taken? Y or N");
        var keyPressed = Console.ReadKey(true);
        switch (keyPressed.Key)
        {
            case ConsoleKey.Y:
                Console.WriteLine("Organizing Directory...");
                DirectoryOrganizer.Organize(settings.DirectoryPaths.DestinationDirectory);
                Console.WriteLine("Organization Complete.");
                break;
            case ConsoleKey.N:
                break;
            default:
                Console.WriteLine("Invalid key was pressed. Assuming No");
                break;
        }

        Console.WriteLine(Environment.NewLine);
        Console.WriteLine("Backup and Organization has finished.");
        Console.WriteLine("Press enter key to exit...");
        Console.ReadLine();
    }
}
