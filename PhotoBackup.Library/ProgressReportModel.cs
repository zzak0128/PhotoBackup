namespace PhotoBackup.Library;

public class ProgressReportModel
{
    public int PercentageComplete { get; set; } = 0;

    public string CurrentFile { get; set; } = "";
}
