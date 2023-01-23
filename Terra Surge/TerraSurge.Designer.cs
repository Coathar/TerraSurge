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
            ApplicationComboBox = new ComboBox();
            ConfigurationLbl = new Label();
            label1 = new Label();
            DebugRecreateDB = new Button();
            DebugRunLoaders = new Button();
            StartStopBtn = new Button();
            PreviewPictureBox = new PictureBox();
            NetworkDeviceComboBox = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            CurrentPlayerDisplayLbl = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)PreviewPictureBox).BeginInit();
            SuspendLayout();
            // 
            // ApplicationComboBox
            // 
            ApplicationComboBox.FormattingEnabled = true;
            ApplicationComboBox.Location = new Point(145, 61);
            ApplicationComboBox.Name = "ApplicationComboBox";
            ApplicationComboBox.Size = new Size(183, 23);
            ApplicationComboBox.TabIndex = 0;
            ApplicationComboBox.DropDown += ApplicationComboBox_DropDown;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(31, 64);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 2;
            label1.Text = "Select Application:";
            label1.TextAlign = ContentAlignment.MiddleRight;
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(9, 93);
            label2.Name = "label2";
            label2.Size = new Size(127, 15);
            label2.TabIndex = 8;
            label2.Text = "Select Network Device:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(706, 24);
            label3.Name = "label3";
            label3.Size = new Size(65, 21);
            label3.TabIndex = 9;
            label3.Text = "Preview";
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
            // button1
            // 
            button1.Location = new Point(125, 252);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 12;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // TerraSurge
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1025, 634);
            Controls.Add(button1);
            Controls.Add(CurrentPlayerDisplayLbl);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(NetworkDeviceComboBox);
            Controls.Add(PreviewPictureBox);
            Controls.Add(StartStopBtn);
            Controls.Add(DebugRunLoaders);
            Controls.Add(DebugRecreateDB);
            Controls.Add(label1);
            Controls.Add(ConfigurationLbl);
            Controls.Add(ApplicationComboBox);
            Name = "TerraSurge";
            Text = "Terra Surge";
            Load += TerraSurge_Load;
            ((System.ComponentModel.ISupportInitialize)PreviewPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox ApplicationComboBox;
        private Label ConfigurationLbl;
        private Label label1;
        private Button DebugRecreateDB;
        private Button DebugRunLoaders;
        private Button StartStopBtn;
        private PictureBox PreviewPictureBox;
        private ComboBox NetworkDeviceComboBox;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label CurrentPlayerDisplayLbl;
        private Button button1;
    }
}