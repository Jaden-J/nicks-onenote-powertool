using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NicksPowerTool.ONReader.PageNodeAugmentation;
using System.Windows.Ink;
using System.IO;
using System.Windows.Media;

namespace NicksPowerTool.ONReader
{
    abstract class ISFPageNode : PageElement, IHasBinaryData
    {
        private StrokeCollection _Strokes = null;
        public StrokeCollection Strokes
        {
            get
            {
                if (_Strokes == null)
                {
                    initialize();
                }
                return _Strokes;
            }
        }

        //Constructor
        public void initialize()
        {
            byte[] byteArray = this.GetBinaryData();
            _Strokes = new StrokeCollection(new MemoryStream(byteArray));
        }

        public bool IsHighlighter() {
            return Strokes.First().DrawingAttributes.IsHighlighter;
        }

        public Color GetColor()
        {
            return Strokes.First().DrawingAttributes.Color;
        }

        public override string ToString()
        {
            String result = "";
            result += NodeName + ": ";
            result += "IsHighlighter=" + IsHighlighter();
            result += ", Color=" + GetColor();
            return result;
        }
    }
}
