using PhotoBackup.Library;
using PhotoBackup.Library.Interfaces;


namespace PhotoBackup.WinForm
{
    public partial class Dashboard : Form
    {
        private readonly ISettings _settings;
        public Dashboard(ISettings settings)
        {
            _settings = settings;
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            backupDirectoryText.PlaceholderText = _settings.DirectoryPaths.IPhoneDirectory;
            destinationText.PlaceholderText = _settings.DirectoryPaths.DestinationDirectory;
        }

        private async void backupStartButton_Click(object sender, EventArgs e)
        {
            backupStartButton.Enabled = false;
            cancelButton.Enabled = true;
            statusbarProgress.Visible = true;
            statusLabel.Text = "Backing Up";
            outputText.Text = $"Backing up {GetText(backupDirectoryText)} to {GetText(destinationText)}";
            IPhonePhotoBackup backup = new IPhonePhotoBackup(_settings);

            _settings.DirectoryPaths.DestinationDirectory = GetText(destinationText);

            Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;

            await Task.Run(() => backup.BackupFiles(progress));

            outputText.Text += Environment.NewLine + "Backup Complete";
            statusLabel.Text = "Ready";
            statusbarProgress.Value = 0;
            statusbarProgress.Visible = false;
            backupStartButton.Enabled = true;
            cancelButton.Enabled = false;
        }

        private string GetText(TextBox textBox)
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
            
        }
    }
}
