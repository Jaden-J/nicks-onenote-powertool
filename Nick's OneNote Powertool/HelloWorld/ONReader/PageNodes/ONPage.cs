using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NicksPowerTool.ONReader.PageNodes
{
    [NodeName("Page")]
    public class ONPage : PageElement
    {
        public String pageXml;
        public ONPage()
        {
        }
        public ONPage(String pageXml)
        {
            this.pageXml = pageXml;
        }
    }
}
