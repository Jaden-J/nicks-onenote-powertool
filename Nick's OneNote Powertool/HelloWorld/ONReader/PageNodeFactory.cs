using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace NicksPowerTool.ONReader
{
    public class PageNodeFactory
    {
        public static String OneNote_Node_Name_Prefix = "one:";
        private static Type PageElementType = typeof(PageElement);
        private static Type PagePropertyType = typeof(PageProperty);
        public static Dictionary<String, Type> PageElementDictionary;
        public static Dictionary<String, Type> PagePropertyDictionary;

        static PageNodeFactory() {
            PageElementDictionary = GeneratePageElementDictionary();
            PagePropertyDictionary = GeneratePagePropertyDictionary()
        }

        public static PageElement GenerateNode(XmlNode node)
        {
            if (isElement(node))
            {
                Type elementType;
                Type[] types = {};
                object[] objects = {};
                if (PageElementDictionary.TryGetValue(getName(node), out elementType))
                {
                    ConstructorInfo c = elementType.GetConstructor(types);
                    return ((PageElement)c.Invoke(objects)).finishConstruction<PageElement>(node);
                }
                else
                {
                    return null;
                }
            }
            else if (isProperty(node))
            {
                Type elementType;
                Type[] types = { typeof(XmlNode) };
                object[] objects = {node};
                if (PagePropertyDictionary.TryGetValue(getName(node), out elementType))
                {
                    ConstructorInfo c = elementType.GetConstructor(types);
                    return (PageProperty)c.Invoke(objects);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                System.Console.WriteLine("WAS NOT PROPERTY OR ELEMENT: " + node.Name);
                return null; /////GENERIC???
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
                object[] pagePropertyNameAttributes = t.GetCustomAttributes(PagePropertyType, false);
                foreach (object o in pagePropertyNameAttributes)
                {
                    PagePropertyDictionary.Add(((NodeName)o).Name, t);
                }
            }
            return dict;
        }

        public static bool isProperty(XmlNode node)
        {
            return isOneNoteNode(node) && PagePropertyDictionary.ContainsKey(getName(node));
        }

        public static bool isElement(XmlNode node)
        {
            return isOneNoteNode(node) && PageElementDictionary.ContainsKey(getName(node));
        }

        public static bool isOneNoteNode(XmlNode node)
        {
            return node.Name.StartsWith("one:");
        }

        public static String getName(XmlNode node)
        {
            return node.Name.Substring(4);
        }
    }
}
