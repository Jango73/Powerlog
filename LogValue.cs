
using System;
using System.Collections.Generic;
using System.Text;

namespace PowerLog
{
    public class LogValue
    {
        private DateTime m_Time;
        private Double m_Value;

        /// <summary>
        /// 
        /// </summary>
        public DateTime Time
        {
            get { return m_Time; }
            set { m_Time = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Double Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Extract"></param>
        /// <param name="_Value"></param>
        public LogValue(DateTime _Time, Double _Value)
        {
            m_Time = _Time;
            m_Value = _Value;
        }
    }
}
