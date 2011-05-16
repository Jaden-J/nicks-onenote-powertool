using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NicksPowerTool.ONReader.Nodes
{
    class PageSettings
    {
        XmlNode node;
        public PageSettings(XmlNode node)
        {
            this.node = node;
        }
    }
}
