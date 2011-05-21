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
