using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NicksPowerTool.ONReader.PageNodeAugmentation;
using System.Xml;

namespace NicksPowerTool.ONReader.PageNodes.PageNodeProperties
{
    [NodeName("Data")]
    class DataProperty : PageProperty, ICanConstruct
    {
        public DataProperty(PageNode parent) : base()
        {
            XmlElement e = parent.Node.OwnerDocument.CreateElement("Data");
        }
    }
}
