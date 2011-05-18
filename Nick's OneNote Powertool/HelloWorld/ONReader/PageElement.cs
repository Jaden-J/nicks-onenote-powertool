using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NicksPowerTool.ONReader
{
    public abstract class PageElement : PageNode
    {
        private List<PageElement> children = new List<PageElement>();
        public List<PageElement> ChildElements
        {
            get
            {
                return children;
            }
        }

        public void addChildElement(PageElement e)
        {
            children.Add(e);
        }
    }
}
