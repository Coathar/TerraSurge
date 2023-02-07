namespace TerraSurge
{
    partial class TerraSurge
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
            ConfigurationLbl = new Label();
            ApplicationLabel = new Label();
            DebugRecreateDB = new Button();
            DebugRunLoaders = new Button();
            StartStopBtn = new Button();
            PreviewPictureBox = new PictureBox();
            NetworkDeviceComboBox = new ComboBox();
            NetworkDeviceLabel = new Label();
            label3 = new Label();
            label4 = new Label();
            CurrentPlayerDisplayLbl = new Label();
            SerialDeviceLabel = new Label();
            SerialDeviceComboBox = new ComboBox();
            ProcessingPreviewPictureBox = new PictureBox();
            label1 = new Label();
            RTMPFeedTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)PreviewPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ProcessingPreviewPictureBox).BeginInit();
            SuspendLayout();
            // 
            // ConfigurationLbl
            // 
            ConfigurationLbl.AutoSize = true;
            ConfigurationLbl.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ConfigurationLbl.Location = new Point(75, 24);
            ConfigurationLbl.Name = "ConfigurationLbl";
            ConfigurationLbl.Size = new Size(106, 21);
            ConfigurationLbl.TabIndex = 1;
            ConfigurationLbl.Text = "Configuration";
            // 
            // ApplicationLabel
            // 
            ApplicationLabel.AutoSize = true;
            ApplicationLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ApplicationLabel.Location = new Point(51, 64);
            ApplicationLabel.Name = "ApplicationLabel";
            ApplicationLabel.Size = new Size(68, 15);
            ApplicationLabel.TabIndex = 2;
            ApplicationLabel.Text = "RTMP Feed:";
            ApplicationLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // DebugRecreateDB
            // 
            DebugRecreateDB.Location = new Point(12, 506);
            DebugRecreateDB.Name = "DebugRecreateDB";
            DebugRecreateDB.Size = new Size(102, 46);
            DebugRecreateDB.TabIndex = 3;
            DebugRecreateDB.Text = "Recreate Database";
            DebugRecreateDB.UseVisualStyleBackColor = true;
            DebugRecreateDB.Click += DebugRecreateDB_Click;
            // 
            // DebugRunLoaders
            // 
            DebugRunLoaders.Location = new Point(12, 454);
            DebugRunLoaders.Name = "DebugRunLoaders";
            DebugRunLoaders.Size = new Size(102, 46);
            DebugRunLoaders.TabIndex = 4;
            DebugRunLoaders.Text = "Run Loaders";
            DebugRunLoaders.UseVisualStyleBackColor = true;
            DebugRunLoaders.Click += DebugRunLoaders_Click;
            // 
            // StartStopBtn
            // 
            StartStopBtn.Location = new Point(258, 478);
            StartStopBtn.Name = "StartStopBtn";
            StartStopBtn.Size = new Size(121, 23);
            StartStopBtn.TabIndex = 5;
            StartStopBtn.Text = "Start";
            StartStopBtn.UseVisualStyleBackColor = true;
            StartStopBtn.Click += StartStopBtn_Click;
            // 
            // PreviewPictureBox
            // 
            PreviewPictureBox.Location = new Point(462, 48);
            PreviewPictureBox.Name = "PreviewPictureBox";
            PreviewPictureBox.Size = new Size(542, 345);
            PreviewPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            PreviewPictureBox.TabIndex = 6;
            PreviewPictureBox.TabStop = false;
            // 
            // NetworkDeviceComboBox
            // 
            NetworkDeviceComboBox.FormattingEnabled = true;
            NetworkDeviceComboBox.Location = new Point(145, 90);
            NetworkDeviceComboBox.Name = "NetworkDeviceComboBox";
            NetworkDeviceComboBox.Size = new Size(183, 23);
            NetworkDeviceComboBox.TabIndex = 7;
            NetworkDeviceComboBox.DropDown += NetworkDeviceComboBox_DropDown;
            // 
            // NetworkDeviceLabel
            // 
            NetworkDeviceLabel.AutoSize = true;
            NetworkDeviceLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            NetworkDeviceLabel.Location = new Point(9, 93);
            NetworkDeviceLabel.Name = "NetworkDeviceLabel";
            NetworkDeviceLabel.Size = new Size(127, 15);
            NetworkDeviceLabel.TabIndex = 8;
            NetworkDeviceLabel.Text = "Select Network Device:";
            NetworkDeviceLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(662, 24);
            label3.Name = "label3";
            label3.Size = new Size(124, 21);
            label3.TabIndex = 9;
            label3.Text = "Capture Preview";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(462, 412);
            label4.Name = "label4";
            label4.Size = new Size(150, 15);
            label4.TabIndex = 10;
            label4.Text = "Currently Logged in Player:";
            // 
            // CurrentPlayerDisplayLbl
            // 
            CurrentPlayerDisplayLbl.AutoSize = true;
            CurrentPlayerDisplayLbl.Location = new Point(618, 412);
            CurrentPlayerDisplayLbl.Name = "CurrentPlayerDisplayLbl";
            CurrentPlayerDisplayLbl.Size = new Size(36, 15);
            CurrentPlayerDisplayLbl.TabIndex = 11;
            CurrentPlayerDisplayLbl.Text = "None";
            // 
            // SerialDeviceLabel
            // 
            SerialDeviceLabel.AutoSize = true;
            SerialDeviceLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            SerialDeviceLabel.Location = new Point(26, 122);
            SerialDeviceLabel.Name = "SerialDeviceLabel";
            SerialDeviceLabel.Size = new Size(110, 15);
            SerialDeviceLabel.TabIndex = 14;
            SerialDeviceLabel.Text = "Select Serial Device:";
            SerialDeviceLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // SerialDeviceComboBox
            // 
            SerialDeviceComboBox.FormattingEnabled = true;
            SerialDeviceComboBox.Location = new Point(145, 119);
            SerialDeviceComboBox.Name = "SerialDeviceComboBox";
            SerialDeviceComboBox.Size = new Size(183, 23);
            SerialDeviceComboBox.TabIndex = 13;
            SerialDeviceComboBox.DropDown += SerialDeviceComboBox_DropDown;
            // 
            // ProcessingPreviewPictureBox
            // 
            ProcessingPreviewPictureBox.Location = new Point(1010, 48);
            ProcessingPreviewPictureBox.Name = "ProcessingPreviewPictureBox";
            ProcessingPreviewPictureBox.Size = new Size(542, 345);
            ProcessingPreviewPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            ProcessingPreviewPictureBox.TabIndex = 15;
            ProcessingPreviewPictureBox.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(1235, 24);
            label1.Name = "label1";
            label1.Size = new Size(144, 21);
            label1.TabIndex = 16;
            label1.Text = "Processing Preview";
            // 
            // RTMPFeedTextBox
            // 
            RTMPFeedTextBox.Location = new Point(145, 61);
            RTMPFeedTextBox.Name = "RTMPFeedTextBox";
            RTMPFeedTextBox.Size = new Size(183, 23);
            RTMPFeedTextBox.TabIndex = 17;
            RTMPFeedTextBox.Text = "rtmp://127.0.0.1/live";
            // 
            // TerraSurge
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1644, 641);
            Controls.Add(RTMPFeedTextBox);
            Controls.Add(label1);
            Controls.Add(ProcessingPreviewPictureBox);
            Controls.Add(SerialDeviceLabel);
            Controls.Add(SerialDeviceComboBox);
            Controls.Add(CurrentPlayerDisplayLbl);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(NetworkDeviceLabel);
            Controls.Add(NetworkDeviceComboBox);
            Controls.Add(PreviewPictureBox);
            Controls.Add(StartStopBtn);
            Controls.Add(DebugRunLoaders);
            Controls.Add(DebugRecreateDB);
            Controls.Add(ApplicationLabel);
            Controls.Add(ConfigurationLbl);
            Name = "TerraSurge";
            Text = "Terra Surge";
            Load += TerraSurge_Load;
            ((System.ComponentModel.ISupportInitialize)PreviewPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)ProcessingPreviewPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label ConfigurationLbl;
        private Label ApplicationLabel;
        private Button DebugRecreateDB;
        private Button DebugRunLoaders;
        private Button StartStopBtn;
        private PictureBox PreviewPictureBox;
        private ComboBox NetworkDeviceComboBox;
        private Label NetworkDeviceLabel;
        private Label label3;
        private Label label4;
        private Label CurrentPlayerDisplayLbl;
        private Label SerialDeviceLabel;
        private ComboBox SerialDeviceComboBox;
        private PictureBox ProcessingPreviewPictureBox;
        private Label label1;
        private TextBox RTMPFeedTextBox;
    }
}