
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PowerLog
{
    public partial class Plotter : UserControl
    {
        public enum EMouseAction
        {
            None,
            Moving,
            Scaling,
            AddMarker
        }

        private List<PlotterValues> m_Values;
        private List<LogValue> m_Markers;
        private DateTime m_Time;
        private Double m_DrawScale;
        private Double m_ValueHeight;
        private Pen m_CurvePen;
        private Pen m_DotPen;
        private Pen m_DelimPen;
        private Pen m_MarkerPen;
        private Brush m_MarkerBrush;
        private EMouseAction m_MouseAction;
        private Point m_MousePosition;
        private Point m_PreviousMousePosition;
        private Point m_Pivot;
        private const Int32 m_LabelWidth = 80;
        private Boolean m_Dirty = false;
        private List<Pen> m_GridPens;

        /// <summary>
        /// 
        /// </summary>
        public List<PlotterValues> Values
        {
            get { return m_Values; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Time
        {
            get { return m_Time; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Double DrawScale
        {
            get { return m_DrawScale; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Double ValueHeight
        {
            get { return m_ValueHeight; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Plotter()
        {
            InitializeComponent();

            m_Values = new List<PlotterValues>();
            m_Markers = new List<LogValue>();
            m_Time = DateTime.MinValue;
            m_DrawScale = 10.0;
            m_ValueHeight = 100.0;
            m_MouseAction = EMouseAction.None;

            m_CurvePen = new Pen(Color.LightGray);
            m_DotPen = new Pen(Color.White);
            m_DelimPen = new Pen(Color.Black);
            m_MarkerPen = new Pen(Color.Yellow);

            m_GridPens = new List<Pen>();

            m_GridPens.Add(new Pen(Color.Cyan));        // 100th of second
            m_GridPens.Add(new Pen(Color.Yellow));      // Second
            m_GridPens.Add(new Pen(Color.Black));       // Minute
            m_GridPens.Add(new Pen(Color.LightGray));   // Hour

            m_MarkerBrush = new SolidBrush(Color.Yellow);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            m_Values.Clear();
            m_Time = DateTime.MinValue;
            m_DrawScale = 10.0;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearMarkers()
        {
            m_Markers.Clear();
            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aValue"></param>
        public void AddValue(String aName, LogValue aValue)
        {
            lock (this)
            {
                m_Dirty = true;

                if (m_Time == DateTime.MinValue)
                {
                    m_Time = aValue.Time;
                }

                foreach (PlotterValues Values in m_Values)
                {
                    if (Values.Name == aName)
                    {
                        Values.AddValue(aValue);
                        return;
                    }
                }

                // Not found, add the value
                PlotterValues NewValues = new PlotterValues(aName);
                NewValues.AddValue(aValue);
                m_Values.Add(NewValues);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshPlots()
        {
            if (m_Dirty)
            {
                m_Dirty = false;
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Paint"></param>
        protected override void OnPaintBackground(PaintEventArgs Paint)
        {
            Paint.Graphics.Clear(System.Drawing.Color.Gray);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs Paint)
        {
            if (m_Values.Count > 0)
            {
                m_ValueHeight = (double)Height / (double)m_Values.Count;
            }

            DrawValues(Paint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aValue"></param>
        private void DrawValues(PaintEventArgs Paint)
        {
            lock (this)
            {
                int Index = 0;

                foreach (PlotterValues aValue in m_Values)
                {
                    DrawValue(Paint, aValue, Index);
                    Index++;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aPlotterValue"></param>
        private void DrawValue(PaintEventArgs Paint, PlotterValues aPlotterValue, int ValueIndex)
        {
            Point PreviousPosition = new Point();
            Point CurrentPosition = new Point();

            Double aVerticalPosition1 = ((ValueIndex + 0) * m_ValueHeight) + (m_ValueHeight * 0.05);
            Double aVerticalPosition2 = ((ValueIndex + 1) * m_ValueHeight) - (m_ValueHeight * 0.05);

            Double DataHeight = aVerticalPosition2 - aVerticalPosition1;

            // Grid
            DrawValueGrid(Paint, aVerticalPosition1, aVerticalPosition2);

            // Delimiters
            Point DelimPoint_11 = new Point(0, (int)aVerticalPosition1);
            Point DelimPoint_12 = new Point(Width, (int)aVerticalPosition1);

            Point DelimPoint_21 = new Point(0, (int)aVerticalPosition2);
            Point DelimPoint_22 = new Point(Width, (int)aVerticalPosition2);

            Paint.Graphics.DrawLine(m_DelimPen, DelimPoint_11, DelimPoint_12);
            Paint.Graphics.DrawLine(m_DelimPen, DelimPoint_21, DelimPoint_22);

            // Range
            Rectangle TextPosition = new Rectangle();
            String Minimum = aPlotterValue.Minimum.ToString();
            String Maximum = aPlotterValue.Maximum.ToString();
            Double LabelHeight = DataHeight * 0.25;

            TextPosition.X = 10;
            TextPosition.Y = (int)(aVerticalPosition1);
            TextPosition.Width = m_LabelWidth;
            TextPosition.Height = (int)LabelHeight;

            DrawCenteredText(Paint, Maximum, TextPosition, System.Drawing.SystemFonts.CaptionFont, SystemBrushes.ActiveCaptionText);

            TextPosition.X = 10;
            TextPosition.Y = (int)(aVerticalPosition1 + LabelHeight * 3.0);
            TextPosition.Width = m_LabelWidth;
            TextPosition.Height = (int)LabelHeight;

            DrawCenteredText(Paint, Minimum, TextPosition, System.Drawing.SystemFonts.CaptionFont, SystemBrushes.ActiveCaptionText);

            TextPosition.X = 10;
            TextPosition.Y = (int)(aVerticalPosition1 + LabelHeight * 1.0);
            TextPosition.Width = m_LabelWidth;
            TextPosition.Height = (int)(LabelHeight * 2.0);

            DrawCenteredText(Paint, aPlotterValue.Name, TextPosition, System.Drawing.SystemFonts.CaptionFont, SystemBrushes.ActiveCaptionText);

            // Lines
            foreach (LogValue aValue in aPlotterValue.Values)
            {
                Double aNormalizedValue = aPlotterValue.GetNormalizedValue(aValue.Value);

                if (aNormalizedValue != Double.NaN)
                {
                    CurrentPosition.X = (int)TimeToPosition(aValue.Time);
                    CurrentPosition.Y = (int)aVerticalPosition1 + (int)((1.0 - aNormalizedValue) * DataHeight);

                    if (
                        CurrentPosition.X > 0 &&
                        CurrentPosition.X < Width &&
                        CurrentPosition.Y > 0 &&
                        CurrentPosition.Y < Height
                        )
                    {
                        if (PreviousPosition.X != 0 && PreviousPosition.Y != 0)
                        {
                            if (PreviousPosition.X > -1000 && PreviousPosition.X < Width + 1000)
                            {
                                Point PreviousPositionLinear = new Point(PreviousPosition.X, PreviousPosition.Y);
                                Point CurrentPositionLinear = new Point(CurrentPosition.X, PreviousPosition.Y);
                                Paint.Graphics.DrawLine(m_CurvePen, PreviousPositionLinear, CurrentPositionLinear);

                                Point Mark1 = new Point(CurrentPosition.X + 0, CurrentPosition.Y - 1);
                                Point Mark2 = new Point(CurrentPosition.X + 0, CurrentPosition.Y + 1);
                                Point Mark3 = new Point(CurrentPosition.X - 1, CurrentPosition.Y + 0);
                                Point Mark4 = new Point(CurrentPosition.X + 1, CurrentPosition.Y + 0);

                                Paint.Graphics.DrawLine(m_DotPen, Mark1, Mark2);
                                Paint.Graphics.DrawLine(m_DotPen, Mark3, Mark4);
                            }
                        }
                    }

                    PreviousPosition = CurrentPosition;
                }
            }

            // Markers
            foreach (LogValue aValue in m_Markers)
            {
                CurrentPosition.X = (int)TimeToPosition(aValue.Time);

                if (CurrentPosition.X > 0 && CurrentPosition.X < Width)
                {
                    Point Mark1 = new Point(CurrentPosition.X, 0);
                    Point Mark2 = new Point(CurrentPosition.X, Height);

                    Paint.Graphics.DrawLine(m_MarkerPen, Mark1, Mark2);

                    TextPosition.X = Mark1.X;
                    TextPosition.Y = Mark1.Y;
                    TextPosition.Width = m_LabelWidth;
                    TextPosition.Height = (int)LabelHeight;

                    String Text = aValue.Time.ToLongTimeString();

                    DrawCenteredText(Paint, Text, TextPosition, System.Drawing.SystemFonts.CaptionFont, m_MarkerBrush);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawValueGrid(PaintEventArgs Paint, Double aVerticalPosition1, Double aVerticalPosition2)
        {
            Point CurrentPosition = new Point();

            for (int TimeScale = 0; TimeScale < 4; TimeScale++)
            {
                DateTime ReferenceTime = m_Time;
                bool CanDraw = false;

                switch (TimeScale)
                {
                    case 0:
                        ReferenceTime = ReferenceTime.AddMilliseconds(-m_Time.Millisecond);

                        if (TimeToPosition(m_Time.AddMilliseconds(100)) - TimeToPosition(m_Time) > 5)
                        {
                            CanDraw = true;
                        }
                        break;
                    case 1:
                        ReferenceTime = ReferenceTime.AddMilliseconds(-m_Time.Millisecond);

                        if (TimeToPosition(m_Time.AddSeconds(1)) - TimeToPosition(m_Time) > 5)
                        {
                            CanDraw = true;
                        }
                        break;
                    case 2:
                        ReferenceTime = ReferenceTime.AddMilliseconds(-m_Time.Millisecond);
                        ReferenceTime = ReferenceTime.AddSeconds(-m_Time.Second);

                        if (TimeToPosition(m_Time.AddMinutes(1)) - TimeToPosition(m_Time) > 5)
                        {
                            CanDraw = true;
                        }
                        break;
                    case 3:
                        ReferenceTime = ReferenceTime.AddMilliseconds(-m_Time.Millisecond);
                        ReferenceTime = ReferenceTime.AddSeconds(-m_Time.Second);
                        ReferenceTime = ReferenceTime.AddMinutes(-m_Time.Minute);

                        if (TimeToPosition(m_Time.AddHours(1)) - TimeToPosition(m_Time) > 5)
                        {
                            CanDraw = true;
                        }
                        break;
                }

                if (CanDraw)
                {
                    // Grid
                    for (int Index = 0; ; Index++)
                    {
                        Point Mark1;
                        Point Mark2;
                        DateTime aTime = ReferenceTime;

                        switch (TimeScale)
                        {
                            case 0: aTime = ReferenceTime.AddMilliseconds((double)Index * 100); break;
                            case 1: aTime = ReferenceTime.AddSeconds((double)Index); break;
                            case 2: aTime = ReferenceTime.AddMinutes((double)Index); break;
                            case 3: aTime = ReferenceTime.AddHours((double)Index); break;
                        }

                        CurrentPosition.X = (int)TimeToPosition(aTime);
                        CurrentPosition.Y = (int)aVerticalPosition1;

                        Mark1 = new Point(CurrentPosition.X + 0, CurrentPosition.Y + (3 + 3 * TimeScale));
                        Mark2 = new Point(CurrentPosition.X + 0, CurrentPosition.Y + 0);

                        Paint.Graphics.DrawLine(m_GridPens[TimeScale], Mark1, Mark2);

                        CurrentPosition.X = (int)TimeToPosition(aTime);
                        CurrentPosition.Y = (int)aVerticalPosition2;

                        Mark1 = new Point(CurrentPosition.X + 0, CurrentPosition.Y - (3 + 3 * TimeScale));
                        Mark2 = new Point(CurrentPosition.X + 0, CurrentPosition.Y + 0);

                        Paint.Graphics.DrawLine(m_GridPens[TimeScale], Mark1, Mark2);

                        if (CurrentPosition.X > Width)
                        {
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aTime"></param>
        /// <returns></returns>
        private Double TimeToPosition(DateTime aTime)
        {
            return (aTime - m_Time).TotalSeconds * m_DrawScale;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="AText"></param>
        /// <param name="ARectangle"></param>
        /// <param name="AFont"></param>
        /// <param name="ABrush"></param>
        private void DrawCenteredText(PaintEventArgs e, String AText, Rectangle ARectangle, Font AFont, Brush ABrush)
        {
            SizeF TextSize = e.Graphics.MeasureString(AText, AFont);
            PointF TextPosition = new PointF(
                ((float)ARectangle.Left + ((float)ARectangle.Width / 2.0f) - (TextSize.Width / 2.0f)),
                ((float)ARectangle.Top + ((float)ARectangle.Height / 2.0f) - (TextSize.Height / 2.0f))
                );
            e.Graphics.DrawString(AText, AFont, ABrush, TextPosition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if ((Control.ModifierKeys & Keys.Control) != 0)
                {
                    DateTime aTime = m_Time.AddSeconds(e.X / m_DrawScale);
                    m_Markers.Add(new LogValue(aTime, 0.0));

                    Invalidate();
                }
                else
                {
                    m_MouseAction = EMouseAction.Moving;
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if ((Control.ModifierKeys & Keys.Control) != 0)
                {
                    foreach (LogValue aValue in m_Markers)
                    {
                        int Position = (int)TimeToPosition(aValue.Time);
                        int Delta = Math.Abs(e.X - Position);
                        if (Delta < 5)
                        {
                            m_Markers.Remove(aValue);

                            Invalidate();
                            break;
                        }
                    }
                }
                else
                {
                    m_MouseAction = EMouseAction.Scaling;
                }
            }

            m_PreviousMousePosition = m_MousePosition = new Point(e.X, e.Y);
            m_Pivot = new Point(e.X, e.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            m_MousePosition = new Point(e.X, e.Y);

            if (m_MouseAction == EMouseAction.Moving)
            {
                Double DeltaX = m_PreviousMousePosition.X - m_MousePosition.X;
                Double Seconds = DeltaX / m_DrawScale;

                m_Time = m_Time.AddMilliseconds((int)(Seconds * 1000.0));

                Invalidate();
            }
            else if (m_MouseAction == EMouseAction.Scaling)
            {
                Double DeltaX = m_MousePosition.X - m_PreviousMousePosition.X;

                DateTime OldCenterTime = m_Time.AddSeconds((double)m_Pivot.X / m_DrawScale);

                m_DrawScale = m_DrawScale + ((DeltaX * m_DrawScale) * 0.01);

                if (m_DrawScale < 0.05)
                {
                    m_DrawScale = 0.05;
                }

                DateTime NewCenterTime = m_Time.AddSeconds((double)m_Pivot.X / m_DrawScale);

                m_Time = m_Time.AddSeconds((OldCenterTime - NewCenterTime).TotalSeconds);

                Invalidate();
            }

            m_PreviousMousePosition = m_MousePosition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            m_MouseAction = EMouseAction.None;
        }
    }
}
