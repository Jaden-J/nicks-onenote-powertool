using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NicksPowerTool
{
    public partial class DebugWin : Form
    {
        public String DebugText
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

        public DebugWin()
        {
            InitializeComponent();
        }
    }
}
