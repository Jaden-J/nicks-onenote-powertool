﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using NicksPowerTool.ONReader.PageNodes;
using System.Windows.Controls;

namespace NicksPowerTool.ONReader
{
    public class PageScanner
    {
        private ONPage page;
        private List<NodeHandler> handlers = new List<NodeHandler>();
        private List<ONNode> nodes = new List<ONNode>();
        private PageScannerContext _pageScannerContext;

        private XmlReader reader;
        private XmlDocument doc = new XmlDocument();

        public List<PageElement> collectedElements;
        public List<PageProperty> collectedProperties;
        public List<GenericPageNode> collectedOther;

        public List<PageNode> collectedPageNodes = new List<PageNode>();

        public PageScannerContext Context
        {
            get { return _pageScannerContext; }
        }

        public PageScanner(ONPage page)
        {
            this.page = page;
            _pageScannerContext = new PageScannerContext(this);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            reader = XmlReader.Create(new StringReader(page.pageXml), settings);
            doc.Load(reader);

            Context.State = PageScannerContext.StateType.READY;
        }

        public void scan()
        {
            if (Context.State != PageScannerContext.StateType.READY) throw new InvalidOperationException();

            collectedOther = new List<GenericPageNode>();
            collectedProperties = new List<PageProperty>();
            collectedElements = new List<PageElement>();
            collectedPageNodes = new List<PageNode>();

            Context.NodeStack = new Stack<PageNode>();
            Context.Node = doc;

            Context.State = PageScannerContext.StateType.SCANNING;

            do
            {
                if (!Context.Elevated)
                {
                    Context.LastGeneratedNode = handleNode(Context.Node);
                }

                if (Context.Node.HasChildNodes && !Context.Elevated)
                {
                    Context.NodeStack.Push(Context.LastGeneratedNode);
                    Context.Node = Context.Node.FirstChild;
                }
                else if (Context.Node.NextSibling != null)
                {
                    Context.Node = Context.Node.NextSibling;
                    Context.Elevated = false;
                }
                else
                {
                    Context.Node = Context.Node.ParentNode;
                    Context.Elevated = true;
                    try
                    {
                        Context.NodeStack.Pop();
                    }
                    catch (InvalidOperationException e)
                    {
                        break;
                    }
                }
            } while (Context.Node != null);

            finish();
        }


        public PageNode handleNode(XmlNode xmlnode)
        {
            PageNode pagenode = PageNodeFactory.GenerateNode(xmlnode);
            if (pagenode is PageElement)
            {
                PageElement element = (PageElement)pagenode;
                collectedElements.Add(element);
                collectedPageNodes.Add(element);

                if (Context.NodeStack.Peek() is PageElement)
                {
                    ((PageElement)Context.NodeStack.Peek()).
                        addChildElement(element);
                }
            }

            else if (pagenode is PageProperty)
            {
                PageProperty property = (PageProperty)pagenode;
                collectedProperties.Add(property);
                collectedPageNodes.Add(property);

                Context.NodeStack.Peek().AddProperty(property);
            }

            else
            {
                collectedOther.Add((GenericPageNode)pagenode);
                collectedPageNodes.Add(pagenode);
                if(Context.NodeStack.Count >0)
                    Context.NodeStack.Peek().AddUndefinedSubNode(pagenode);
            }
            
            return pagenode;
        }

        public bool isRecordedNode(XmlNode node)
        {
            //((object)node).GetType().
            return true;
        }

        public void finish()
        {
            Context.State = PageScannerContext.StateType.DONE;
        }
    }
}
