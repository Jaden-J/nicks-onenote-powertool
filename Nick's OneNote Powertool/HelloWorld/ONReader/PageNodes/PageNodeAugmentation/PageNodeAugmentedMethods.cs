using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NicksPowerTool.ONReader.PageNodes.PageNodeAugmentation.Interfaces;
using NicksPowerTool.ONReader.PageNodes.PageNodeProperties;

namespace NicksPowerTool.ONReader.PageNodes.PageNodeAugmentation
{
    public static class PageNodeAugmentedMethods
    {       
        public static string GetBinaryDataString(this IHasBinaryData n) {
            CallbackIDProperty cip = (CallbackIDProperty)((PageNode)n).
                ChildPageNodes.Find(c => c.GetType().Equals(typeof(CallbackIDProperty)));
            String binaryString;
            LoadNPT.onApp.GetBinaryPageContent(cip.OwnerPage.PageID, cip.CallbackIDValue, out binaryString);
            return binaryString;
        }

        public static byte[] GetBinaryData(this IHasBinaryData n)
        {
            return Convert.FromBase64String(n.GetBinaryDataString());
        }
    }
}
