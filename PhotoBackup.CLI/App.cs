using PhotoBackup.Library;
using PhotoBackup.Library.SettingsModels;

namespace PhotoBackup.CLI;

public class App
{
    private readonly ISettings _settings;

    public App(ISettings settings)
    {
        _settings = settings;
    }

    public void Run(string[] args)
    {

        IPhonePhotoBackup backup = new(_settings);
 
        Console.Clear();
        Console.WriteLine(backup.ActiveDirectory.Count());

        backup.DownloadScannedFiles();

        Console.WriteLine(backup.ActiveDirectory.Count());
        Console.ReadLine();
        //backup.DownloadScannedFiles();


        //Console.WriteLine("Download?");
        //ConsoleKeyInfo keyPressed = Console.ReadKey();
        //if(keyPressed.Key == ConsoleKey.Y)
        //{
        //    backup.DownloadScannedFiles();
        //}
        //else
        //{
        //    Console.WriteLine("Closing Application");
        //    Console.ReadLine();
        //    Environment.Exit(0);
        //}

        if (args.Length == 0)
        {
            Console.WriteLine("No Arguments");
        }
        else
        {
            foreach (string arg in args)
            {
                Console.WriteLine(arg);
            }
        }
    }
}
