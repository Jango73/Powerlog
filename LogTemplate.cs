
using System;
using System.Collections.Generic;
using System.Text;

namespace PowerLog
{
    public class LogTemplate
    {
        private List<LogBlock> m_Tags;

        /// <summary>
        /// 
        /// </summary>
        public List<LogBlock> Tags
        {
            get { return m_Tags; }
            set { m_Tags = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public LogTemplate()
        {
            m_Tags = new List<LogBlock>();
        }
    }
}
