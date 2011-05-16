using System;
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
using System.IO;


using System.Drawing.Imaging;


namespace NicksPowerTool
{
    class Debugg
    {
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
    }
}
