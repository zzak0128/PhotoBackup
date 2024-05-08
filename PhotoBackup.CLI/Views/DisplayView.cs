namespace PhotoBackup.CLI.Views;

public static class DisplayView
{
    public static void StartMessage()
    {
        Console.WriteLine($"PhotoManager{Environment.NewLine}");
        Console.WriteLine($"Usage: PhotoManager [path-to-folder]");
        Console.WriteLine($"Usage: PhotoManager --import [save-to-folder-path]{Environment.NewLine}");
        Console.WriteLine($"path-to-folder:");
        Console.WriteLine("    The path to the folder you want to organize into date folders");
        Console.WriteLine($"save-to-folder-path:");
        Console.WriteLine("    The path to the folder you want to save iPhone photos to");
    }
}

