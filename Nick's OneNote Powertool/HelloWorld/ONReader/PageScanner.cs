using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using NicksPowerTool.ONReader.HiearchyNodes;
using System.Windows.Controls;

namespace NicksPowerTool.ONReader
{
    public class PageScanner
    {
        private Page page;
        private List<NodeHandler> handlers = new List<NodeHandler>();
        private List<ONNode> nodes = new List<ONNode>();

        private List<XmlNode> currentHierarchy = new List<XmlNode>();

        private XmlReader reader;
        private XmlDocument doc = new XmlDocument();

        public PageScanner(Page page)
        {
            this.page = page;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            reader = XmlReader.Create(new MemoryStream(Encoding.ASCII.GetBytes(page.pageXml)), settings);
            doc.Load(reader);
        }

        public void scan()
        {
            XmlNode node = doc;
            int levels = 0;
            bool elevated = false;
            do
            {
                if (node.NodeType != XmlNodeType.CDATA)
                {
                    handleNode(node);
                }

                //move through the document
                if (node.HasChildNodes && !elevated)
                {
                    node = node.FirstChild;
                    levels++;
                }
                else if (node.NextSibling != null)
                {
                    node = node.NextSibling;
                    elevated = false;
                }
                else
                {
                    node = node.ParentNode;
                    levels--;
                    elevated = true;
                }
            } while (node != null);
        }

        public void handleNode(XmlNode node)
        {
        }

        public void processChildren(XmlNode node)
        {
        }

        public bool isChildrenList(XmlNode node)
        {
            return (node.Name == NAME_OECHILDREN);
        }

        public bool isRecordedNode(XmlNode node)
        {
            //((object)node).GetType().
            return true;
        }

        private String NAME_OECHILDREN = "one:OECHildren";
    }
}
