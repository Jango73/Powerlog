
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PowerLog
{
    public class LogBlock
    {
        private String m_Name;
        private String m_Parent;
        private String m_Pattern;
        private String m_Extract;
        private String m_Plot;
        private DateTime m_Time;
        private Color m_BackColor;
        private Color m_ForeColor;
        private Double m_PlotValue;

        // Runtime storage

        private String m_Text;
        private Int64 m_Offset;
        private Int64 m_Line;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public String Parent
        {
            get { return m_Parent; }
            set { m_Parent = value; }
        }

        /// <summary>
        /// Gets or sets the pattern.
        /// </summary>
        /// <value>The pattern.</value>
        public String Pattern
        {
            get { return m_Pattern; }
            set { m_Pattern = value; }
        }

        /// <summary>
        /// Gets or sets the extract.
        /// </summary>
        /// <value>The extract.</value>
        public String Extract
        {
            get { return m_Extract; }
            set { m_Extract = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public String Plot
        {
            get { return m_Plot; }
            set { m_Plot = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Time
        {
            get { return m_Time; }
            set { m_Time = value; }
        }

        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color BackColor
        {
            get { return m_BackColor; }
            set { m_BackColor = value; }
        }

        /// <summary>
        /// Gets or sets the color of the fore.
        /// </summary>
        /// <value>The color of the fore.</value>
        public Color ForeColor
        {
            get { return m_ForeColor; }
            set { m_ForeColor = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Double PlotValue
        {
            get { return m_PlotValue; }
            set { m_PlotValue = value; }
        }

        // Runtime storage

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public String Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public Int64 Offset
        {
            get { return m_Offset; }
            set { m_Offset = value; }
        }

        /// <summary>
        /// Gets or sets the line.
        /// </summary>
        /// <value>The line.</value>
        public Int64 Line
        {
            get { return m_Line; }
            set { m_Line = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogBlock"/> class.
        /// </summary>
        public LogBlock()
        {
            m_Time = DateTime.MinValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogBlock"/> class.
        /// </summary>
        /// <param name="Target">The target.</param>
        public LogBlock(LogBlock Target)
        {
            m_Name = Target.m_Name;
            m_Parent = Target.m_Parent;
            m_Pattern = Target.m_Pattern;
            m_Extract = Target.m_Extract;
            m_Plot = Target.m_Plot;
            m_Time = Target.m_Time;
            m_BackColor = Target.m_BackColor;
            m_ForeColor = Target.m_ForeColor;
            m_PlotValue = Target.m_PlotValue;

            m_Text = Target.m_Text;
            m_Offset = Target.m_Offset;
            m_Line = Target.m_Line;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogBlock"/> class.
        /// </summary>
        /// <param name="_Name">Name of the _.</param>
        /// <param name="_Parent">The _ parent.</param>
        /// <param name="_Pattern">The _ pattern.</param>
        /// <param name="_Extract">The _ extract.</param>
        /// <param name="_BackColor">Color of the _ back.</param>
        /// <param name="_ForeColor">Color of the _ fore.</param>
        public LogBlock(String _Name, String _Parent, String _Pattern, String _Extract, String _Plot, Color _BackColor, Color _ForeColor)
        {
            m_Name = _Name;
            m_Parent = _Parent;
            m_Pattern = _Pattern;
            m_Extract = _Extract;
            m_Plot = _Plot;
            m_Time = DateTime.Now;
            m_BackColor = _BackColor;
            m_ForeColor = _ForeColor;
            m_PlotValue = 0.0;
        }
    }
}
