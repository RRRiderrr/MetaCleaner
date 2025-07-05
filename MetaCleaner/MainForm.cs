using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace MetaCleaner
{
    public partial class MainForm : Form
    {
        private readonly Random rnd = new Random();
        private Image _dropIcon;
        private bool isExiting = false;

        class Config
        {
            public bool AutoCleanEnabled { get; set; }
            public List<string> Folders { get; set; }
        }

        private Config config;
        private readonly string configPath = Path.Combine(Application.StartupPath, "config.json");
        private readonly List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();

        public MainForm()
        {
            InitializeComponent();

            // Load arrow icon
            var icoPath = Path.Combine(Application.StartupPath, "Resources", "UploadArrow.png");
            if (File.Exists(icoPath))
                _dropIcon = Image.FromFile(icoPath);

            // Load configuration
            LoadConfig();
            chkEnableAutoClean.Checked = config.AutoCleanEnabled;
            lstFolders.Items.AddRange(config.Folders.ToArray());
            btnApply.Enabled = false;

            // Set up tray icon double-click or load behavior
            notifyIcon.Visible = false;

            // Enable watchers if already enabled
            if (config.AutoCleanEnabled)
                EnableAutoClean();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // If auto-clean is enabled, start minimized to tray
            if (config.AutoCleanEnabled)
            {
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
                notifyIcon.Visible = true;
            }
        }

        private void LoadConfig()
        {
            if (File.Exists(configPath))
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath));
            else
                config = new Config { AutoCleanEnabled = false, Folders = new List<string>() };
        }

        private void SaveConfig()
        {
            File.WriteAllText(configPath, JsonConvert.SerializeObject(config, Formatting.Indented));
        }

        private void chkEnableAutoClean_CheckedChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = btnRemove.Enabled = chkEnableAutoClean.Checked;
            btnApply.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK && !lstFolders.Items.Contains(dlg.SelectedPath))
            {
                lstFolders.Items.Add(dlg.SelectedPath);
                btnApply.Enabled = true;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstFolders.SelectedItem != null)
            {
                lstFolders.Items.Remove(lstFolders.SelectedItem);
                btnApply.Enabled = true;
            }
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            // 1. Save config
            config.AutoCleanEnabled = chkEnableAutoClean.Checked;
            config.Folders = lstFolders.Items.Cast<string>().ToList();
            SaveConfig();

            // 2. Enable or disable auto-clean
            if (config.AutoCleanEnabled)
            {
                EnableAutoClean();
                // Then run initial clean in background
                await Task.Run(InitialClean);
            }
            else
            {
                DisableAutoClean();
            }

            btnApply.Enabled = false;
        }

        private void EnableAutoClean()
        {
            // Add to startup
            using var key = Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Run", true);
            key.SetValue("MetaCleaner", $"\"{Application.ExecutablePath}\"");

            // Reset watchers
            foreach (var w in watchers) w.Dispose();
            watchers.Clear();

            // Watch each folder
            foreach (var dir in config.Folders)
            {
                if (!Directory.Exists(dir)) continue;
                var w = new FileSystemWatcher(dir)
                {
                    IncludeSubdirectories = true,
                    EnableRaisingEvents = true,
                    NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
                };
                w.Created += (s, e) => CleanReplace(e.FullPath);
                w.Changed += (s, e) => CleanReplace(e.FullPath);
                watchers.Add(w);
            }
        }

        private void DisableAutoClean()
        {
            // Remove from startup
            using var key = Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (key.GetValue("MetaCleaner") != null)
                key.DeleteValue("MetaCleaner");

            // Dispose watchers
            foreach (var w in watchers) w.Dispose();
            watchers.Clear();
        }

        private void InitialClean()
        {
            var all = config.Folders
                .Where(Directory.Exists)
                .SelectMany(d => Directory.GetFiles(d, "*", SearchOption.AllDirectories))
                .ToArray();

            int total = all.Length;
            if (total == 0) return;

            Invoke((Action)(() =>
            {
                pbAutoProgress.Visible = true;
                pbAutoProgress.Minimum = 0;
                pbAutoProgress.Maximum = total;
                pbAutoProgress.Value = 0;
                lblAutoStatus.Visible = true;
                lblAutoStatus.Text = "Starting initial clean...";
            }));

            for (int i = 0; i < total; i++)
            {
                try { CleanReplace(all[i]); } catch { }
                int done = i + 1;
                Invoke((Action)(() =>
                {
                    pbAutoProgress.Value = done;
                    lblAutoStatus.Text = $"Cleaning {Path.GetFileName(all[i])} ({done}/{total})";
                }));
            }

            Invoke((Action)(() => lblAutoStatus.Text = "Initial clean completed."));
        }

        private void CleanReplace(string path)
        {
            if (!File.Exists(path)) return;
            if (Path.GetFileName(path).EndsWith("_exiftool_tmp", StringComparison.OrdinalIgnoreCase))
                return;

            var ext = Path.GetExtension(path).ToLowerInvariant();
            if (ext == ".docx" || ext == ".xlsx")
            {
                CleanOffice(path);
            }
            else
            {
                try
                {
                    using var p = Process.Start(new ProcessStartInfo(
                        Program.ExifPath,
                        $"-all= -overwrite_original \"{path}\"")
                    { CreateNoWindow = true, UseShellExecute = false });
                    p.WaitForExit();
                }
                catch { }
            }

            try { SetRandomTimestamps(path); } catch { }
        }

        private void CleanSaveAs(string input, string output)
        {
            var ext = Path.GetExtension(input).ToLowerInvariant();
            bool ok = false;

            if (ext == ".docx" || ext == ".xlsx")
            {
                File.Copy(input, output, true);
                CleanOffice(output);
                ok = true;
            }
            else
            {
                try
                {
                    using var p = Process.Start(new ProcessStartInfo(
                        Program.ExifPath,
                        $"-all= -o \"{output}\" \"{input}\"")
                    { CreateNoWindow = true, UseShellExecute = false });
                    p.WaitForExit();
                    if (p.ExitCode == 0 && File.Exists(output))
                        ok = true;
                }
                catch { }
            }

            if (!ok)
                File.Copy(input, output, true);

            try { SetRandomTimestamps(output); } catch { }
        }

        private void CleanOffice(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            try
            {
                using var fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                if (ext == ".docx")
                {
                    using var doc = WordprocessingDocument.Open(fs, true);
                    var p = doc.PackageProperties;
                    p.Creator = p.Title = p.Subject = p.Keywords =
                    p.Description = p.LastModifiedBy = p.Category =
                    p.ContentStatus = p.Version = "";
                    if (doc.CustomFilePropertiesPart != null)
                        doc.DeletePart(doc.CustomFilePropertiesPart);
                }
                else // .xlsx
                {
                    using var doc = SpreadsheetDocument.Open(fs, true);
                    var p = doc.PackageProperties;
                    p.Creator = p.Title = p.Subject = p.Keywords =
                    p.Description = p.LastModifiedBy = p.Category =
                    p.ContentStatus = p.Version = "";
                    if (doc.CustomFilePropertiesPart != null)
                        doc.DeletePart(doc.CustomFilePropertiesPart);
                }
            }
            catch (IOException) { }
            catch { }
        }

        private void SetRandomTimestamps(string path)
        {
            if (!File.Exists(path)) return;
            var start = new DateTime(2000, 1, 1);
            var span = (DateTime.Now - start).TotalSeconds;
            var rand = start.AddSeconds(rnd.NextDouble() * span);
            try { File.SetCreationTime(path, rand); } catch { }
            try { File.SetLastWriteTime(path, rand.AddSeconds(rnd.Next(0, 3600))); } catch { }
        }

        // ------ UI Drag & Drop for Clean tab ------

        private void PanelDrop_Paint(object sender, PaintEventArgs e)
        {
            var panel = (Panel)sender;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int W = panel.Width, H = panel.Height;
            int frameW = W / 2, frameH = H / 3;
            int frameX = (W - frameW) / 2, frameY = (H - frameH) / 2 - 20;

            using var pen = new Pen(Color.Gray) { DashPattern = new float[] { 4, 4 }, Width = 2 };
            g.DrawRectangle(pen, frameX, frameY, frameW, frameH);

            if (_dropIcon != null)
            {
                int maxW = (int)(frameW * 0.4), maxH = (int)(frameH * 0.4);
                float s = Math.Min((float)maxW / _dropIcon.Width, (float)maxH / _dropIcon.Height);
                int iW = (int)(_dropIcon.Width * s), iH = (int)(_dropIcon.Height * s);
                int iX = frameX + (frameW - iW) / 2, iY = frameY + (frameH - iH) / 2;
                g.DrawImage(_dropIcon, new Rectangle(iX, iY, iW, iH));
            }

            const string txt = "Choose a file or drag it here.";
            using var fnt = new Font("Segoe UI", 11);
            using var br = new SolidBrush(Color.Gray);
            var sz = g.MeasureString(txt, fnt);
            g.DrawString(txt, fnt, br, (W - sz.Width) / 2, frameY + frameH + 10);
        }

        private void PanelDrop_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void PanelCleanDrop_DragDrop(object sender, DragEventArgs e)
        {
            ProcessFileBatch((string[])e.Data.GetData(DataFormats.FileDrop));
        }

        private void PanelCleanDrop_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog { Multiselect = true, Filter = "All files (*.*)|*.*" };
            if (dlg.ShowDialog() == DialogResult.OK)
                ProcessFileBatch(dlg.FileNames);
        }

        private void ProcessFileBatch(string[] files)
        {
            panelCleanDrop.Enabled = false;
            pbFileProgress.Visible = true;
            pbFileProgress.Minimum = 0;
            pbFileProgress.Maximum = files.Length;
            pbFileProgress.Value = 0;
            foreach (var f in files)
            {
                CleanReplace(f);
                pbFileProgress.Value++;
                Application.DoEvents();
            }
            pbFileProgress.Visible = false;
            panelCleanDrop.Enabled = true;
        }

        // ------ Read tab ------

        private void PanelReadDrop_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0) ReadMetadata(files[0]);
        }

        private void PanelReadDrop_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog { Multiselect = false, Filter = "All files (*.*)|*.*" };
            if (dlg.ShowDialog() == DialogResult.OK) ReadMetadata(dlg.FileName);
        }

        private void ReadMetadata(string file)
        {
            try
            {
                if (!File.Exists(Program.ExifPath))
                {
                    txtMetadata.Text = $"ExifTool not found at:\r\n{Program.ExifPath}";
                    return;
                }
                var psi = new ProcessStartInfo(Program.ExifPath, $"-charset utf8 -s -G \"{file}\"")
                {
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                using var p = Process.Start(psi);
                string outp = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                txtMetadata.Text = string.IsNullOrWhiteSpace(outp) ? "No metadata found." : outp;
            }
            catch (Exception ex)
            {
                txtMetadata.Text = "Error reading metadata:\r\n" + ex.Message;
            }
        }

        private void HandleFile(string file)
        {
            if (config.AutoCleanEnabled)
            {
                CleanReplace(file);
                return;
            }
            var res = MessageBox.Show(
                "Yes → Clean and Save As\nNo → Clean and Replace",
                "MetaCleaner",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                using var dlg = new SaveFileDialog { Filter = "All files (*.*)|*.*", FileName = Path.GetFileName(file) };
                if (dlg.ShowDialog() == DialogResult.OK)
                    CleanSaveAs(file, dlg.FileName);
            }
            else if (res == DialogResult.No)
            {
                CleanReplace(file);
            }
        }

        // ------ Tray & Closing ------

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (!isExiting && config.AutoCleanEnabled && WindowState == FormWindowState.Minimized)
            {
                notifyIcon.Visible = true;
                Hide();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isExiting && config.AutoCleanEnabled)
            {
                e.Cancel = true;
                notifyIcon.Visible = true;
                Hide();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isExiting = true;
            notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
