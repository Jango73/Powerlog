
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace PowerLog
{
    class LogItem
    {
        private String m_Text;
        private Color m_BackColor;
        private Color m_ForeColor;

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
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color BackColor
        {
            get { return m_BackColor; }
            set { m_BackColor = value; }
        }

        public Color ForeColor
        {
            get { return m_ForeColor; }
            set { m_ForeColor = value; }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override String ToString()
        {
            return m_Text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogItem"/> class.
        /// </summary>
        /// <param name="_Text">The _ text.</param>
        /// <param name="_BackColor">Color of the _ back.</param>
        public LogItem(String _Text, Color _BackColor, Color _ForeColor)
        {
            m_Text = _Text;
            m_BackColor = _BackColor;
            m_ForeColor = _ForeColor;
        }
    }
}
