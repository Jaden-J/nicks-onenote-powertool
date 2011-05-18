using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NicksPowerTool.ONReader.PageNodes.PageNodeAugmentation.Interfaces;

namespace NicksPowerTool.ONReader.PageNodes.PageNodeAugmentation
{
    public static class PageNodeAugmentedMethods
    {       
        public static string GetBinaryDataString(this IHasBinaryData n) {
            PageNode node = ((PageNode)n);

        }

        public static byte[] GetBinaryData(this IHasBinaryData n)
        {
            return Convert.FromBase64String(n.GetBinaryDataString());
        }
    }
}
