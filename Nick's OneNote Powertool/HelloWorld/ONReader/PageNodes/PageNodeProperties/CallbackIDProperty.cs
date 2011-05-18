using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NicksPowerTool.ONReader.PageNodes.PageNodeProperties
{
    [NodeName("CallbackID")]
    class CallbackIDProperty : PageProperty
    {
        public String CallbackIDValue
        {
            get
            {
                return Attributes.GetAttributeValueString("callbackID");
            }
        }
    }
}
