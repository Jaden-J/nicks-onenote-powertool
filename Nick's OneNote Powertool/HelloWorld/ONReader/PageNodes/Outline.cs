using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NicksPowerTool
{
    class Outline : ONNode
    {
        public String Author;
        public String LastModifiedBy;
        public DateTime LastModifiedTime;

        public Outline(XmlNode node, ONNode parentNode) : base(node, parentNode)
        {
        }
    }
}
