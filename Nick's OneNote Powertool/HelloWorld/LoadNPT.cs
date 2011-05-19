using System;
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
using System.Xaml;
using System.Threading;
using System.Collections;
using NicksPowerTool.ONReader.PageNodes;
using NicksPowerTool.ONReader;
using NicksPowerTool.ONReader.PageNodeAugmentation;

//61139959-A5E4-4261-977A-6262429033EB
namespace NicksPowerTool
{
	[GuidAttribute("E0BDB923-AD41-4BA1-9311-252509FAD397"), ProgId("NicksPowerTool.LoadNPT")]
	public class LoadNPT : IDTExtensibility2, IRibbonExtensibility
	{
		#region IDTExtensibility2 Members

		public static ApplicationClass onApp;// = new ApplicationClass();
		String pageId;
		bool debugging = true;

		public LoadNPT()
		{
			String text = "LoadNPT created";
			if (debugging) MessageBox.Show(text);
		}

		public void OnConnection(object Application, ext_ConnectMode ConnectMode, object AddInInst, ref Array custom)
		{
			/*
				For debugging, it is useful to have a MessageBox.Show() here, so that execution is paused while you have a chance to get VS to 'Attach to Process' 
			*/
			//if (debugging) MessageBox.Show("Starting the plugin");
			onApp = (ApplicationClass)Application;
		}
		public void OnDisconnection(Extensibility.ext_DisconnectMode disconnectMode, ref System.Array custom)
		{
			//Clean up. Application is closing
			onApp = null;
			pageId = null;
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}
		public void OnBeginShutdown(ref System.Array custom)
		{
			if (onApp != null)
				onApp = null;
			if (pageId != null) 
				pageId = null;

		}
		public void OnStartupComplete(ref Array custom) {
		}

		public void OnAddInsUpdate(ref Array custom) { }

		#endregion

		#region IRibbonExtensibility Members

		/// <summary>
		/// Called at the start of the running of the add-in. Loads the ribbon
		/// </summary>
		public string GetCustomUI(string RibbonID)
		{
			return Properties.Resources.ribbon;
		}


		/// <summary>
		/// Called from the onAction="" parameter in ribbon.xml. This is effectivley the onClick() function
		/// </summary>
		/// <param name="control">The control that was just clicked. control.Id will give you its ID</param>
		[STAThread]
		public void showHello(IRibbonControl control)
		{
			//string id = onApp.Windows.CurrentWindow.CurrentPageId;
			//MessageBox.Show("Current Page ID = " + id, "Hello World!");

            Debugg.showHello(control);
		}

		public void showBinary(IRibbonControl control)
		{
			//string id = onApp.Windows.CurrentWindow.CurrentPageId;
			//MessageBox.Show("Current Page ID = " + id, "Hello World!");

            Debugg.showBinary(control);
		}

        public void showLocation(IRibbonControl control)
        {
            Debugg.showLocation(control);
        }

		public void pullImageOCRText()
		{
            
		}

		/// <summary>
		/// Called from the loadImage="" parameter in ribbon.xml. Converts the images into IStreams
		/// </summary>
		/// <param name="imageName">The image="" parameter in ribbon.xml, i.e. the image name</param>
		public IStream GetImage(string imageName)
		{
			MemoryStream mem = new MemoryStream();
			Properties.Resources.HelloWorld.Save(mem, ImageFormat.Png);
			
			return new CCOMStreamWrapper(mem);


			/*
			 * If you have multiple images, something like (below) works:
			 * switch (imageName)
			 * {
			 *		case "image1.png":
			 *			Properties.Resources.image1.Save(mem, ImageFormat.Png);
			 *			break;
			 *			
			 *		case ...
			 *		
			 *		default: ...
			 * }
			*/
		}

		#endregion

		public void debug()
		{
			Window w = onApp.Windows[0];

			ulong handle = w.WindowHandle;
			long lhandle = (long)handle;
			String handleHex = handle.ToString("X");
			String lhandleHex = lhandle.ToString("X");
			String other = "hi";
		}

		public static Window getActiveWindow()
		{
			Window result = null;
			foreach (Window w in onApp.Windows)
			{
				if (w.Active)
				{
					result = w;
				}
			}
			return result;
		}
	}
}