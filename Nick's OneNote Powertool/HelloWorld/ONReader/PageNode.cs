using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml;

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
                Object o = (object)_PageNodes.FindAll(match => match.GetType().Equals(typeof(PageProperty)));
                return (List<PageProperty>)o;
            }
        }
        public List<PageNode> PageNodes
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
                return PageNodes.FindAll(match => !match.GetType().Equals(typeof(PageProperty)) && !match.GetType().Equals(typeof(PageElement)));
            }
        }


        public enum SelectedValue { NOT_SELECTED, PARTIAL, ALL }

        [DefaultValue(SelectedValue.NOT_SELECTED)]
        public SelectedValue Selected
        {
            get
            {
                return SelectedValue.NOT_SELECTED;
            }
        }

        public void AddProperty(PageProperty property) {
            PageNodes.Add(property);
        }
    }
}
