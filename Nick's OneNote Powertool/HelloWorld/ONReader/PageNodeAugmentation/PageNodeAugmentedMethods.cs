using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NicksPowerTool.ONReader.PageNodes.PageNodeProperties;
using System.Windows;

namespace NicksPowerTool.ONReader.PageNodeAugmentation
{
    public static class PageNodeAugmentedMethods
    {
        #region IHasBinaryData
        public static string GetBinaryDataString(this IHasBinaryData n) {
            CallbackIDProperty cip = ((PageNode)n).GetChildNode<CallbackIDProperty>();
            DataProperty dp = ((PageNode)n).GetChildNode<DataProperty>();
            if (cip != null)
            {
                String binaryString = "";
                LoadNPT.onApp.GetBinaryPageContent(cip.OwnerPage.PageID, cip.CallbackIDValue, out binaryString);
                return binaryString;
            }
            else if (dp != null)
            {
                return dp.Node.Value;
            }
            else
            {
                return null;
            }
        }

        public static byte[] GetBinaryData(this IHasBinaryData n)
        {
            String s = n.GetBinaryDataString();
            if (s != null)
            {
                return Convert.FromBase64String(n.GetBinaryDataString());
            }
            else
            {
                return null;
            }
        }

        public static bool ReplaceCallbackWithData(this IHasBinaryData n)
        {
            PageNode pn = ((PageNode)n);
            CallbackIDProperty cip = ((PageNode)n).GetChildNode<CallbackIDProperty>();
            if (cip != null)
            {
                String data = n.GetBinaryDataString();

                DataProperty dp = new DataProperty(pn);
                dp.Node.Value = data;

                pn.RemoveChildNode(cip);

                return true;
            }
            else
            {
                return false;
            }
        }

        /*public static SetBinaryData(this IHasBinaryData n, byte[] data) {
            
        }*/
        #endregion

        #region IHasPosition
        public static PositionProperty GetPositionProperty(this IHasPosition inode)
        {
            PageNode node = (PageNode)inode;
            return node.GetChildNode<PositionProperty>();
        }

        public static Point GetLocation(this IHasPosition inode) {
            return inode.GetPositionProperty().Location2D;
        }

        public static int GetZLevel(this IHasPosition inode)
        {
            return inode.GetPositionProperty().Z;
        }
        #endregion

        #region IHasSize
        public static SizeProperty GetSizeProperty(this IHasSize inode)
        {
            PageNode node = (PageNode)inode;
            return node.GetChildNode<SizeProperty>();
        }

        public static Size GetSize(this IHasSize inode) {
            return inode.GetSizeProperty().Size;
        }

        #endregion

        #region IHasPageArea
        public static Rect GetRect(this IHasPageArea inode) {
            Point p = inode.GetLocation();
            Size s = inode.GetSize();
            Rect r = new Rect(p, s);
            return r;
        }
        #endregion

        #region IHasRelativeArea
        public static Rect GetRelativePosition(this IHasRelativeArea inode)
        {
            PageNode node = (PageNode)inode;
            double x = node.Attributes.GetAttributeValueDouble("x");
            double y = node.Attributes.GetAttributeValueDouble("y");

            double width = node.Attributes.GetAttributeValueDouble("width");
            double height = node.Attributes.GetAttributeValueDouble("height");

            return new Rect(x, y, width, height);
        }
        #endregion
    }
}
