
using System;
using System.Collections.Generic;
using System.Text;

namespace PowerLog
{
    public class PlotterValues
    {
        private String m_Name;
        private List<LogValue> m_Values;
        private Double m_Minimum;
        private Double m_Maximum;

        /// <summary>
        /// 
        /// </summary>
        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<LogValue> Values
        {
            get { return m_Values; }
            set { m_Values = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Double Minimum
        {
            get { return m_Minimum; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Double Maximum
        {
            get { return m_Maximum; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Double Range
        {
            get { return m_Maximum - m_Minimum; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PlotterValues()
        {
            m_Values = new List<LogValue>();
            m_Minimum = 999999.0;
            m_Maximum = -999999.0;
        }

        /// <summary>
        /// 
        /// </summary>
        public PlotterValues(String aName)
        {
            m_Name = aName;
            m_Values = new List<LogValue>();
            m_Minimum = 999999.0;
            m_Maximum = -999999.0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aValue"></param>
        public void AddValue(LogValue aValue)
        {
            lock (this)
            {
                /*
                if (m_Values.Count > 0)
                {
                    if (aValue == m_Values[m_Values.Count - 1])
                    {
                        return;
                    }
                }
                 * */

                if (aValue.Value < m_Minimum) m_Minimum = aValue.Value;
                if (aValue.Value > m_Maximum) m_Maximum = aValue.Value;

                m_Values.Add(aValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aValue"></param>
        /// <returns></returns>
        public Double GetNormalizedValue(Double aValue)
        {
            if (m_Values.Count <= 1) return aValue;

            return (aValue - m_Minimum) / Range;
        }
    }
}
