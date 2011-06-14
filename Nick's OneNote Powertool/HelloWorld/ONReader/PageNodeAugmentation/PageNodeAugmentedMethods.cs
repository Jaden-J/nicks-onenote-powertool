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
                dp.Node.InnerXml = data;

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

        #region Tagable
        public static List<TagProperty> GetTags(this Tagable inode)
        {

        }

        public static bool AddTag(this Tagable inode, TagProperty tagToAdd) {
        
        }
        #endregion

        #region IHasText
        public static string GetText(this IHasText inode) //combine all the text regardless 
            // of whether you've got selection stuff going on
            // Might also want to reselect if any of it was selected
        {
            PageNode n = (PageNode)inode;
            List<TProperty> tNodes = n.GetChildNodes<TProperty>();

            StringBuilder sb = new StringBuilder();

            if (tNodes.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (TProperty t in tNodes)
                {
                    sb.Append(t.GetCDataValue());
                }
            }
            return sb.ToString();
        }

        public static void SetText(this IHasCData inode, String text)
        {

        }
        #endregion

        #region IHasCData
        public static string GetCDataValue(this IHasCData inode)
        {
            CDataProperty cd = ((PageNode)inode).GetChildNode<CDataProperty>();
            if (cd == null)
            {
                return "";
            }
            else
            {
                return cd.Node.Value;
            }
        }

        public static void SetCDataValue(this IHasCData inode, string val)
        {
            CDataProperty cd = ((PageNode)inode).GetChildNode<CDataProperty>();
            if (cd == null)
            {
                cd = new CDataProperty(inode);
            }
            cd.Node.Value = val;
        }
        #endregion
    }
}
