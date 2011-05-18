using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NicksPowerTool.ONReader
{
    public abstract class PageNode : ONNode
    {
        private List<PageProperty> properties = new List<PageProperty>();
        private List<PageNode> _undefinedSubNodes = new List<PageNode>();
        public List<PageProperty> PageProperties
        {
            get
            {
                return properties;
            }
        }

        public void AddProperty(PageProperty p)
        {
            PageProperties.Add(p);
        }

        public List<PageNode> UndefinedSubNodes
        {
            get { return _undefinedSubNodes; }
            set { _undefinedSubNodes = value; }
        }

        public void AddUndefinedSubNode(PageNode p)
        {
            UndefinedSubNodes.Add(p);
        }
    }
}
