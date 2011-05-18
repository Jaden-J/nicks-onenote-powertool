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

        public String GetAttributeValueString(String localName) {
            XmlNode x = XmlAttributes.GetNamedItem(localName);
            return (x != null) ? x.Value : null;
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