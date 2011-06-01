using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NicksPowerTool.ONReader;
using NicksPowerTool.ONReader.PageNodes;
using NicksPowerTool.ONReader.PageNodeAugmentation;
using System.Xml;

namespace NicksPowerTool.ONWriter
{
    class ONXmlWriter
    {

        public ONPage FillInBinary(ONPage page)
        {
            XmlNode node = page.Node.CloneNode(true);
            XmlDocument doc = new XmlDocument();
        }
    }
}
