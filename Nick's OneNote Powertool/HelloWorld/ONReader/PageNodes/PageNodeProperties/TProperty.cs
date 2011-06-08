using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NicksPowerTool.ONReader.PageNodeAugmentation;

namespace NicksPowerTool.ONReader.PageNodes.PageNodeProperties
{
    [NodeName("T")]
    class TProperty : PageProperty, IHasCData
    {
        public string Value
        {
            get
            {
                CDataProperty cd = GetChildNode<CDataProperty>();
                if (cd == null)
                {
                    return "";
                }
                else
                {
                    return cd.Node.Value;
                }
            }

            set
            {
                CDataProperty cd = GetChildNode<CDataProperty>();
                if (cd == null)
                {
                    cd = new CDataProperty(this);
                }
                cd.Node.Value = value;
            }
        }
    }
}
