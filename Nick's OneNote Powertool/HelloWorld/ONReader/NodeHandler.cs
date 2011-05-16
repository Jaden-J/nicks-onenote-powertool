using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NicksPowerTool.ONReader
{
    abstract class NodeHandler
    {

        PageScanner scanner;

        public NodeHandler(PageScanner scanner) 
        {
            this.scanner = scanner;
        }

        public abstract void Handle(XmlNode currentNode);

        public abstract Boolean canHandle(XmlNode currentNode);


    }
}
