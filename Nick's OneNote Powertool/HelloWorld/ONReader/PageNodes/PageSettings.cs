using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NicksPowerTool.ONReader.PageNodes
{
    class PageSettings : PageNode
    {
        XmlNode node;
        public PageSettings(XmlNode node)
        {
            this.node = node;
        }
    }
}
