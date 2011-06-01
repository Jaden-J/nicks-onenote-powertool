using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using NicksPowerTool.ONReader.PageNodes;
using NicksPowerTool.ONReader.PageNodeAugmentation;

namespace NicksPowerTool.ONReader
{
    public class PageNodeFactory
    {
        public static String OneNote_Node_Name_Prefix = "one:";
        private static Type PageElementType = typeof(PageElement);
        private static Type PagePropertyType = typeof(PageProperty);
        private static Dictionary<String, Type> _PageElementDictionary;
        private static Dictionary<String, Type> _PagePropertyDictionary;
        public static Dictionary<String, Type> PageElements
        {
            get
            {
                if (_PageElementDictionary == null) _PageElementDictionary = GeneratePageElementDictionary();
                return _PageElementDictionary;
            }
        }

        public static Dictionary<String, Type> PageProperties
        {
            get
            {
                if (_PagePropertyDictionary == null) _PagePropertyDictionary = GeneratePagePropertyDictionary();
                return _PagePropertyDictionary;
            }
        }

        public static PageNode GenerateNode(PageScannerContext context)
        {
            XmlNode node = context.Node;

            if (node == null) {
                return null;
            }

            if (isElement(node))
            {
                Type elementType;
                Type[] types = {};
                object[] objects = {};
                if (PageElements.TryGetValue(node.LocalName, out elementType))
                {
                    ConstructorInfo c = elementType.GetConstructor(types);
                    return ((PageElement)c.Invoke(objects)).finishConstruction<PageElement>(context);
                }
                else
                {
                    return null;
                }
            }
            else if (isProperty(node))
            {
                Type elementType;
                Type[] types = {};
                object[] objects = {};
                if (PageProperties.TryGetValue(node.LocalName, out elementType))
                {
                    ConstructorInfo c = elementType.GetConstructor(types);
                    return ((PageProperty)c.Invoke(objects)).finishConstruction<PageProperty>(context);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return new GenericPageNode().finishConstruction<GenericPageNode>(context);
            }
        }

        public static Dictionary<String, Type> GeneratePageElementDictionary()
        {
            Dictionary<String, Type> dict = new Dictionary<String, Type>();
            List<Type> types = Assembly.GetCallingAssembly().GetTypes().Where(type => type.IsSubclassOf(PageElementType)).ToList();
            foreach (Type t in types)
            {
                object[] pageElementNameAttributes = t.GetCustomAttributes(typeof(NodeName), false);
                foreach (object o in pageElementNameAttributes)
                {
                    dict.Add(((NodeName)o).Name, t);
                }
            }
            return dict;
        }

        public static Dictionary<String, Type> GeneratePagePropertyDictionary()
        {
            Dictionary<String, Type> dict = new Dictionary<String, Type>();

            List<Type> propertyTypes = Assembly.GetCallingAssembly().GetTypes().Where(type => type.IsSubclassOf(PagePropertyType)).ToList();
            foreach (Type t in propertyTypes)
            {
                object[] pagePropertyNameAttributes = t.GetCustomAttributes(typeof(NodeName), false);
                foreach (object o in pagePropertyNameAttributes)
                {
                    dict.Add(((NodeName)o).Name, t);
                }
            }
            return dict;
        }

        public static bool isProperty(XmlNode node)
        {
            return isOneNoteNode(node) && PageProperties.ContainsKey(node.LocalName);
        }

        public static bool isElement(XmlNode node)
        {
            return isOneNoteNode(node) && PageElements.ContainsKey(node.LocalName);
        }

        public static bool isOneNoteNode(XmlNode node)
        {
            return node.Name.StartsWith("one:") || node.NodeType == XmlNodeType.CDATA;
        }

        public static T ConstructNewPageNode<T>(PageNode parent) where T : PageNode
        {
            Type t = typeof(T);
            if (t.GetInterfaces().Contains(typeof(ICanConstruct)))
            {
                Type[] prams = {typeof(PageNode)};
                Object[] input = {parent};
                ConstructorInfo c = t.GetConstructor(prams);
                Object o = c.Invoke(input);
                T newNode = (T)o;
                return newNode;
            }
            else
            {
                return null;
            }
        }
    }
}
