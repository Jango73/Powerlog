
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;

namespace PowerLog
{
    public partial class MainForm : Form
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Work

        /// <summary>
        /// Opens the log.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        private void OpenLog(String FileName)
        {
            String ShortFileName = Path.GetFileName(FileName);

            foreach (TabPage SearchPage in MainTabs.TabPages)
            {
                if (SearchPage.Name == ShortFileName) return;
            }

            TabPage Page = new TabPage();
            Page.Name = ShortFileName;
            Page.Text = ShortFileName;

            LogDisplay Display = new LogDisplay();
            Display.Dock = DockStyle.Fill;
            Display.FileName = FileName;

            Page.Controls.Add(Display);
            MainTabs.TabPages.Add(Page);

            MainTabs.SelectedTab = Page;

            AddToRecent(FileName);
        }

        #endregion

        #region Events

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Properties.Settings.Default.RecentFiles != null)
            {
                ToolStripDropDownItem Recent = GetRecentMenu();

                if (Recent != null)
                {
                    foreach (String FileName in Properties.Settings.Default.RecentFiles)
                    {
                        ToolStripMenuItem NewItem = new ToolStripMenuItem(GetDisplayFileName(FileName));
                        NewItem.Tag = FileName;
                        Recent.DropDownItems.Add(NewItem);
                    }
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Closing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            SaveRecentFiles();

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Handles the Click event of the openToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog Dlg = new OpenFileDialog();
            Dlg.Filter = "Log files (*.log)|*.log|All files (*.*)|*.*";

            if (Dlg.ShowDialog() == DialogResult.OK)
            {
                OpenLog(Dlg.FileName);
            }
        }

        /// <summary>
        /// Handles the Click event of the quitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the KeyDown event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            ((LogDisplay)MainTabs.SelectedTab.Controls[0]).HandleKeyDown(sender, e);
        }

        /// <summary>
        /// Handles the KeyDown event of the MainTabs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void MainTabs_KeyDown(object sender, KeyEventArgs e)
        {
            if (MainTabs.SelectedTab != null)
            {
                ((LogDisplay)MainTabs.SelectedTab.Controls[0]).HandleKeyDown(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            String[] Files = (String[])e.Data.GetData(DataFormats.FileDrop);

            foreach (String File in Files)
            {
                OpenLog(File);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        /// <summary>
        /// Handles the DropDownItemClicked event of the RecentMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.ToolStripItemClickedEventArgs"/> instance containing the event data.</param>
        private void RecentMenu_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            String FileName = e.ClickedItem.Tag.ToString();

            if (File.Exists(FileName))
            {
                OpenLog(FileName);
            }
            else
            {
                MainStatusText.Text = "File does not exist, removing from recent files...";

                ToolStripDropDownItem Recent = GetRecentMenu();

                if (Recent != null)
                {
                    foreach (ToolStripDropDownItem RecentItem in Recent.DropDownItems)
                    {
                        if (RecentItem.Tag.ToString() == FileName)
                        {
                            Recent.DropDownItems.Remove(RecentItem);
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Wheels the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="Distance">The distance.</param>
        public void Wheel(object sender, int Distance)
        {
            ListBox Box = (ListBox)sender;

            if (Distance < 0)
            {
                MainTabs_KeyDown(sender, new KeyEventArgs(Keys.Down));
            }
        }

        #endregion

        #region RecentFiles

        /// <summary>
        /// Adds to recent.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        private void AddToRecent(String FileName)
        {
            int MaxRecentFiles = Convert.ToInt32(Properties.Settings.Default.MaxRecentFiles);

            ToolStripDropDownItem Recent = GetRecentMenu();

            if (Recent != null)
            {
                if (Recent.DropDownItems.Count >= MaxRecentFiles)
                {
                    Recent.DropDownItems.RemoveAt(0);
                }

                Boolean Found = false;

                foreach (ToolStripDropDownItem RecentItem in Recent.DropDownItems)
                {
                    if (RecentItem.Tag.ToString() == FileName)
                    {
                        Found = true;
                        break;
                    }
                }

                if (Found == false)
                {
                    ToolStripMenuItem NewItem = new ToolStripMenuItem(GetDisplayFileName(FileName));
                    NewItem.Tag = FileName;
                    Recent.DropDownItems.Add(NewItem);
                }
            }
        }

        /// <summary>
        /// Gets the display name of the file.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <returns></returns>
        private String GetDisplayFileName(String FileName)
        {
            String PathComp = Path.GetDirectoryName(FileName);
            String FileComp = Path.GetFileName(FileName);

            Boolean CutDone = false;

            while (PathComp.Length + FileComp.Length > 80)
            {
                int Index = PathComp.IndexOf('\\');

                if (Index > 0)
                {
                    PathComp = PathComp.Substring(0, Index);
                    CutDone = true;
                }
                else
                {
                    break;
                }
            }

            if (CutDone == true)
            {
                return PathComp + "\\..\\" + FileComp;
            }

            return FileName;
        }

        /// <summary>
        /// Saves the recent files.
        /// </summary>
        private void SaveRecentFiles()
        {
            if (Properties.Settings.Default.RecentFiles == null)
            {
                Properties.Settings.Default.RecentFiles = new System.Collections.Specialized.StringCollection();
            }

            Properties.Settings.Default.RecentFiles.Clear();

            ToolStripDropDownItem Recent = GetRecentMenu();

            if (Recent != null)
            {
                foreach (ToolStripDropDownItem RecentItem in Recent.DropDownItems)
                {
                    if (Properties.Settings.Default.RecentFiles.Contains(RecentItem.Tag.ToString()) == false)
                    {
                        Properties.Settings.Default.RecentFiles.Add(RecentItem.Tag.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Gets the recent menu.
        /// </summary>
        /// <returns></returns>
        private ToolStripDropDownItem GetRecentMenu()
        {
            foreach (ToolStripMenuItem Item in MainMenu.Items)
            {
                if (Item.Name == "FileMenu")
                {
                    foreach (ToolStripItem TestDropItem in Item.DropDownItems)
                    {
                        if (TestDropItem is ToolStripDropDownItem)
                        {
                            ToolStripDropDownItem DropItem = (ToolStripDropDownItem)TestDropItem;

                            if (DropItem.Name == "RecentMenu") return DropItem;
                        }
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
