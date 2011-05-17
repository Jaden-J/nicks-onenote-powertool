using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NicksPowerTool.ONReader.PageNodes.PageNodeProperties
{
    class GenericProperty : PageNodeProperty
    {
        private String GenericNodeName;
        public override String NodeName { get { return GenericNodeName;}}

        public GenericProperty(String nodeName)
        {
            this.GenericNodeName = nodeName;
        }
    }
}
