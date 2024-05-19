using ImageMagick;

namespace PhotoBackup.Library;

public static class DirectoryOrganizer
{
    public static void Organize(string directoryPath)
    {
        MoveFiles(directoryPath);
    }

    private static void MoveFiles(string directoryPath)
    {
        DirectoryInfo dir = new DirectoryInfo(directoryPath);
        foreach (var file in dir.GetFiles())
        {
            DateTime fileDate = GetDateTaken(file);

            var slash = Path.DirectorySeparatorChar;
            string moveToDirectory = $"{directoryPath}{slash}{fileDate.Date.Month}-{fileDate.Date.Year}";
            string fileName = file.Name;
            try
            {
                Directory.CreateDirectory(moveToDirectory);
                file.MoveTo(Path.Combine(moveToDirectory, fileName));
            }
            catch
            {
                throw;
            }
        }
    }

    private static DateTime GetDateTaken(FileInfo image)
    {
        var imageFile = new MagickImage(image);
        var exifProfile = imageFile.GetExifProfile();
        if (exifProfile is null)
        {
            return image.LastWriteTime;
        }

        string? lastModified = exifProfile.Values.FirstOrDefault(x => x.Tag == ExifTag.DateTimeOriginal)?.ToString();
        if (lastModified == null)
        {
            return image.LastWriteTime;
        }

        lastModified = lastModified.Replace(":", "-").Split(" ")[0];

        DateTime dateTaken;
        var canParse = DateTime.TryParse(lastModified.ToString(), out dateTaken);

        if (canParse == false)
        {
            return image.LastWriteTime;
        }

        return dateTaken;
    }
}


