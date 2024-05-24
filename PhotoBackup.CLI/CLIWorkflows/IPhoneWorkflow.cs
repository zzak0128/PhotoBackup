using PhotoBackup.Library;
using PhotoBackup.Library.Interfaces;
using System.Runtime.Versioning;

namespace PhotoBackup.CLI.CLIWorkflows;

[SupportedOSPlatform("windows")]
internal static class IPhoneWorkflow
{
    internal static async Task Run(ISettings settings, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Attempting to scan device for photos...");
        IPhonePhotoBackup backup = new IPhonePhotoBackup(settings);

        Console.WriteLine($"Found {backup.ActiveDirectory.Count()} files on device");
        Console.WriteLine($"Beginning photo backup to {settings.DirectoryPaths.DestinationDirectory}");


        Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
        progress.ProgressChanged += ReportProgress;

        await backup.BackupFilesAsync(progress, cancellationToken);

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
        Console.WriteLine("Press enter key to exit...");
        Console.ReadLine();
    }

    private static void ReportProgress(object? sender, ProgressReportModel e)
    {
        Console.CursorVisible = false;

        try
        {
            var cusorStart = Console.GetCursorPosition();
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, cusorStart.Top);
            Console.Write($"{e.PercentageComplete}% Completed -- Current File: {e.CurrentFile}");
        }
        finally
        {

            Console.CursorVisible = true;
        }
    }
}
