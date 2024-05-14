using PhotoBackup.Library;
using PhotoBackup.Library.Interfaces;

namespace PhotoBackup.CLI.CLIWorkflows;

internal static class IPhoneWorkflow
{
    internal static void Run(ISettings settings)
    {
        Console.WriteLine($"Attempting to scan device for photos...");
        IPhonePhotoBackup backup = new IPhonePhotoBackup(settings);

        Console.WriteLine($"Found {backup.ActiveDirectory.Count()} files on device");
        Console.WriteLine($"Beginning photo backup to {settings.DirectoryPaths.DestinationDirectory}");

        backup.BackupFiles();

        Console.WriteLine("Backup Completed");
        Console.WriteLine($"{backup.ActiveDirectory.Count()} photos were backed up.");

        Console.WriteLine("Would you like to organize the destination folder? Y or N");
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
                Console.WriteLine("Invalid key was pressed. Assuming that's a No.");
                break;
        }

        Console.WriteLine(Environment.NewLine);
        Console.WriteLine("Backup and Organization has finished.");
        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }
}
