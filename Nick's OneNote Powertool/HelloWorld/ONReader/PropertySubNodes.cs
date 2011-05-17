using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NicksPowerTool.ONReader
{
    class PropertySubNodes : Attribute
    {
        public String[] SubNodes;
        public PropertySubNodes(params String[] properties)
        {
            SubNodes = properties;
        }
    }
}
