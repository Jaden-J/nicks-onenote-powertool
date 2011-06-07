using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NicksPowerTool.ONReader.PageNodeParts;

namespace NicksPowerTool
{

    public abstract class ONNode
    {
        public XmlNode Node { get; set; }
        
        public bool Changed { get; set; }
        private ONNodeAttributeSet _Attributes;
        public ONNodeAttributeSet Attributes
        {
            get
            {
                return _Attributes;
            }
        }

        public ONNode()
        {
            Changed = false;
        }

        public T finishConstruction<T>(XmlNode node) where T : ONNode
        {
            if (node != null) this.Node = node;
            this._Attributes = new ONNodeAttributeSet(this);
            return (T)this;
        }

        public static DateTime ParseDateTime(string date)
        {
            DateTime.
        }
        /*
        public ONNode GetTopNode()
        {
            ONNode pn = this;
            while (pn.ParentNode != null)
            {
                pn = pn.ParentNode;
            }
            return pn;
        }*/
    }
}