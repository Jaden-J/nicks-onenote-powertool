﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NicksPowerTool.ONReader
{
    public abstract class PageElement : PageNode
    {
        public PageElement() : base() { } 
        public PageElement(PageNode pn) : base(pn) {}
    }
}
