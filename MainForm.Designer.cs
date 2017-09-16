namespace PowerLog
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RecentMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainStatus = new System.Windows.Forms.StatusStrip();
            this.MainStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainTabs = new System.Windows.Forms.TabControl();
            this.MainMenu.SuspendLayout();
            this.MainStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(984, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.RecentMenu,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(37, 20);
            this.FileMenu.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // RecentMenu
            // 
            this.RecentMenu.Name = "RecentMenu";
            this.RecentMenu.Size = new System.Drawing.Size(152, 22);
            this.RecentMenu.Text = "Recent";
            this.RecentMenu.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.RecentMenu_DropDownItemClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // MainStatus
            // 
            this.MainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainStatusText});
            this.MainStatus.Location = new System.Drawing.Point(0, 542);
            this.MainStatus.Name = "MainStatus";
            this.MainStatus.Size = new System.Drawing.Size(984, 22);
            this.MainStatus.TabIndex = 1;
            this.MainStatus.Text = "MainStatus";
            // 
            // MainStatusText
            // 
            this.MainStatusText.AutoSize = false;
            this.MainStatusText.Name = "MainStatusText";
            this.MainStatusText.Size = new System.Drawing.Size(400, 17);
            this.MainStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainTabs
            // 
            this.MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabs.Location = new System.Drawing.Point(0, 24);
            this.MainTabs.Name = "MainTabs";
            this.MainTabs.SelectedIndex = 0;
            this.MainTabs.Size = new System.Drawing.Size(984, 518);
            this.MainTabs.TabIndex = 2;
            this.MainTabs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainTabs_KeyDown);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 564);
            this.Controls.Add(this.MainTabs);
            this.Controls.Add(this.MainStatus);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PowerLog";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.MainStatus.ResumeLayout(false);
            this.MainStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.StatusStrip MainStatus;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.TabControl MainTabs;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem RecentMenu;
        private System.Windows.Forms.ToolStripStatusLabel MainStatusText;
    }
}

