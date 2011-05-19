﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Generic;
using System.Linq;
using System.Text;

using Extensibility;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.OneNote;
using Microsoft.Office.Core;
using System.Windows.Forms;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Office.Interop.OneNote;
using System.IO;
using NicksPowerTool.ONReader.PageNodes;
using NicksPowerTool.ONReader;
using NicksPowerTool.ONReader.PageNodeAugmentation;


using System.Drawing.Imaging;


namespace NicksPowerTool
{
    public class Debugg : IQuickFilingDialogCallback
    {
        public Debugg()
        {
        }

        public static void debug()
        {
            Microsoft.Office.Interop.OneNote.ApplicationClass app = new ApplicationClass();
            Window w = app.Windows[0];

            ulong handle = w.WindowHandle;
            long lhandle = (long)handle;
            String handleHex = handle.ToString("X");
            String lhandleHex = lhandle.ToString("X");
            String other = "hi";
        }

        public String getHierarchy()
        {
            ApplicationClass app = new ApplicationClass();
            String strXML;
            app.GetHierarchy(null, HierarchyScope.hsPages, out strXML);

            return strXML;
        }

        public static void showHello(IRibbonControl control)
        {
            //string id = onApp.Windows.CurrentWindow.CurrentPageId;
            //MessageBox.Show("Current Page ID = " + id, "Hello World!");

            IQuickFilingDialog dialog = LoadNPT.onApp.QuickFiling();

            dialog.CheckboxText = "Return binary";
            dialog.CheckboxState = false;

            dialog.TreeDepth = HierarchyElement.hePages;
            dialog.AddButton("Select", HierarchyElement.hePages, HierarchyElement.hePages, true);
            dialog.Run(new Debugg());
        }

        public void OnDialogClosed(IQuickFilingDialog dialog)
        {
            if (dialog.PressedButton >= 0)
            {
                String pageId = dialog.SelectedItem;

                String pagexml;
                LoadNPT.onApp.GetPageContent(pageId, out pagexml, dialog.CheckboxState ?
                    PageInfo.piBinaryDataSelection : PageInfo.piSelection);

                DebugWin.ShowDebugXmlWindow(pagexml);
            }
        }

        public static void showBinary(IRibbonControl control)
        {
            //string id = onApp.Windows.CurrentWindow.CurrentPageId;
            //MessageBox.Show("Current Page ID = " + id, "Hello World!");

            try
            {
                String pageid = ONPage.getActivePageID();

                List<IHasBinaryData> binaryDataNodes = new List<IHasBinaryData>();
                PageScanner scanner = new PageScanner(pageid);
                scanner.NodeCreated += new PageScanner.NodeCreatedEventHandler((n, c) =>
                { if (n is IHasBinaryData) { binaryDataNodes.Add((IHasBinaryData)n); } });

                scanner.scan();

                String base64 = "";
                foreach (IHasBinaryData node in binaryDataNodes)
                {
                    base64 += ((PageNode)node).NodeName + ":\r\n";
                    base64 += "\tString length: " + node.GetBinaryDataString().Length + ".\t";
                    base64 += "\tBytes exported: " + node.GetBinaryData().Length + ".\t";
                    base64 += "\r\n\r\n";
                }

                DebugWin.ShowDebugStringWindow(base64);
            }
            catch (Exception e)
            {
                string output = e.Message + "\n" + e.StackTrace;
                DebugWin.ShowDebugStringWindow(output);
            }
        }
    }
}
