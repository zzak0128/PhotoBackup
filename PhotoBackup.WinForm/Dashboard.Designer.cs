namespace PhotoBackup.WinForm
{
    partial class Dashboard
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            statusStrip = new StatusStrip();
            statusbarProgress = new ToolStripProgressBar();
            statusLabel = new ToolStripStatusLabel();
            backupStartButton = new Button();
            backupDirectoryLabel = new Label();
            backupDirectoryText = new TextBox();
            destinationLabel = new Label();
            destinationText = new TextBox();
            outputText = new TextBox();
            outputLabel = new Label();
            cancelButton = new Button();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { statusbarProgress, statusLabel });
            statusStrip.Location = new Point(0, 604);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1155, 22);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip1";
            // 
            // statusbarProgress
            // 
            statusbarProgress.Name = "statusbarProgress";
            statusbarProgress.Size = new Size(100, 16);
            statusbarProgress.Step = 100;
            statusbarProgress.Style = ProgressBarStyle.Continuous;
            statusbarProgress.Visible = false;
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(39, 17);
            statusLabel.Text = "Ready";
            // 
            // backupStartButton
            // 
            backupStartButton.Location = new Point(341, 83);
            backupStartButton.Name = "backupStartButton";
            backupStartButton.Size = new Size(140, 62);
            backupStartButton.TabIndex = 1;
            backupStartButton.Text = "Backup Now";
            backupStartButton.UseVisualStyleBackColor = true;
            backupStartButton.Click += backupStartButton_Click;
            // 
            // backupDirectoryLabel
            // 
            backupDirectoryLabel.AutoSize = true;
            backupDirectoryLabel.Location = new Point(12, 9);
            backupDirectoryLabel.Name = "backupDirectoryLabel";
            backupDirectoryLabel.Size = new Size(85, 30);
            backupDirectoryLabel.TabIndex = 2;
            backupDirectoryLabel.Text = "Backup:";
            // 
            // backupDirectoryText
            // 
            backupDirectoryText.Location = new Point(12, 42);
            backupDirectoryText.Name = "backupDirectoryText";
            backupDirectoryText.Size = new Size(1115, 35);
            backupDirectoryText.TabIndex = 3;
            // 
            // destinationLabel
            // 
            destinationLabel.AutoSize = true;
            destinationLabel.Location = new Point(12, 120);
            destinationLabel.Name = "destinationLabel";
            destinationLabel.Size = new Size(124, 30);
            destinationLabel.TabIndex = 4;
            destinationLabel.Text = "Destination:";
            // 
            // destinationText
            // 
            destinationText.Location = new Point(12, 153);
            destinationText.Name = "destinationText";
            destinationText.Size = new Size(1115, 35);
            destinationText.TabIndex = 5;
            // 
            // outputText
            // 
            outputText.Location = new Point(12, 259);
            outputText.Multiline = true;
            outputText.Name = "outputText";
            outputText.ReadOnly = true;
            outputText.ScrollBars = ScrollBars.Vertical;
            outputText.Size = new Size(1115, 317);
            outputText.TabIndex = 6;
            // 
            // outputLabel
            // 
            outputLabel.AutoSize = true;
            outputLabel.Location = new Point(12, 226);
            outputLabel.Name = "outputLabel";
            outputLabel.Size = new Size(79, 30);
            outputLabel.TabIndex = 7;
            outputLabel.Text = "Output";
            // 
            // cancelButton
            // 
            cancelButton.Enabled = false;
            cancelButton.Location = new Point(509, 83);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(140, 62);
            cancelButton.TabIndex = 8;
            cancelButton.Text = "Stop";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1155, 626);
            Controls.Add(cancelButton);
            Controls.Add(outputLabel);
            Controls.Add(outputText);
            Controls.Add(destinationText);
            Controls.Add(destinationLabel);
            Controls.Add(backupDirectoryText);
            Controls.Add(backupDirectoryLabel);
            Controls.Add(backupStartButton);
            Controls.Add(statusStrip);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5, 6, 5, 6);
            Name = "Dashboard";
            Text = "PhotoBackup Dashboard";
            Load += Dashboard_Load;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStripProgressBar statusbarProgress;
        private Button backupStartButton;
        private Label backupDirectoryLabel;
        private TextBox backupDirectoryText;
        private Label destinationLabel;
        private TextBox destinationText;
        private TextBox outputText;
        private Label outputLabel;
        private Button cancelButton;
    }
}
