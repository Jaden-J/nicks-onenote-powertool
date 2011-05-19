using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace NicksPowerTool.ONReader.PageNodes.PageNodeProperties
{
    [NodeName("Size")]
    public class SizeProperty : PageProperty
    {
        public double Width
        {
            get
            {
                return Attributes.GetAttributeValueDouble("width");
            }

            set
            {
                Attributes.SetAttributeValue("width", false, value.ToString());
            }
        }

        public double Height
        {
            get
            {
                return Attributes.GetAttributeValueDouble("height");
            }

            set
            {
                Attributes.SetAttributeValue("height", false, value.ToString());
            }
        }

        public Size Size
        {
            get
            {
                return new Size(Width, Height);
            }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }
    }
}
