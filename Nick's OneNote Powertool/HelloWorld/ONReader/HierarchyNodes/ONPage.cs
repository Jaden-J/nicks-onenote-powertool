﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NicksPowerTool.ONReader.HiearchyNodes
{
    
    public class ONPage : HierarchyNode
    {
        public String pageXml;
        public override String NodeName
        {
            get
            {
                return "Page";
            }
        }
        public ONPage(String pageXml)
        {
            this.pageXml = pageXml;
        }
    }
}
