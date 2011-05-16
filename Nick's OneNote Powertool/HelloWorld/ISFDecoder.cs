using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Ink;
using System.IO;
using System.Collections.ObjectModel;

namespace NicksPowerTool
{
    class ISFDecoder
    {
        private String isf;
        private StrokeCollection strokes;

        public ISFDecoder(String isf)
        {
            this.isf = isf;
            decode();
        }

        public void decode()
        {
            byte[] byteArray = Convert.FromBase64String(isf);
            strokes = new StrokeCollection(new MemoryStream(byteArray));
        }

        public String getPropertyData()
        {
            Guid[] guids = strokes.GetPropertyDataIds();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Property data IDs:");
            foreach (Guid g in guids)
            {
                sb.Append(g.ToString() + " (");
                sb.Append(g.GetType() + "): ");
                sb.AppendLine(Convert.ToBase64String((byte[])strokes.GetPropertyData(g)));
            }
            return sb.ToString();
        }

        public String getDrawingAttributes()
        {
            DrawingAttributes da = strokes.First().DrawingAttributes;
            Guid[] guids = da.GetPropertyDataIds();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Drawing Attribute IDs:");
            foreach (Guid g in guids)
            {
                sb.Append(g.ToString() + " (");
                sb.Append(g.GetType() + "): ");
                sb.AppendLine(da.GetPropertyData(g).ToString() +
                    "(" + da.GetPropertyData(g).GetType() + ")");
            }
            sb.AppendLine("Color: " + da.Color.ToString());
            sb.AppendLine("Is Highlighter:" + da.IsHighlighter.ToString());
            return sb.ToString();
        }

        public String getGestures()
        {
            ReadOnlyCollection<GestureRecognitionResult> gestures = new GestureRecognizer().Recognize(strokes);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Gestures from this stroke collection: ");
            foreach (GestureRecognitionResult result in gestures)
            {
                sb.AppendLine("Gesture: " + result.ApplicationGesture + ".  Confidence: " + result.RecognitionConfidence);
            }
            return sb.ToString();
        }

        public String getDebugText()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(getPropertyData());
            sb.AppendLine("");
            sb.AppendLine(getDrawingAttributes());
            sb.AppendLine("");
            sb.AppendLine("Color GUID: " + DrawingAttributeIds.Color.ToString());
            sb.AppendLine("IsHighlighter GUID: " + DrawingAttributeIds.IsHighlighter.ToString());
            sb.AppendLine("");
            sb.AppendLine(getGestures());
            return sb.ToString();
        }
    }
}
