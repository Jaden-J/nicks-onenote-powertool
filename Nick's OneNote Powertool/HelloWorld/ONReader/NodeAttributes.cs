using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NicksPowerTool.ONReader
{
    class NodeAttributes : Attribute
    {
        String[] Attributes;
        public NodeAttributes(params String[] nodeAttributes) {
            this.Attributes = nodeAttributes;
        }
    }
}
