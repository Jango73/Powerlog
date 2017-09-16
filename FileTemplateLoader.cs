
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Windows.Forms;

namespace PowerLog
{
    public class FileTemplateLoader
    {
        public static LogTemplate Load(String FileName, String Templates)
        {
            LogTemplate aTemplate = new LogTemplate();
            XmlDocument Doc = new XmlDocument();

            try
            {
                Doc.Load(Templates);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }

            XmlNodeList TemplateList = Doc.SelectNodes("Root/Template");

            foreach (XmlNode TemplateNode in TemplateList)
            {
                XmlNode FilePatternNode = TemplateNode.SelectSingleNode("FileNamePattern");

                if (FilePatternNode != null)
                {
                    if (Regex.Match(FileName, FilePatternNode.InnerXml).Success)
                    {
                        XmlNodeList TagList = TemplateNode.SelectNodes("Tag");

                        foreach (XmlNode TagNode in TagList)
                        {
                            String Name = TagNode.SelectSingleNode("Name").InnerXml;
                            String Parent = TagNode.SelectSingleNode("Parent").InnerXml;
                            String Pattern = TagNode.SelectSingleNode("Pattern").InnerXml;

                            String Extract = String.Empty;

                            XmlNode ExtractNode = TagNode.SelectSingleNode("Extract");
                            if (ExtractNode != null) Extract = Tools.ConvertHTMLEntities(ExtractNode.InnerXml);

                            String Plot = String.Empty;

                            XmlNode PlotNode = TagNode.SelectSingleNode("Plot");
                            if (PlotNode != null) Plot = Tools.ConvertHTMLEntities(PlotNode.InnerXml);

                            String BackColorText = TagNode.SelectSingleNode("BackColor").InnerXml;
                            String ForeColorText = TagNode.SelectSingleNode("ForeColor").InnerXml;

                            Color BackColor = GetColorFromWording(true, BackColorText);
                            Color ForeColor = GetColorFromWording(false, ForeColorText);

                            aTemplate.Tags.Add(new LogBlock(Name, Parent, Pattern, Extract, Plot, BackColor, ForeColor));
                        }

                        return aTemplate;
                    }
                }
            }

            return aTemplate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aTemplate"></param>
        /// <param name="aMatch"></param>
        public static DateTime GetTimeFromMatch(Match aMatch)
        {
            Int32 Day = 0;
            Int32 Month = 0;
            Int32 Year = 0;
            Int32 Hour = 0;
            Int32 Minute = 0;
            Int32 Second = 0;
            Int32 Millisecond = 0;

            if (aMatch.Groups.Count > 0)
            {
                if (aMatch.Groups["DAY"] != null) Day = Tools.GetInt(aMatch.Groups["DAY"].ToString());
                if (aMatch.Groups["MONTH"] != null) Month = Tools.GetInt(aMatch.Groups["MONTH"].ToString());
                if (aMatch.Groups["YEAR"] != null) Year = Tools.GetInt(aMatch.Groups["YEAR"].ToString());
                if (aMatch.Groups["HOUR"] != null) Hour = Tools.GetInt(aMatch.Groups["HOUR"].ToString());
                if (aMatch.Groups["MINUTE"] != null) Minute = Tools.GetInt(aMatch.Groups["MINUTE"].ToString());
                if (aMatch.Groups["SECOND"] != null) Second = Tools.GetInt(aMatch.Groups["SECOND"].ToString());
                if (aMatch.Groups["MILLISECOND"] != null) Millisecond = Tools.GetInt(aMatch.Groups["MILLISECOND"].ToString());
            }

            if (Year > 0)
            {
                if (Year < 100)
                {
                    Year += 2000;
                }

                return new DateTime(Year, Month, Day, Hour, Minute, Second, Millisecond);
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the color from wording.
        /// </summary>
        /// <param name="Back">if set to <c>true</c> [back].</param>
        /// <param name="Wording">The wording.</param>
        /// <returns></returns>
        public static Color GetColorFromWording(Boolean Back, String Wording)
        {
            switch (Wording)
            {
                case "Aqua": return Color.Aqua;
                case "Aquamarine": return Color.Aquamarine;
                case "Beige": return Color.Beige;
                case "Black": return Color.Black;
                case "Blue": return Color.Blue;
                case "Cyan": return Color.Cyan;
                case "Green": return Color.Green;
                case "Magenta": return Color.Magenta;
                case "Orange": return Color.Orange;
                case "Red": return Color.Red;
                case "Teal": return Color.Teal;
                case "Turquoise": return Color.Turquoise;
                case "White": return Color.White;
                case "Yellow": return Color.Yellow;
            }

            if (Back) return Color.White;

            return Color.Black;
        }

    }
}
