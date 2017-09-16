namespace PowerLog
{
    partial class LogDisplay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BuilderProgress = new System.Windows.Forms.ProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.PageLog = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.FilterLabel = new System.Windows.Forms.Label();
            this.Filter = new System.Windows.Forms.TextBox();
            this.TailFile = new System.Windows.Forms.CheckBox();
            this.BtRefresh = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.LogTree = new System.Windows.Forms.TreeView();
            this.PagePlot = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.BtRefresh2 = new System.Windows.Forms.Button();
            this.BtClearMarkers = new System.Windows.Forms.Button();
            this.LogText = new PowerLog.SubListBox();
            this.ThePlotter = new PowerLog.Plotter();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.PageLog.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.PagePlot.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.BuilderProgress);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 2;
            // 
            // BuilderProgress
            // 
            this.BuilderProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BuilderProgress.Location = new System.Drawing.Point(0, 0);
            this.BuilderProgress.Name = "BuilderProgress";
            this.BuilderProgress.Size = new System.Drawing.Size(800, 25);
            this.BuilderProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BuilderProgress.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.PageLog);
            this.tabControl1.Controls.Add(this.PagePlot);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 571);
            this.tabControl1.TabIndex = 1;
            // 
            // PageLog
            // 
            this.PageLog.Controls.Add(this.splitContainer2);
            this.PageLog.Location = new System.Drawing.Point(4, 22);
            this.PageLog.Name = "PageLog";
            this.PageLog.Padding = new System.Windows.Forms.Padding(3);
            this.PageLog.Size = new System.Drawing.Size(792, 545);
            this.PageLog.TabIndex = 0;
            this.PageLog.Text = "Log";
            this.PageLog.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer2.Size = new System.Drawing.Size(786, 539);
            this.splitContainer2.SplitterDistance = 30;
            this.splitContainer2.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.FilterLabel);
            this.flowLayoutPanel1.Controls.Add(this.Filter);
            this.flowLayoutPanel1.Controls.Add(this.TailFile);
            this.flowLayoutPanel1.Controls.Add(this.BtRefresh);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(786, 30);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // FilterLabel
            // 
            this.FilterLabel.Location = new System.Drawing.Point(3, 0);
            this.FilterLabel.Name = "FilterLabel";
            this.FilterLabel.Size = new System.Drawing.Size(80, 23);
            this.FilterLabel.TabIndex = 0;
            this.FilterLabel.Text = "Filter";
            this.FilterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Filter
            // 
            this.Filter.Enabled = false;
            this.Filter.Location = new System.Drawing.Point(89, 3);
            this.Filter.Name = "Filter";
            this.Filter.Size = new System.Drawing.Size(100, 20);
            this.Filter.TabIndex = 1;
            this.Filter.TextChanged += new System.EventHandler(this.Filter_TextChanged);
            // 
            // TailFile
            // 
            this.TailFile.Checked = true;
            this.TailFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TailFile.Location = new System.Drawing.Point(195, 3);
            this.TailFile.Name = "TailFile";
            this.TailFile.Size = new System.Drawing.Size(120, 24);
            this.TailFile.TabIndex = 2;
            this.TailFile.Text = "Follow tail";
            this.TailFile.UseVisualStyleBackColor = true;
            // 
            // BtRefresh
            // 
            this.BtRefresh.Location = new System.Drawing.Point(321, 3);
            this.BtRefresh.Name = "BtRefresh";
            this.BtRefresh.Size = new System.Drawing.Size(75, 20);
            this.BtRefresh.TabIndex = 3;
            this.BtRefresh.Text = "Refresh";
            this.BtRefresh.UseVisualStyleBackColor = true;
            this.BtRefresh.Click += new System.EventHandler(this.BtRefresh_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.LogTree, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.LogText, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(786, 505);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // LogTree
            // 
            this.LogTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogTree.Location = new System.Drawing.Point(3, 3);
            this.LogTree.Name = "LogTree";
            this.LogTree.Size = new System.Drawing.Size(244, 499);
            this.LogTree.TabIndex = 2;
            this.LogTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.LogTree_AfterSelect);
            // 
            // PagePlot
            // 
            this.PagePlot.Controls.Add(this.splitContainer3);
            this.PagePlot.Location = new System.Drawing.Point(4, 22);
            this.PagePlot.Name = "PagePlot";
            this.PagePlot.Padding = new System.Windows.Forms.Padding(3);
            this.PagePlot.Size = new System.Drawing.Size(792, 545);
            this.PagePlot.TabIndex = 1;
            this.PagePlot.Text = "Plot";
            this.PagePlot.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.flowLayoutPanel2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.ThePlotter);
            this.splitContainer3.Size = new System.Drawing.Size(786, 539);
            this.splitContainer3.SplitterDistance = 30;
            this.splitContainer3.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.BtRefresh2);
            this.flowLayoutPanel2.Controls.Add(this.BtClearMarkers);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(786, 30);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // BtRefresh2
            // 
            this.BtRefresh2.Location = new System.Drawing.Point(3, 3);
            this.BtRefresh2.Name = "BtRefresh2";
            this.BtRefresh2.Size = new System.Drawing.Size(75, 20);
            this.BtRefresh2.TabIndex = 3;
            this.BtRefresh2.Text = "Refresh";
            this.BtRefresh2.UseVisualStyleBackColor = true;
            this.BtRefresh2.Click += new System.EventHandler(this.BtRefresh_Click);
            // 
            // BtClearMarkers
            // 
            this.BtClearMarkers.Location = new System.Drawing.Point(84, 3);
            this.BtClearMarkers.Name = "BtClearMarkers";
            this.BtClearMarkers.Size = new System.Drawing.Size(100, 20);
            this.BtClearMarkers.TabIndex = 4;
            this.BtClearMarkers.Text = "Clear markers";
            this.BtClearMarkers.UseVisualStyleBackColor = true;
            this.BtClearMarkers.Click += new System.EventHandler(this.BtClearMarkers_Click);
            // 
            // LogText
            // 
            this.LogText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogText.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.LogText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogText.FormattingEnabled = true;
            this.LogText.HorizontalScrollbar = true;
            this.LogText.ItemHeight = 16;
            this.LogText.Location = new System.Drawing.Point(253, 3);
            this.LogText.Name = "LogText";
            this.LogText.ScrollAlwaysVisible = true;
            this.LogText.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.LogText.Size = new System.Drawing.Size(530, 499);
            this.LogText.TabIndex = 3;
            this.LogText.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LogText_DrawItem);
            this.LogText.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.LogText_MeasureItem);
            // 
            // ThePlotter
            // 
            this.ThePlotter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ThePlotter.Location = new System.Drawing.Point(0, 0);
            this.ThePlotter.Name = "ThePlotter";
            this.ThePlotter.Size = new System.Drawing.Size(786, 505);
            this.ThePlotter.TabIndex = 0;
            // 
            // LogDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "LogDisplay";
            this.Size = new System.Drawing.Size(800, 600);
            this.SizeChanged += new System.EventHandler(this.LogDisplay_SizeChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.PageLog.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.PagePlot.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label FilterLabel;
        private System.Windows.Forms.TextBox Filter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TreeView LogTree;
        private System.Windows.Forms.CheckBox TailFile;
        private SubListBox LogText;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage PageLog;
        private System.Windows.Forms.TabPage PagePlot;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ProgressBar BuilderProgress;
        private Plotter ThePlotter;
        private System.Windows.Forms.Button BtRefresh;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button BtRefresh2;
        private System.Windows.Forms.Button BtClearMarkers;
    }
}
