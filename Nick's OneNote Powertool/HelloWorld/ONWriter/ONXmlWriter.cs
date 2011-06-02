using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NicksPowerTool.ONReader;
using NicksPowerTool.ONReader.PageNodes;
using NicksPowerTool.ONReader.PageNodeAugmentation;
using System.Xml;

namespace NicksPowerTool.ONWriter
{
    class ONXmlWriter
    {

        public static void FillInBinary(ONPage page)
        {
            PageDiver pd = new PageDiver(page);

            pd.PageNodeHit += new PageDiver.PageNodeHitEventHandler(FillInBinaryAction);

            pd.Scan();
        }

        private static void FillInBinaryAction(PageNode node)
        {
            if (node is IHasBinaryData)
            {
                IHasBinaryData bdnode = (IHasBinaryData)node;
                bool result = bdnode.ReplaceCallbackWithData();
                if (!result) //if you fail
                {
                    System.Console.WriteLine("You didn't get data for: ");
                    System.Console.Write("\t" + node.NodeName);
                    foreach (PageNode parentNode in node.ParentNodesStack)
                    {
                        System.Console.Write(" => " + parentNode.NodeName);
                    }
                    System.Console.WriteLine("");
                }
            }
        }
    }
}
