using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;

namespace NicksPowerTool.ONReader.PageNodeParts
{
    public class ONNodeAttributeSet
    {
        private ONNode Node;
        private XmlNode XNode;

        public XmlAttributeCollection XmlAttributes
        {
            get
            {
                return XNode.Attributes;
            }
        }

        public ONNodeAttributeSet(ONNode node)
        {
            XNode = node.Node;
        }

        public String GetAttributeValue(String localName) {
            if (XmlAttributes == null)
            {
                return "";
            }
            XmlNode x = XmlAttributes.GetNamedItem(localName);
            return (x != null) ? x.Value : null;
        }

        public bool SetAttributeValue(String localName, bool create, String value)
        {
            XmlNode x = XmlAttributes.GetNamedItem(localName);
            if (x != null)
            {
                x.Value = value;
                return true;
            }
            else if (create)
            {
                XmlAttribute a = Node.Node.OwnerDocument.CreateAttribute(localName);
                a.Value = value;
                XmlAttributes.Append(a);
                return true;
            }
            else
            {
                return false;
            }
        }

        public double GetAttributeValueDouble(String localName) {
            String s = GetAttributeValue(localName);
            double val = Double.Parse(s);
            return val;
        }

        public bool GetAttributeValueBool(String localName)
        {
            String s = GetAttributeValue(localName);
            bool val = Boolean.Parse(s);
            return val;
        }

        public int GetAttributeValueInt(String localName)
        {
            String s = GetAttributeValue(localName);
            int val = Int32.Parse(s);
            return val;
        }

        public void AddOrChangeAttribute(String attributeName, String attributeValue)
        {
            XmlDocument doc = XNode.OwnerDocument;
            XmlAttribute attr = GetAttribute(attributeName);
            if (attr == null)
            {
                attr = doc.CreateAttribute(attributeName, XNode.NamespaceURI);
            }

            attr.Value = attributeValue;
        }

        public bool ContainsAttribute(String attributeName)
        {
            return GetAttribute(attributeName) == null;
        }

        private XmlAttribute GetAttribute(String attributeName)
        {
            foreach (XmlNode n in XNode.Attributes)
            {
                if (n.Name == attributeName)
                {
                    return n as XmlAttribute;
                }
            }
            return null;
        }

        public override string ToString()
        {
            String result = "Attributes: ";
            foreach (XmlNode node in XmlAttributes)
            {
                result += node.LocalName + "=" + node.Value + "; ";
            }
            return result;
        }
    }
}
/*
public void Add(K key, V value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(K key)
        {
            throw new NotImplementedException();
        }

        public ICollection<K> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(K key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(K key, out V value)
        {
            throw new NotImplementedException();
        }

        public ICollection<V> Values
        {
            get { throw new NotImplementedException(); }
        }

        public V this[K key]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public new IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            throw new NotImplementedException();
        }*/