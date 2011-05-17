using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NicksPowerTool.ONReader.PageNodes.PageNodeProperties
{
    [NodeName("GENERIC")]
    class GenericProperty : PageProperty
    {
        private String GenericNodeName;
        public String NodeName { get { return GenericNodeName;}}

        public GenericProperty(String nodeName)
        {
            this.GenericNodeName = nodeName;
        }
    }
}
