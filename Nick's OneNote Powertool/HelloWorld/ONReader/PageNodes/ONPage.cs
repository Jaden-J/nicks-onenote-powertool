using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Office.Interop.OneNote;

namespace NicksPowerTool.ONReader.PageNodes
{
    [NodeName("Page")]
    public class ONPage : PageElement
    {
        public String pageXml;
        public ONPage()
        {
        }

        public ONPage(String pageXml)
        {
            this.pageXml = pageXml;
        }

        public String PageID
        {
            get
            {
                return Attributes.GetAttributeValue("ID");
            }
        }

        public static String getActivePageID()
        {
            Window w = LoadNPT.getActiveWindow();
            if (w != null)
            {
                return w.CurrentPageId;
            }
            else
            {
                return null;
            }
        }
    }
}
