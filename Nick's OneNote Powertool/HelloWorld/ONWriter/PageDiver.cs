using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NicksPowerTool.ONReader.PageNodes;
using NicksPowerTool.ONReader;

namespace NicksPowerTool.ONWriter
{
    class PageDiver
    {
        private PageNode _RootNode;
        public PageNode RootNode {
            get
            {
                return _RootNode;
            }
        }

        public delegate void PageNodeHitEventHandler(PageNode hitNode);
        public event PageNodeHitEventHandler PageNodeHit;

        public PageDiver(PageNode root)
        {
            _RootNode = root;
        }
        
        public void Scan()
        {
            bool Elevated = false;
            PageNode CurrentNode = RootNode;

            do
            {
                //Register a hit
                if (!Elevated)
                {
                    PageNodeHit(CurrentNode);
                }

                if (!Elevated && CurrentNode.ChildPageNodes.Count > 0)
                {
                    CurrentNode = CurrentNode.ChildPageNodes.First();
                }
                else if (CurrentNode.NextSibling != null)
                {
                    CurrentNode = CurrentNode.NextSibling;
                    Elevated = false;
                }
                else
                {
                    CurrentNode = CurrentNode.ParentNode;
                    Elevated = true;
                }
            } while (CurrentNode != RootNode || !Elevated);
        }
    }
}
