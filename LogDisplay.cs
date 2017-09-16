
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;

namespace PowerLog
{
    public partial class LogDisplay : UserControl
    {
        #region Fields

        private Int64 m_NumStreamOpen = 0;
        private Int64 m_Offset;
        private Double m_Progress = 0.0;
        private volatile String m_FileName;
        private volatile int m_DisplayedLines;
        private volatile Boolean m_FileChanged = false;
        private volatile FileStream m_Stream = null;
        private volatile System.Windows.Forms.Timer m_Timer;
        private volatile FileSystemWatcher m_Watcher = null;
        private volatile Dictionary<Int64, Int64> m_LineOffsetDico = null;
        private volatile Dictionary<Int64, LogBlock> m_OffsetBlockDico = null;
        private volatile LogTemplate m_Template = null;
        private volatile List<LogBlock> m_NewNodes = null;

        private BackgroundWorker LineOffsetDicoBuilder;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public String FileName
        {
            get { return m_FileName; }
            set
            {
                m_FileName = value;

                LoadFileTemplate();
                InitializeWatcher();
                InitializeLineOffsetDicoBuilder();
                ResetDisplay();
                ThePlotter.Clear();

                if (TailFile.Checked) SeekEnd();

                UpdateDisplay();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LogDisplay"/> class.
        /// </summary>
        public LogDisplay()
        {
            InitializeComponent();

            m_Timer = new System.Windows.Forms.Timer();
            m_Timer.Interval = 500;
            m_Timer.Tick += new EventHandler(m_Timer_Tick);
            m_Timer.Start();

            LogText.WheelDelegate += HandleKeyDown;

            LogText.Font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular);

            m_LineOffsetDico = new Dictionary<Int64, Int64>();
            m_OffsetBlockDico = new Dictionary<Int64, LogBlock>();
        }

        #endregion

        #region Work

        /// <summary>
        /// Opens the stream.
        /// </summary>
        private void OpenStream()
        {
            if (m_NumStreamOpen == 0)
            {
                m_Stream = new FileStream(m_FileName, FileMode.Open, FileAccess.Read, System.IO.FileShare.ReadWrite);
            }

            m_NumStreamOpen++;
        }

        /// <summary>
        /// Closes the stream.
        /// </summary>
        private void CloseStream()
        {
            m_NumStreamOpen--;

            if (m_NumStreamOpen == 0)
            {
                if (m_Stream != null)
                {
                    m_Stream.Close();
                    m_Stream.Dispose();
                }
                m_Stream = null;
            }
        }

        /// <summary>
        /// Loads the file template.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        private void LoadFileTemplate()
        {
            m_Template = FileTemplateLoader.Load(m_FileName, "Templates.xml");
        }

        /// <summary>
        /// Initializes the watcher.
        /// </summary>
        private void InitializeWatcher()
        {
            if (m_Watcher != null)
            {
                m_Watcher.EnableRaisingEvents = false;
                m_Watcher.Dispose();
            }

            m_Watcher = new FileSystemWatcher(Path.GetDirectoryName(m_FileName));
            m_Watcher.Filter = Path.GetFileName(FileName);
            m_Watcher.NotifyFilter = NotifyFilters.LastWrite;
            m_Watcher.Changed += new FileSystemEventHandler(m_Watcher_Changed);
            m_Watcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Initializes the line offset array builder.
        /// </summary>
        private void InitializeLineOffsetDicoBuilder()
        {
            if (LineOffsetDicoBuilder != null)
            {
                LineOffsetDicoBuilder.CancelAsync();
                while (LineOffsetDicoBuilder.IsBusy)
                {
                    Application.DoEvents();
                }
                LineOffsetDicoBuilder.Dispose();
            }

            m_LineOffsetDico.Clear();
            m_OffsetBlockDico.Clear();

            LineOffsetDicoBuilder = new BackgroundWorker();
            LineOffsetDicoBuilder.WorkerSupportsCancellation = true;
            LineOffsetDicoBuilder.DoWork += new DoWorkEventHandler(LineOffsetDicoBuilder_DoWork);
            LineOffsetDicoBuilder.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LineOffsetDicoBuilder_RunWorkerCompleted);
            LineOffsetDicoBuilder.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the DoWork event of the LineOffsetArrayBuilder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void LineOffsetDicoBuilder_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(10);

            m_NewNodes = new List<LogBlock>();

            Int64 Line = 0;
            Int64 Offset = 0;

            // Get the last offset/line where we stopped

            if (m_LineOffsetDico.Keys.Count > 0)
            {
                List<Int64> KeyList = new List<Int64>(m_LineOffsetDico.Keys);
                Line = KeyList[KeyList.Count - 1];
                Offset = m_LineOffsetDico[Line];
            }

            FileStream TheStream = null;

            try
            {
                int LineCount = 0;

                // Open stream, read a line and close stream

                TheStream = new FileStream(m_FileName, FileMode.Open, FileAccess.Read, System.IO.FileShare.ReadWrite);

                while (true && LineCount < 1000)
                {
                    if (LineOffsetDicoBuilder.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    Int64 SavedOffset = 0;

                    TheStream.Position = Offset;
                    SavedOffset = Offset;
                    String LineString = GetLine(TheStream);
                    Offset = TheStream.Position;

                    if (LineString == String.Empty) break;

                    if (m_LineOffsetDico.ContainsKey(Line) == false)
                    {
                        m_LineOffsetDico.Add(Line, SavedOffset);

                        LogBlock Block = GetLineBlock(LineString);

                        if (Block != null)
                        {
                            if (Block.Plot == String.Empty)
                            {
                                Block.Offset = SavedOffset;
                                Block.Line = Line;

                                m_NewNodes.Add(Block);

                                m_OffsetBlockDico.Add(SavedOffset, Block);
                            }
                            else
                            {
                                ThePlotter.AddValue(Block.Name, new LogValue(Block.Time, Block.PlotValue));
                            }
                        }

                        Line++;
                    }
                    else
                    {
                        Line++;
                    }

                    LineCount++;
                }

                m_Progress = ((double)Offset / (double)TheStream.Length);

                TheStream.Close();
                TheStream.Dispose();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the LineOffsetArrayBuilder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void LineOffsetDicoBuilder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) return;

            BuilderProgress.Value = (int)(m_Progress * 100.0);

            foreach (LogBlock Block in m_NewNodes)
            {
                TreeNode Node = new TreeNode();

                Node.Name = Block.Name;
                Node.Text = Block.Text;
                Node.Tag = Block;

                if (LogTree.Nodes.Count > 0)
                {
                    TreeNode LastNode = GetLastNode(LogTree.Nodes[LogTree.Nodes.Count - 1], Block.Parent);

                    if (LastNode != null)
                    {
                        LastNode.Nodes.Add(Node);
                    }
                    else
                    {
                        LogTree.Nodes.Add(Node);
                    }
                }
                else
                {
                    LogTree.Nodes.Add(Node);
                }
            }

            ThePlotter.RefreshPlots();

            LineOffsetDicoBuilder.RunWorkerAsync();
        }

        /// <summary>
        /// Gets the last node.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        public TreeNode GetLastNode(TreeNode Start, String Name)
        {
            if (Start.Name == Name) return Start;

            if (Start.Nodes.Count > 0)
            {
                TreeNode Node = GetLastNode(Start.Nodes[Start.Nodes.Count - 1], Name);
                if (Node != null) return Node;
            }

            return null;
        }

        /// <summary>
        /// Resets the display.
        /// </summary>
        private void ResetDisplay()
        {
            m_DisplayedLines = ((LogText.Height - 30) / LogText.ItemHeight);
            m_Offset = 0;
        }

        /// <summary>
        /// Updates the display.
        /// </summary>
        private void UpdateDisplay()
        {
            SuspendLayout();
            LogText.Items.Clear();

            String FilterString = Filter.Text.ToLower();

            int RealLineCount = 0;

            try
            {
                OpenStream();
                m_Stream.Position = m_Offset;

                int LineCount = 0;
                Int64 SavedOffset = 0;

                while (true)
                {
                    SavedOffset = m_Stream.Position;

                    String Line = GetLine(m_Stream);

                    if (Line == String.Empty) break;

                    if (FilterString == String.Empty || Line.ToLower().Contains(FilterString))
                    {
                        LogText.Items.Add(GetLogItemForOffset(SavedOffset, Line));

                        LineCount++;
                        if (LineCount >= m_DisplayedLines) break;
                    }

                    RealLineCount++;
                    if (RealLineCount > m_DisplayedLines * 10) break;
                }

                ResumeLayout();
            }
            catch
            {
                CloseStream();
            }
            finally
            {
                CloseStream();
            }
        }

        /// <summary>
        /// Gets the line.
        /// </summary>
        /// <returns></returns>
        private String GetLine(FileStream Stream)
        {
            StringBuilder Builder = new StringBuilder();

            while (true)
            {
                int Char = Stream.ReadByte();

                if (Stream.Position >= Stream.Length) break;

                if (Char == (int)'\n') break;

                if (Char != '\r') Builder.Append((char)Char);
            }

            return Builder.ToString();
        }

        /// <summary>
        /// Gets the log item for line.
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <returns></returns>
        private LogItem GetLogItemForOffset(Int64 Offset, String Text)
        {
            if (m_OffsetBlockDico.ContainsKey(Offset))
            {
                LogBlock Const = m_OffsetBlockDico[Offset];

                return new LogItem(Text, Const.BackColor, Const.ForeColor);
            }

            return new LogItem(Text, Color.White, Color.Black);
        }

        /// <summary>
        /// Gets the line tag.
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <returns></returns>
        private LogBlock GetLineBlock(String LineText)
        {
            foreach (LogBlock Blk in m_Template.Tags)
            {
                Match Pattern = Regex.Match(LineText, Blk.Pattern);

                if (Pattern.Success)
                {
                    Match Extract = Regex.Match(LineText, Blk.Extract);
                    Match Plot = Regex.Match(LineText, Blk.Plot);

                    LogBlock ReturnValue = new LogBlock(Blk);

                    if (Blk.Plot != String.Empty && Plot.Success)
                    {
                        if (Plot.Groups["VALUE"] != null)
                        {
                            String aValue = Plot.Groups["VALUE"].ToString();
                            ReturnValue.PlotValue = Tools.GetDouble(aValue);
                        }
                    }

                    ReturnValue.Time = FileTemplateLoader.GetTimeFromMatch(Extract);

                    ReturnValue.Text = Blk.Name + " " + Extract.Value;

                    return ReturnValue;
                }
            }

            return null;
        }

        /// <summary>
        /// Seeks the home.
        /// </summary>
        private void SeekHome()
        {
            m_Offset = 0;
        }

        /// <summary>
        /// Seeks the home.
        /// </summary>
        private void SeekEnd()
        {
            try
            {
                OpenStream();
                m_Offset = m_Stream.Length;

                SeekPrevLine(m_DisplayedLines - 1);
            }
            catch
            {
                CloseStream();
            }
            finally
            {
                CloseStream();
            }
        }

        /// <summary>
        /// Seeks the next line.
        /// </summary>
        private void SeekNextLine(int TotalCount)
        {
            try
            {
                OpenStream();
                m_Stream.Position = m_Offset;

                int Count = 0;

                while (true)
                {
                    int Char = m_Stream.ReadByte();

                    if (m_Stream.Position >= m_Stream.Length) break;

                    if (Char == (int)'\n')
                    {
                        Count++;

                        if (Count >= TotalCount)
                        {
                            m_Offset = m_Stream.Position;
                            break;
                        }
                    }
                }
            }
            catch
            {
                CloseStream();
            }
            finally
            {
                CloseStream();
            }
        }

        /// <summary>
        /// Seeks the prev line.
        /// </summary>
        private void SeekPrevLine(int TotalCount)
        {
            if (m_Offset == 0) return;

            try
            {
                OpenStream();
                m_Stream.Position = m_Offset;

                int Count = 0;

                while (true)
                {
                    if (m_Stream.Position <= 1)
                    {
                        m_Offset = 0;
                        break;
                    }

                    m_Stream.Position -= 2;

                    int Char = m_Stream.ReadByte();

                    if (Char == (int)'\n')
                    {
                        Count++;

                        if (Count >= TotalCount)
                        {
                            m_Offset = m_Stream.Position;
                            break;
                        }
                    }
                }

                if (m_Offset <= 0) m_Offset = 0;
            }
            catch
            {
                CloseStream();
            }
            finally
            {
                CloseStream();
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        public void HandleKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Home:
                    {
                        SeekHome();
                        UpdateDisplay();
                    }
                    break;
                case Keys.End:
                    {
                        SeekEnd();
                        UpdateDisplay();
                    }
                    break;
                case Keys.Down:
                    {
                        SeekNextLine(1);
                        UpdateDisplay();
                    }
                    break;
                case Keys.PageDown:
                    {
                        SeekNextLine(m_DisplayedLines / 3);
                        UpdateDisplay();
                    }
                    break;
                case Keys.Up:
                    {
                        SeekPrevLine(1);
                        UpdateDisplay();
                    }
                    break;
                case Keys.PageUp:
                    {
                        SeekPrevLine(m_DisplayedLines / 3);
                        UpdateDisplay();
                    }
                    break;
            }
        }

        /// <summary>
        /// Handles the Tick event of the m_Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void m_Timer_Tick(object sender, EventArgs e)
        {
            m_Timer.Stop();

            if (m_FileChanged)
            {
                m_FileChanged = false;

                if (TailFile.Checked)
                {
                    SeekEnd();
                    UpdateDisplay();
                }
            }

            m_Timer.Start();
        }

        /// <summary>
        /// Handles the Changed event of the m_Watcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.IO.FileSystemEventArgs"/> instance containing the event data.</param>
        private void m_Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(500);
            m_FileChanged = true;
        }

        /// <summary>
        /// Handles the TextChanged event of the Filter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Filter_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the SizeChanged event of the LogDisplay control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LogDisplay_SizeChanged(object sender, EventArgs e)
        {
            m_DisplayedLines = ((LogText.Height - 30) / LogText.ItemHeight);
            m_FileChanged = true;
        }

        /// <summary>
        /// Handles the AfterSelect event of the LogTree control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void LogTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LogBlock Node = (LogBlock)LogTree.SelectedNode.Tag;

            if (m_LineOffsetDico.ContainsKey(Node.Line))
            {
                m_Offset = m_LineOffsetDico[Node.Line];
                UpdateDisplay();
            }
        }

        /// <summary>
        /// Handles the MeasureItem event of the LogText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MeasureItemEventArgs"/> instance containing the event data.</param>
        private void LogText_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox Box = (ListBox)sender;

            Size size = e.Graphics.MeasureString(Box.Items[e.Index].ToString(), Box.Font).ToSize();
            e.ItemWidth = size.Width;
            e.ItemHeight = size.Height;

            if
            (
                Box.HorizontalScrollbar &&
                Box.Width < e.ItemWidth &&
                e.ItemWidth > Box.HorizontalExtent
            )
            {
                Box.HorizontalExtent = e.ItemWidth;
            }
        }

        /// <summary>
        /// Handles the DrawItem event of the LogText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void LogText_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox Box = (ListBox)sender;

            if (e.Index >= 0)
            {
                LogItem Item = (LogItem)Box.Items[e.Index];

                // e.DrawFocusRectangle();
                // e.DrawBackground();

                e.Graphics.FillRectangle(new SolidBrush(Item.BackColor), e.Bounds);
                e.Graphics.DrawString(Item.Text, Box.Font, new SolidBrush(Item.ForeColor), e.Bounds);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtRefresh_Click(object sender, EventArgs e)
        {
            FileName = m_FileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtClearMarkers_Click(object sender, EventArgs e)
        {
            ThePlotter.ClearMarkers();
        }

        #endregion
    }
}
