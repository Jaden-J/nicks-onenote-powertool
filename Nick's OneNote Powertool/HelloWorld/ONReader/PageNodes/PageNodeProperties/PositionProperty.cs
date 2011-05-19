using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace NicksPowerTool.ONReader.PageNodes.PageNodeProperties
{
    [NodeName("Position")]
    public class PositionProperty : PageProperty
    {
        public double X
        {
            get
            {
                return Attributes.GetAttributeValueDouble("x");
            }

            set
            {
                Attributes.SetAttributeValue("x", false, value.ToString());
            }
        }

        public double Y
        {
            get
            {
                return Attributes.GetAttributeValueDouble("y");
            }

            set
            {
                Attributes.SetAttributeValue("y", false, value.ToString());
            }
        }

        public Point Location2D
        {
            get
            {
                return new Point(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public int Z
        {
            get
            {
                return Attributes.GetAttributeValueInt("z");
            }

            set
            {
                Attributes.SetAttributeValue("z", false, value.ToString());
            }
        }
    }
}
