using ImageMagick;

namespace PhotoBackup.Library;

public static class DirectoryOrganizer
{
    public async static Task OrganizeAsync(string directoryPath, CancellationToken cancellationToken)
    {
        await Task.Run(() => MoveFiles(directoryPath, cancellationToken), cancellationToken).ConfigureAwait(false);
    }

    private static void MoveFiles(string directoryPath, CancellationToken cancellationToken)
    {
        DirectoryInfo dir = new DirectoryInfo(directoryPath);
        foreach (var file in dir.GetFiles())
        {
            if (cancellationToken.IsCancellationRequested == false)
            {
                DateTime fileDate = GetDateTaken(file);

                var slash = Path.DirectorySeparatorChar;
                string moveToDirectory = $"{directoryPath}{slash}{fileDate.Date.Month}-{fileDate.Date.Year}";
                string fileName = file.Name;
                Directory.CreateDirectory(moveToDirectory);
                try
                {
                    file.MoveTo(Path.Combine(moveToDirectory, fileName));
                }
                catch (IOException)
                {
                    Directory.CreateDirectory(Path.Combine(directoryPath, "Duplicates"));
                    file.MoveTo(Path.Combine(directoryPath, "Duplicates", fileName), true);
                    Console.WriteLine(file.Name);
                }
                catch
                {
                    throw;
                }
            }
        }
    }

    private static DateTime GetDateTaken(FileInfo image)
    {
        MagickImage? imageFile;
        try
        {
            imageFile = new MagickImage(image);

        }
        catch
        {
            return image.LastWriteTime;
        }
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


