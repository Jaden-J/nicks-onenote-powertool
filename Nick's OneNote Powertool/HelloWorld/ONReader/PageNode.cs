using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.Reflection;
using NicksPowerTool.ONReader.PageNodes;
using System.Collections;

namespace NicksPowerTool.ONReader
{
    public abstract class PageNode : ONNode
    {
        private List<PageNode> _PageNodes = new List<PageNode>();

        public List<PageProperty> PageProperties
        {
            get
            {
                //CHEATING!!!!
                Object o = (object)ChildPageNodes.FindAll(match => match.GetType().Equals(typeof(PageProperty)));
                return (List<PageProperty>)o;
            }
        }

        public String NodeName
        {
            get
            {
                return ((NodeName)this.GetType().GetCustomAttributes(typeof(NodeName), false).First()).Name;
            }
        }

        public List<PageNode> ChildPageNodes
        {
            get
            {
                return _PageNodes;
            }
        }

        public List<PageNode> UndefinedSubNodes
        {
            get
            {
                return ChildPageNodes.FindAll(match => !match.GetType().Equals(typeof(PageProperty)) && !match.GetType().Equals(typeof(PageElement)));
            }
        }

        public PageNode NextSibling
        {
            get
            {
                try
                {
                    return ParentNode.ChildPageNodes[ParentNode.ChildPageNodes.IndexOf(this) + 1];
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public PageNode PreviousSibling
        {
            get
            {
                try
                {
                    return ParentNode.ChildPageNodes[ParentNode.ChildPageNodes.IndexOf(this) - 1];
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public enum SelectedValue { NOT_SELECTED, PARTIAL, ALL }

        public SelectedValue Selected
        {
            get
            {
                switch (Attributes.GetAttributeValueString("selected")) {
                    case "partial":
                        return SelectedValue.PARTIAL;
                    case "all":
                        return SelectedValue.ALL;
                    default:
                        return SelectedValue.NOT_SELECTED;
                }
            }
        }
        private ONPage _OwnerPage;
        public ONPage OwnerPage
        {
            get
            {
                return _OwnerPage;
            }
        }

        public PageNode ParentNode { get; set; }

        public Stack<PageNode> ParentNodesStack
        {
            get
            {
                List<PageNode> list = new List<PageNode>();
                PageNode temp = this;
                while ((temp = temp.ParentNode) != null)
                {
                    list.Add(temp);
                }
                list.Reverse();
                return new Stack<PageNode>(list);
            }
        }

        //CONSTRUCTOR
        public PageNode() : base() { }

        public PageNode(PageNode parent) : base()
        {
            XmlDocument doc = parent.Node.OwnerDocument;
            XmlElement e = doc.CreateElement(parent.Node.Prefix, NodeName, parent.Node.NamespaceURI);

            Node = e;
            parent.AddChildNode(this, true);
            //YOU SHOULD MAKE ATTRIBUTES FOR THIS FOR AUTOMATIC DEFAULT CONSTRUCTION
        }

        public void AddChildNode(PageNode node) {
            AddChildNode(node, false);
        }

        public void AddChildNode(PageNode child, Boolean adaptXml)
        {
            ChildPageNodes.Add(child);

            if (adaptXml)
            {
                Node.AppendChild(child.Node);

                PageScannerContext childContext = GetContextForChild(child);
                child.finishConstruction<PageNode>(childContext);
            }
        }

        public void RemoveChildNode(PageNode node)
        {
            Node.RemoveChild(node.Node);
            _PageNodes.Remove(node);
        }

        public PageNode GetChildNode(String localName)
        {
            foreach (PageNode n in ChildPageNodes)
            {
                if (n.NodeName.Equals(localName)) return n;
            }
            return null;
        }

        public T GetChildNode<T>() where T : PageNode
        {
            foreach (PageNode n in ChildPageNodes)
            {
                if (n is T) return n as T;
            }
            return null;
        }

        public List<T> GetChildNodes<T>() where T : PageNode
        {
            List<T> result = new List<T>();
            foreach (PageNode n in ChildPageNodes)
            {
                if (n is T) result.Add(n as T);
            }
            return result;
        }

        public T finishConstruction<T>(PageScannerContext context) where T : PageNode
        {
            _OwnerPage = context.Page;
            try
            {
                ParentNode = context.NodeStack.Peek();
            }
            catch (Exception e)
            {
                ParentNode = null;
            }
            return finishConstruction<T>(context.Node);
        }

        public PageScannerContext GetContextForChild(PageNode child) //WON'T INCLUDE A NODE
        {
            PageScannerContext result = new PageScannerContext(null); //Oh god this code is so horrible

            Stack<PageNode> ps = ParentNodesStack;
            ps.Push(this);
            result.NodeStack = ps;

            result.Node = child.Node; //So make sure to set the node first!

            result.Page = OwnerPage;

            return result;
        }

        public override string ToString()
        {
            String result = "";
            result += NodeName + ": Value=" + Node.Value + "; " + Attributes;
            return result;
        }
    }
}
