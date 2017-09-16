
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Globalization;

namespace PowerLog
{
    public class Tools
    {
        static CultureInfo m_Culture = CultureInfo.CreateSpecificCulture("en-US");
        static NumberStyles m_NumberStyle = NumberStyles.Number;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aNode"></param>
        /// <returns></returns>
        public static Double GetDoubleFromXmlNode(XmlNode aNode)
        {
            if (aNode != null)
            {
                return GetDouble(aNode.InnerXml);
            }

            return -1.0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aNode"></param>
        /// <returns></returns>
        public static Int32 GetIntFromXmlNode(XmlNode aNode)
        {
            if (aNode != null)
            {
                return GetInt(aNode.InnerXml);
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aText"></param>
        /// <returns></returns>
        public static Double GetDouble(String aText)
        {
            if (aText.Contains("e"))
            {
                return 0.0;
            }

            Double aDouble;

            if (Double.TryParse(aText, m_NumberStyle, m_Culture, out aDouble))
            {
                return aDouble;
            }

            return -1.0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aText"></param>
        /// <returns></returns>
        public static Int32 GetInt(String aText)
        {
            Int32 anInt;

            if (Int32.TryParse(aText, out anInt))
            {
                return anInt;
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aText"></param>
        /// <returns></returns>
        public static String ConvertHTMLEntities(String aText)
        {
            aText = aText.Replace("&lt;", "<");
            aText = aText.Replace("&gt;", ">");

            return aText;
        }
    }
}
