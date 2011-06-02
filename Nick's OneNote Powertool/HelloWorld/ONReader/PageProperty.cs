using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NicksPowerTool.ONReader
{
    public abstract class PageProperty : PageNode
    {
        public PageProperty() : base() { } 
        public PageProperty(PageNode pn) : base(pn) {}
    }
}
