using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using NicksPowerTool.ONReader.PageNodes;
using NicksPowerTool.ONReader;

namespace NicksPowerTool.ONWriter
{
    class ONPageContentUpdater
    {
        public delegate bool IncludeNodeDelegate(PageNode node);
        public IncludeNodeDelegate IncludeNode;

        public void generateWritableDocument(ONPage page)
        {
            XmlDocument result = new XmlDocument();

        }
    }
}
