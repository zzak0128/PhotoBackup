using PhotoBackup.Library;


namespace PhotoBackup.WinForm
{
    public partial class Dashboard : Form
    {
        private CancellationTokenSource? tokenSource;

        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            backupDirectoryText.PlaceholderText = UserSettings.Default.IPhoneDirectory;
            destinationText.PlaceholderText = UserSettings.Default.DestinationDirectory;
        }

        private async void backupStartButton_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource();

            backupStartButton.Enabled = false;
            cancelButton.Enabled = true;
            orgButton.Enabled = false;

            statusbarProgress.Visible = true;
            statusLabel.Text = "Backing Up";
            outputText.Text = $"Backing up {GetText(backupDirectoryText)} to {GetText(destinationText)}";
            IPhonePhotoBackup backup = new();

            UserSettings.Default.DestinationDirectory = GetText(destinationText);

            Progress<ProgressReportModel> progress = new();
            progress.ProgressChanged += ReportProgress;

            await backup.BackupFilesAsync(progress, tokenSource.Token);

            if (tokenSource.Token.IsCancellationRequested)
            {
                outputText.Text += Environment.NewLine + "Backup has been termintated";
                statusLabel.Text = "Ready";
            }
            else
            {
                outputText.Text += Environment.NewLine + "Backup Complete";
                statusLabel.Text = "Ready";
            }

            statusbarProgress.Value = 0;
            statusbarProgress.Visible = false;
            backupStartButton.Enabled = true;
            cancelButton.Enabled = false;
            orgButton.Enabled = true;
        }

        private static string GetText(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                return textBox.PlaceholderText;
            }

            return textBox.Text;
        }

        private void ReportProgress(object? sender, ProgressReportModel e)
        {
            outputText.Text += $"{Environment.NewLine}{e.CurrentFile}";
            statusbarProgress.Value = e.PercentageComplete;
        }

        private async void cancelButton_Click(object sender, EventArgs e)
        {
            outputText.Text += Environment.NewLine + "Stopping Backup";
            statusLabel.Text = "Stopping";
            await tokenSource!.CancelAsync();

        }

        private async void orgButton_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource();

            outputText.Text = "Organizing Directory";
            statusLabel.Text = "Organizing...";
            try
            {
                await DirectoryOrganizer.OrganizeAsync(UserSettings.Default.DestinationDirectory, tokenSource.Token);
                outputText.Text = "Directory has been organized";
                statusLabel.Text = "Ready";
            }
            catch (DirectoryNotFoundException)
            {
                outputText.Text = "ERROR: The directory could not be found or does not exist.";
                statusLabel.Text = "Error";
            }
        }
    }
}
