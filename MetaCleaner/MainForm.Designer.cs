namespace MetaCleaner
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabAutomation;
        private System.Windows.Forms.TabPage tabClean;
        private System.Windows.Forms.TabPage tabRead;

        // Automation controls
        private System.Windows.Forms.CheckBox chkEnableAutoClean;
        private System.Windows.Forms.ListBox lstFolders;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ProgressBar pbAutoProgress;
        private System.Windows.Forms.Label lblAutoStatus;

        // Clean file metadata
        private System.Windows.Forms.Panel panelCleanDrop;
        private System.Windows.Forms.ProgressBar pbFileProgress;

        // Read file metadata
        private System.Windows.Forms.Panel panelReadDrop;
        private System.Windows.Forms.TextBox txtMetadata;

        // Tray
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

        // Footer
        private System.Windows.Forms.Label lblMadeBy;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));

            // Tabs
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabAutomation = new System.Windows.Forms.TabPage();
            this.tabClean = new System.Windows.Forms.TabPage();
            this.tabRead = new System.Windows.Forms.TabPage();

            // Automation
            this.chkEnableAutoClean = new System.Windows.Forms.CheckBox();
            this.lstFolders = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.pbAutoProgress = new System.Windows.Forms.ProgressBar();
            this.lblAutoStatus = new System.Windows.Forms.Label();

            // Clean
            this.panelCleanDrop = new System.Windows.Forms.Panel();
            this.pbFileProgress = new System.Windows.Forms.ProgressBar();

            // Read
            this.panelReadDrop = new System.Windows.Forms.Panel();
            this.txtMetadata = new System.Windows.Forms.TextBox();

            // Tray
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            // Footer
            this.lblMadeBy = new System.Windows.Forms.Label();

            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabAutomation);
            this.tabControl.Controls.Add(this.tabClean);
            this.tabControl.Controls.Add(this.tabRead);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 450);

            // 
            // tabAutomation
            // 
            this.tabAutomation.Controls.Add(this.lblAutoStatus);
            this.tabAutomation.Controls.Add(this.pbAutoProgress);
            this.tabAutomation.Controls.Add(this.chkEnableAutoClean);
            this.tabAutomation.Controls.Add(this.lstFolders);
            this.tabAutomation.Controls.Add(this.btnAdd);
            this.tabAutomation.Controls.Add(this.btnRemove);
            this.tabAutomation.Controls.Add(this.btnApply);
            this.tabAutomation.Location = new System.Drawing.Point(4, 22);
            this.tabAutomation.Name = "tabAutomation";
            this.tabAutomation.Padding = new System.Windows.Forms.Padding(10);
            this.tabAutomation.Size = new System.Drawing.Size(792, 424);
            this.tabAutomation.TabIndex = 0;
            this.tabAutomation.Text = "Automation";
            this.tabAutomation.UseVisualStyleBackColor = true;

            // 
            // chkEnableAutoClean
            // 
            this.chkEnableAutoClean.AutoSize = true;
            this.chkEnableAutoClean.Location = new System.Drawing.Point(20, 20);
            this.chkEnableAutoClean.Name = "chkEnableAutoClean";
            this.chkEnableAutoClean.Size = new System.Drawing.Size(160, 17);
            this.chkEnableAutoClean.TabIndex = 0;
            this.chkEnableAutoClean.Text = "Enable auto-clean folders";
            this.chkEnableAutoClean.CheckedChanged += new System.EventHandler(this.chkEnableAutoClean_CheckedChanged);

            // 
            // lstFolders
            // 
            this.lstFolders.FormattingEnabled = true;
            this.lstFolders.Location = new System.Drawing.Point(20, 50);
            this.lstFolders.Name = "lstFolders";
            this.lstFolders.Size = new System.Drawing.Size(500, 150);
            this.lstFolders.TabIndex = 1;

            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(530, 50);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // 
            // btnRemove
            // 
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(530, 80);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);

            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(530, 110);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);

            // 
            // pbAutoProgress
            // 
            this.pbAutoProgress.Location = new System.Drawing.Point(20, 190);
            this.pbAutoProgress.Name = "pbAutoProgress";
            this.pbAutoProgress.Size = new System.Drawing.Size(500, 23);
            this.pbAutoProgress.TabIndex = 5;
            this.pbAutoProgress.Visible = false;

            // 
            // lblAutoStatus
            // 
            this.lblAutoStatus.AutoSize = true;
            this.lblAutoStatus.Location = new System.Drawing.Point(20, 220);
            this.lblAutoStatus.Name = "lblAutoStatus";
            this.lblAutoStatus.Size = new System.Drawing.Size(0, 13);
            this.lblAutoStatus.TabIndex = 6;
            this.lblAutoStatus.Visible = false;

            // 
            // tabClean
            // 
            this.tabClean.Controls.Add(this.panelCleanDrop);
            this.tabClean.Controls.Add(this.pbFileProgress);
            this.tabClean.Location = new System.Drawing.Point(4, 22);
            this.tabClean.Name = "tabClean";
            this.tabClean.Padding = new System.Windows.Forms.Padding(10);
            this.tabClean.Size = new System.Drawing.Size(792, 424);
            this.tabClean.TabIndex = 1;
            this.tabClean.Text = "Clean file metadata";
            this.tabClean.UseVisualStyleBackColor = true;

            // 
            // panelCleanDrop
            // 
            this.panelCleanDrop.AllowDrop = true;
            this.panelCleanDrop.BackColor = System.Drawing.Color.FromArgb(230, 240, 245);
            this.panelCleanDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCleanDrop.Location = new System.Drawing.Point(10, 10);
            this.panelCleanDrop.Name = "panelCleanDrop";
            this.panelCleanDrop.Size = new System.Drawing.Size(772, 384);
            this.panelCleanDrop.TabIndex = 0;
            this.panelCleanDrop.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelDrop_Paint);
            this.panelCleanDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.PanelDrop_DragEnter);
            this.panelCleanDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.PanelCleanDrop_DragDrop);
            this.panelCleanDrop.Click += new System.EventHandler(this.PanelCleanDrop_Click);

            // 
            // pbFileProgress
            // 
            this.pbFileProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbFileProgress.Location = new System.Drawing.Point(10, 394);
            this.pbFileProgress.Name = "pbFileProgress";
            this.pbFileProgress.Size = new System.Drawing.Size(772, 20);
            this.pbFileProgress.TabIndex = 1;
            this.pbFileProgress.Visible = false;

            // 
            // tabRead
            // 
            this.tabRead.Controls.Add(this.txtMetadata);
            this.tabRead.Controls.Add(this.panelReadDrop);
            this.tabRead.Location = new System.Drawing.Point(4, 22);
            this.tabRead.Name = "tabRead";
            this.tabRead.Padding = new System.Windows.Forms.Padding(10);
            this.tabRead.Size = new System.Drawing.Size(792, 424);
            this.tabRead.TabIndex = 2;
            this.tabRead.Text = "Read file metadata";
            this.tabRead.UseVisualStyleBackColor = true;

            // 
            // panelReadDrop
            // 
            this.panelReadDrop.AllowDrop = true;
            this.panelReadDrop.BackColor = System.Drawing.Color.FromArgb(230, 240, 245);
            this.panelReadDrop.Location = new System.Drawing.Point(10, 10);
            this.panelReadDrop.Name = "panelReadDrop";
            this.panelReadDrop.Size = new System.Drawing.Size(380, 404);
            this.panelReadDrop.TabIndex = 0;
            this.panelReadDrop.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelDrop_Paint);
            this.panelReadDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.PanelDrop_DragEnter);
            this.panelReadDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.PanelReadDrop_DragDrop);
            this.panelReadDrop.Click += new System.EventHandler(this.PanelReadDrop_Click);

            // 
            // txtMetadata
            // 
            this.txtMetadata.Location = new System.Drawing.Point(400, 10);
            this.txtMetadata.Multiline = true;
            this.txtMetadata.Name = "txtMetadata";
            this.txtMetadata.ReadOnly = true;
            this.txtMetadata.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMetadata.Size = new System.Drawing.Size(382, 404);
            this.txtMetadata.TabIndex = 1;

            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.ContextMenuStrip = this.contextMenu;
            this.notifyIcon.Text = "MetaCleaner";

            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.openToolStripMenuItem,
                this.exitToolStripMenuItem
            });
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(104, 48);

            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);

            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);

            // 
            // lblMadeBy
            // 
            this.lblMadeBy.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.lblMadeBy.AutoSize = true;
            this.lblMadeBy.Location = new System.Drawing.Point(700, 430);
            this.lblMadeBy.Name = "lblMadeBy";
            this.lblMadeBy.Size = new System.Drawing.Size(88, 13);
            this.lblMadeBy.TabIndex = 0;
            this.lblMadeBy.Text = "Made by Rider, v1.0";

            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblMadeBy);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MetaCleaner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.MainForm_Resize);

            this.tabControl.ResumeLayout(false);
            this.tabAutomation.ResumeLayout(false);
            this.tabAutomation.PerformLayout();
            this.tabClean.ResumeLayout(false);
            this.tabClean.PerformLayout();
            this.tabRead.ResumeLayout(false);
            this.tabRead.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
