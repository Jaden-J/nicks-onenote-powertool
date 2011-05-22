using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

namespace NicksPowerTool
{
    public partial class DebugWin : Form
    {
        public String DebugXmlText
        {
            get
            {
                return debugText.Text;
            }
            set
            {
                String xml = value;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                StringBuilder sb = new StringBuilder();
                System.IO.TextWriter tw = new System.IO.StringWriter(sb);
                XmlTextWriter xtw = new XmlTextWriter(tw);
                xtw.Formatting = Formatting.Indented;
                doc.Save(xtw);
                xtw.Close();


                debugText.Text = sb.ToString();
            }
        }

        public String DebugText
        {
            get
            {
                return debugText.Text;
            }
            set
            {
                debugText.Text = value;
            }
        }

        public DebugWin()
        {
            InitializeComponent();
        }

        [STAThread]
        public static void ShowDebugXmlWindow(String xmlText)
        {
            DebugWin win = new DebugWin();
            win.DebugXmlText = xmlText;
            StartDebugWindowThread(win);
        }

        [STAThread]
        private static void StartDebugWindowThread(DebugWin di)
        {
            Thread t = new Thread(new ThreadStart(() => { 
                di.Show();
                di.Focus();
                di.BringToFront();
                di.Activate();
                System.Windows.Threading.Dispatcher.Run(); }));
            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = true;
            t.Start();
        }

        [STAThread]
        public static void ShowDebugStringWindow(String s)
        {
            DebugWin di = new DebugWin();
            di.DebugText = s;
            StartDebugWindowThread(di);
        }
    }
}
