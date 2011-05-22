using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NicksPowerTool.ONReader.PageNodeAugmentation;
using System.Windows.Ink;
using System.IO;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace NicksPowerTool.ONReader
{
    abstract class ISFPageNode : PageElement, IHasBinaryData
    {
        private bool _dataCaptureFailed = false;
        private StrokeCollection _Strokes = null;
        public StrokeCollection Strokes
        {
            get
            {
                if (!_dataCaptureFailed && _Strokes == null)
                {
                    initialize();
                }
                return _Strokes;
            }
        }

        //Constructor
        private void initialize()
        {
            try
            {
                byte[] byteArray = this.GetBinaryData();
                _Strokes = new StrokeCollection(new MemoryStream(byteArray));
                _dataCaptureFailed = false;
            }
            catch (Exception e)
            {
                _dataCaptureFailed = true;
            }
        }

        public bool ContainsISFData()
        {
            return Strokes != null;
        }

        public bool IsHighlighter() {
            return Strokes.First().DrawingAttributes.IsHighlighter;
        }

        public Color GetColor()
        {
            return Strokes.First().DrawingAttributes.Color;
        }

        public ReadOnlyCollection<GestureRecognitionResult> RecognizeGestures()
        {
            List<Stroke> temp = new List<Stroke>();
            temp.Add(Strokes.FirstOrDefault());
            ReadOnlyCollection<GestureRecognitionResult> results =
                new GestureRecognizer().Recognize(new StrokeCollection(temp));
            return results;
        }

        public void test() {
            LoadNPT.onApp.UpdatePageContent(Node.OwnerDocument.InnerXml);
            IEnumerable<Stroke> ss = Strokes.AsEnumerable();
            foreach (Stroke s in ss)
            {
            }
        }

        public override string ToString()
        {
            String result = "";
            result += NodeName + ": ";
            result += "Contains ISF Data: " + ContainsISFData();
            if (ContainsISFData())
            {
                result += ", IsHighlighter=" + IsHighlighter();
                result += ", Color=" + GetColor();
                result += ", Gesture recognition: ";

                ReadOnlyCollection<GestureRecognitionResult> gestures = RecognizeGestures();
                foreach (GestureRecognitionResult grr in gestures)
                {
                    result += "\r\n\t" + grr.ApplicationGesture +
                        " (Confidence: " + grr.RecognitionConfidence + "), ";
                }
            }
            return result;
        }
    }
}
