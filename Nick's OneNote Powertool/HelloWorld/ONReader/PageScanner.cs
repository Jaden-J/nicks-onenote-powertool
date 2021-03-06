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
        public ONPage Page;
        private String _RawXml;
        public String RawXml
        {
            get {
                return _RawXml;
            }
        }

        private List<NodeHandler> handlers = new List<NodeHandler>();
        private List<ONNode> nodes = new List<ONNode>();
        private PageScannerContext _pageScannerContext;

        private XmlReader reader;
        private XmlDocument doc = new XmlDocument();

        public List<PageElement> collectedElements;
        public List<PageProperty> collectedProperties;
        public List<GenericPageNode> collectedOther;

        public delegate void NodeCreatedEventHandler(PageNode pn, PageScannerContext context);
        public event NodeCreatedEventHandler NodeCreated;

        public List<PageNode> collectedPageNodes = new List<PageNode>();

        public PageScannerContext Context
        {
            get { return _pageScannerContext; }
        }


        //page must be filled with text already, used for debugging only
        public PageScanner(ONPage page)
        {
            _RawXml = page.pageXml;
            Initialize();
        }


        public PageScanner(String pageId)
        {
            String pageXml;
            LoadNPT.onApp.GetPageContent(pageId, out pageXml, Microsoft.Office.Interop.OneNote.PageInfo.piSelection);

            _RawXml = pageXml;
            Initialize();
        }

        private void Initialize()
        {
            _pageScannerContext = new PageScannerContext(this);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            reader = XmlReader.Create(new StringReader(RawXml), settings);
            doc.Load(reader);

            Context.State = PageScannerContext.StateType.READY;

            NodeCreated += new NodeCreatedEventHandler((n, c) =>
            {
                if (n is ONPage) Page = (ONPage)n;
            });
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

                if (!Context.Elevated && Context.Node.HasChildNodes)
                {
                    Context.NodeStack.Push(Context.LastGeneratedNode);
                    Context.Node = Context.Node.FirstChild;
                }
                else if (Context.Node.NextSibling != null) 
                {
                    if (!Context.Elevated) NodeCreated(Context.LastGeneratedNode, Context); //NodeCreated
                    //Completed. Going to next one. Didn't just finish a tree (not elevated). No children.
                    Context.Node = Context.Node.NextSibling;
                    Context.Elevated = false;
                }
                else //Node is finished. No siblings you've just finished its tree
                {
                    
                    try
                    {
                        if (!Context.Elevated) //If you're at the very bottom, so you 
                            //need to handle current node and the parent.
                        {
                            NodeCreated(Context.LastGeneratedNode, Context);
                        }

                        Context.Node = Context.Node.ParentNode;
                        Context.Elevated = true;

                        PageNode completed = Context.NodeStack.Pop();
                        NodeCreated(completed, Context); //NodeCreated
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
            PageNode pagenode = PageNodeFactory.GenerateNode(Context);

            if (pagenode is ONPage)
            {
                Context.Page = (ONPage)pagenode;
            }

            if (pagenode is PageElement)
            {
                PageElement element = (PageElement)pagenode;
                collectedElements.Add(element);
                collectedPageNodes.Add(element);

                if (Context.NodeStack.Peek() is PageElement)
                {
                    ((PageElement)Context.NodeStack.Peek()).
                        AddChildNode(element);
                }
            }

            else if (pagenode is PageProperty)
            {
                PageProperty property = (PageProperty)pagenode;
                collectedProperties.Add(property);
                collectedPageNodes.Add(property);

                Context.NodeStack.Peek().AddChildNode(property);
            }

            else
            {
                collectedOther.Add((GenericPageNode)pagenode);
                collectedPageNodes.Add(pagenode);
                if(Context.NodeStack.Count >0)
                    Context.NodeStack.Peek().AddChildNode(pagenode);
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
