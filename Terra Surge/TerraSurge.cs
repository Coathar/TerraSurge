using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Runtime.InteropServices;
using AForge.Video.DirectShow;
using DirectShowLib;
using IronOcr;
using OpenCvSharp;
using SharpPcap;
using TerraSurge.Game;
using TerraSurge.ScreenWatcher;
using TerraSurgeShared;
using TerraSurgeShared.Models;

namespace TerraSurge
{
    public partial class TerraSurge : Form
    {
        private bool ScreenWatcherRunning { get; set; } = false;

        private RTMPStreamManager WebcamManager { get; set; }

        public GameMonitor GameMonitor { get; set; }

        public SRTServer SRTServer { get; set; }

        public CancellationTokenSource CTS { get; set; }

        public AppDbContext DBContext { get; set; }
        public TemplateManager TemplateManager { get; set; }

        private Dictionary<string, string> CachedCameras { get; set; } = new Dictionary<string, string>();

        private Dictionary<string, ICaptureDevice> CachedNetworkDevices { get; set; } = new Dictionary<string, ICaptureDevice>();

        public TerraSurge()
        {
            InitializeComponent();
        }

        private void TerraSurge_Load(object sender, EventArgs e)
        {
            DBContext = new AppDbContext();
            TemplateManager = new TemplateManager();
            TemplateManager.LoadTemplates();
        }

        private void DebugRunLoaders_Click(object sender, EventArgs e)
        {
            DBContext.RunLoaders();
        }

        private void DebugRecreateDB_Click(object sender, EventArgs e)
        {
            DBContext.Database.EnsureDeleted();
            DBContext.Database.EnsureCreated();
        }

        private void StartStopBtn_Click(object sender, EventArgs e)
        {
            if (!ScreenWatcherRunning)
            {
                CTS = new CancellationTokenSource();

                if (!string.IsNullOrEmpty(RTMPFeedTextBox.Text)
                {
                    WebcamManager = new WebcamManager(RTMPFeedTextBox.Text, this);
                    WebcamManager.Start();

                    GameMonitor = new GameMonitor(this, CachedNetworkDevices[NetworkDeviceComboBox.Text]);
                    GameMonitor.Start();
                }
                else
                {
                    MessageBox.Show("Invalid process selected. Please try selecting the process again.");
                    return;
                }
            }
            else
            {
                CTS.Cancel();
            }


            ScreenWatcherRunning = !ScreenWatcherRunning;
            StartStopBtn.Text = ScreenWatcherRunning ? "Stop" : "Start";
        }

        private void CameraComboBox_DropDown(object sender, EventArgs e)
        {
            //Process[] processes = Process.GetProcesses();

            //foreach (Process p in processes)
            //{
            //    if (!string.IsNullOrEmpty(p.MainWindowTitle))
            //    {
            //        string fileName = string.Empty;

            //        // Assemble file name and if it doesn't exist, skip this process.
            //        try
            //        {
            //            fileName = $" - ({Path.GetFileName(p?.MainModule?.FileName)})";
            //        }
            //        catch (Win32Exception)
            //        { }

            //        // Append window title with file name.
            //        string displayName = $"{p.MainWindowTitle}{fileName}";

            //        // Removes duplicate programs that are marked with a (1). Weird workaround, will need to redo later
            //        if (CachedCameras.ContainsKey(displayName))
            //        {
            //            displayName += "(1)";
            //        }

            //        toShow.Add(displayName);
            //        CachedCameras.Add(displayName, p.ProcessName);
            //    }
            //}

            CameraComboBox.Items.Clear();
            CameraComboBox.Items.AddRange(DsDevice.GetDevicesOfCat(DirectShowLib.FilterCategory.VideoInputDevice).Select(x => x.Name).ToArray());

            int maxWidth = 0, temp = 0;

            foreach (var obj in CameraComboBox.Items)
            {
                temp = TextRenderer.MeasureText(obj.ToString(), CameraComboBox.Font).Width;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }

            CameraComboBox.DropDownWidth = maxWidth;
        }

        public void SetCapturePreview(Bitmap bitmap)
        {
            if (PreviewPictureBox.InvokeRequired)
            {
                PreviewPictureBox.Invoke(SetCapturePreview, bitmap);
            }
            else
            {
                if (PreviewPictureBox.Image != null)
                    PreviewPictureBox.Image.Dispose();

                PreviewPictureBox.Image = bitmap;
            }
        }

        public void SetProcessingPreview(Bitmap bitmap)
        {
            if (ProcessingPreviewPictureBox.InvokeRequired)
            {
                ProcessingPreviewPictureBox.Invoke(SetProcessingPreview, bitmap);
            }
            else
            {
                if (ProcessingPreviewPictureBox.Image != null)
                    ProcessingPreviewPictureBox.Image.Dispose();

                ProcessingPreviewPictureBox.Image = bitmap;
            }
        }

        public void SetCurrentPlayer(BNetPlayer player)
        {
            if (CurrentPlayerDisplayLbl.InvokeRequired)
            {
                CurrentPlayerDisplayLbl.Invoke(SetCurrentPlayer, player);
            }
            else
            {
                CurrentPlayerDisplayLbl.Text = $"{player.DisplayName}#{player.Tag} ({player.AccountID})";
            }
        }

        private void NetworkDeviceComboBox_DropDown(object sender, EventArgs e)
        {
            CaptureDeviceList networkDevices = CaptureDeviceList.Instance;

            if (networkDevices.Count == 0)
            {
                MessageBox.Show(text: "No devices found! Make sure WinPcap is installed.");
                return;
            }

            List<string> toShow = new List<string>();

            foreach (ICaptureDevice device in networkDevices)
            {
                toShow.Add($"{device.Description}");
                CachedNetworkDevices[device.Description] = device;
            }

            NetworkDeviceComboBox.Items.Clear();
            NetworkDeviceComboBox.Items.AddRange(toShow.OrderBy(s => s).ToArray());

            int maxWidth = 0, temp = 0;

            foreach (var obj in NetworkDeviceComboBox.Items)
            {
                temp = TextRenderer.MeasureText(obj.ToString(), NetworkDeviceComboBox.Font).Width;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }

            NetworkDeviceComboBox.DropDownWidth = maxWidth;
        }

        private void SerialDeviceComboBox_DropDown(object sender, EventArgs e)
        {
            SerialDeviceComboBox.Items.Clear();

            string[] ports = SerialPort.GetPortNames();

            if (ports.Length == 0)
            {
                MessageBox.Show(text: "No devices found");
                return;
            }

            SerialDeviceComboBox.Items.AddRange(ports.OrderBy(s => s).ToArray());
            int maxWidth = 0, temp = 0;

            foreach (var obj in SerialDeviceComboBox.Items)
            {
                temp = TextRenderer.MeasureText(obj.ToString(), NetworkDeviceComboBox.Font).Width;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }

            SerialDeviceComboBox.DropDownWidth = maxWidth;
        }

        public string GetSerialPort()
        {
            string toReturn = string.Empty;

            Invoke((MethodInvoker)delegate ()
            {
                toReturn = (string)SerialDeviceComboBox.SelectedItem;
            });

            return toReturn;
        }
    }
}