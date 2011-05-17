using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NicksPowerTool.ONReader.HiearchyNodes
{
    
    public class Page : HierarchyNode
    {
        public String pageXml;
        public Page(String pageXml)
        {
            this.pageXml = pageXml;
        }
    }
}
