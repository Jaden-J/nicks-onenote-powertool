using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace NicksPowerTool.ONReader.Tools
{
    class SelectionTools
    {
        public static List<PageNode> GetSelectedHierarchy(PageNode page)
        {
            List<PageNode> result = new List<PageNode>();
            PageNode node = page;
            do {
                result.Add(node);
                node = node.ChildPageNodes.Find(n => n.Selected != PageNode.SelectedValue.NOT_SELECTED);
            } while (node != null);
            return result;
        }

        public static PageNode GetSelectedChild(PageNode page)
        {
            return GetSelectedHierarchy(page).Find(n => n.Selected == PageNode.SelectedValue.ALL);
        }

        
    }
}
