using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NicksPowerTool.ONReader.PageNodeAugmentation;

namespace NicksPowerTool.ONReader.PageNodes.PageNodeProperties
{
    [NodeName("#cdata-section")]
    class CDataProperty : PageProperty
    {
        public CDataProperty() : base()
        {

        }

        public CDataProperty(IHasCData pn)
            : base((PageNode)pn)
        {
        }
    }
}
