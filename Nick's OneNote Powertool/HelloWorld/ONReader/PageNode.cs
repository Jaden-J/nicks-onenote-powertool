using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.Reflection;
using NicksPowerTool.ONReader.PageNodes;

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

        public void AddChildNode(PageNode node) {
            ChildPageNodes.Add(node);
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

        public T finishConstruction<T>(XmlNode node, ONPage page) where T : PageNode
        {
            _OwnerPage = page;
            return finishConstruction<T>(node);
        }

        public override string ToString()
        {
            String result = "";
            result += NodeName + ": Value=" + Node.Value + "; " + Attributes;
            return result;
        }
    }
}
