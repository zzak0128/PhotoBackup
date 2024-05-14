using PhotoBackup.Library.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhotoBackup.CLI;
using PhotoBackup.Library.SettingsModels;
using PhotoBackup.Library.Interfaces;
using CommandLine;

var builder = CreateHostBuilder(args);
using IHost host = builder.Build();

using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    services.GetRequiredService<App>().Run(args);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    await host.StopAsync();
}


IHostBuilder CreateHostBuilder(string[] args)
{
    IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json", false)
            .Build();

    Settings settings = BuildSettingsFromConfig(config);

    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            services.AddSingleton(config);
            services.AddSingleton<ISettings>(settings);
            services.AddSingleton<App>();
        });
}




Settings BuildSettingsFromConfig(IConfiguration config)
{
    Settings settings = new()
    {
        DirectoryPaths = config.BuildSettings<DirectoryPaths>(),
    };

    return settings;
}

//using PhotoBackup.CLI.Views;

//if (args.Length == 0)
//{
//    DisplayView.StartMessage();
//}
//else
//{
//    if (args[0].ToLower() == "--import")
//    {
//        if (string.IsNullOrEmpty(args[1]))
//        {
//            DisplayView.StartMessage();
//        }
//        else
//        {
//            Console.WriteLine("Beginning import");

//            string filePath = args[1];
//            System.Console.WriteLine(filePath);
//            //await PhotoFetcher.PhotoImport(filePath);
//        }
//    }
//    else
//    {
//        Console.WriteLine("Beginning cleanup");

//        string filePath = args[0];
//        System.Console.WriteLine(filePath);
//        // PhotoReorganizer scanner = new PhotoReorganizer();
//        // scanner.CreateDirectories(filePath);
//        // scanner.MoveFiles(filePath);
//    }

//}


//using PhotoBackup.Library;

//using (var scanner = new IPhonePhotoBackup())
//{
//    scanner.Scan(DeviceType.iPhone);
//    Console.WriteLine(scanner.Device.FriendlyName);
//    foreach (var file in scanner.ActiveDirectory.FileList)
//    {
//        Console.WriteLine(file.FullName);
//    }

//    scanner.DownloadScannedFiles(@"C:\users\703434671\Downloads\PhotoTest");
//}

//DirectoryOrganizer.Organize(@"C:\users\703434671\Downloads\PhotoTest");