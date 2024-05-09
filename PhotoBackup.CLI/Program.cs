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


using PhotoBackup.Library;

using (var scanner = new IPhonePhotoBackup())
{
    scanner.Scan(DeviceType.iPhone);
    Console.WriteLine(scanner.Device.FriendlyName);
    foreach (var file in scanner.ActiveDirectory.FileList)
    {
        Console.WriteLine(file.FullName);
    }
}

Console.ReadLine();