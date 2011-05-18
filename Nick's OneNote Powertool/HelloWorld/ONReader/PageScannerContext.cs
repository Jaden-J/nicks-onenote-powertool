using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Xml;


namespace NicksPowerTool.ONReader
{
    public class PageScannerContext
    {
        #region Variables
        private PageScanner _pageScanner;
        private Stack<PageNode> _nodeStack;
        private StateType _state;
        private bool _elevated;
        private XmlNode _node;
        private PageNode _lastGeneratedNode;
        #endregion
        public PageScanner PageScanner
        {
            get { return _pageScanner; }
            set { _pageScanner = value; }
        }

        public Stack<PageNode> NodeStack
        {
            get { return _nodeStack; }
            set { _nodeStack = value; }
        }

        public enum StateType {NOT_READY, READY, SCANNING, DONE}

        [DefaultValue(PageScannerContext.StateType.READY)]
        public StateType State
        {
            get { return _state; }
            set { _state = value; }
        }

        [DefaultValue(false)]
        public bool Elevated
        {
            get { return _elevated; }
            set { _elevated = value; }
        }

        public XmlNode Node
        {
            get { return _node; }
            set { _node = value; }
        }

        public PageScannerContext(PageScanner ps) 
        {
            PageScanner = ps;
        }

        public PageNode LastGeneratedNode
        {
            get { return _lastGeneratedNode; }
            set { _lastGeneratedNode = value; }
        }
    }
}
