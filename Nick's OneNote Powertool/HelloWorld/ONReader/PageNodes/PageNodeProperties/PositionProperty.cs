using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace NicksPowerTool.ONReader.PageNodes.PageNodeProperties
{
    [NodeName("Position")]
    class PositionProperty : PageProperty
    {
        public double x
        {
            get
            {
                return GetDoubleValue("x");
            }

            set
            {
                Attributes
            }
        }

        public double y
        {

        }

        public double z
        {

        }

        private double GetDoubleValue(String parameter)
        {
            String s = Attributes.GetAttributeValueString(parameter);
            return Double.Parse(s);
        }

        private void SetDoubleValue(String parameter, double value)
        {
            Attributes.
        }
    }
}
