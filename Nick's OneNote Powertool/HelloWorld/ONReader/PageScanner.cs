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
        private ONPage page;
        private List<NodeHandler> handlers = new List<NodeHandler>();
        private List<ONNode> nodes = new List<ONNode>();

        private List<XmlNode> currentHierarchy = new List<XmlNode>();

        private XmlReader reader;
        private XmlDocument doc = new XmlDocument();

        public PageScanner(ONPage page)
        {
            this.page = page;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            reader = XmlReader.Create(new StringReader(page.pageXml), settings);
            doc.Load(reader);
        }

        public void scan()
        {
            XmlNode node = doc;
            int levels = 0;
            bool elevated = false;
            do
            {
                if (node.NodeType != XmlNodeType.CDATA && PageNodeFactory.isElement(node))
                {
                    handleNode(node);
                }
                else if (!PageNodeFactory.isElement(node) && !PageNodeFactory.isElement(node))
                {
                    System.Console.WriteLine("NOT PROPERTY OR ELEMENT: " + node.Name);
                }

                iterateNode(node, out node, levels, out levels, elevated, out elevated);
                //move through the document
                /*
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
                }*/
            } while (node != null);
        }

        public void iterateNode(XmlNode nodein, out XmlNode nodeout, int levelin, out int levelout, bool elevatedin, out bool elevatedout)
        {
            //move through the document
            if (nodein.HasChildNodes && !elevatedin)
            {
                nodeout = nodein.FirstChild;
                levelout = ++levelin;
                elevatedout = false;
            }
            else if (nodein.NextSibling != null)
            {
                nodeout = nodein.NextSibling;
                levelout = levelin;
                elevatedout = false;
            }
            else
            {
                nodeout = nodein.ParentNode;
                levelout = --levelin;
                elevatedout = true;
            }
        }

        public void handleNode(XmlNode xmlnode)
        {
            *PageElement pagenode = PageNodeFactory.GenerateNode(xmlnode);
            processChildren(pagenode);
        }

        public void processChildren(PageElement node)
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
