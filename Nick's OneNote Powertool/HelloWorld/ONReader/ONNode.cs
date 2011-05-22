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
        public ONNode ParentNode { get; set; }

        public Stack<ONNode> ParentNodesStack
        {
            get
            {
                List<ONNode> list = new List<ONNode>();
                ONNode temp = this;
                while (temp != null)
                {
                    list.Add(temp);
                }
                list.Reverse();
                return new Stack<ONNode>(list);
            }
        }
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
            this.Node = node;
            this._Attributes = new ONNodeAttributeSet(this);
            return (T)this;
        }

        public ONNode GetTopNode()
        {
            ONNode pn = this;
            while (pn.ParentNode != null)
            {
                pn = pn.ParentNode;
            }
            return pn;
        }
    }
}