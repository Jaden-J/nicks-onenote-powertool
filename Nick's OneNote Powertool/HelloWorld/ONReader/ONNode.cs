using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NicksPowerTool
{

    public abstract class ONNode
    {
        public XmlNode Node { get; set; }
        public ONNode ParentNode { get; set; }
        public bool Changed { get; set; }
        public abstract String NodeName { get; }
    }
}