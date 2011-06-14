using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.OneNote;

namespace NicksPowerTool
{
    class ONAppTools
    {
        private static ApplicationClass _App;
        public static ApplicationClass App
        {
            get
            {
                return _App;
            }

            set
            {
                if (_App == null)
                {
                    _App = value;
                }
            }
        }

        public static string GetCurrentPageId()
        {
            return App.Windows.CurrentWindow.CurrentPageId;
        }

        public static string GetCurrentSectionId()
        {
            return App.Windows.CurrentWindow.CurrentSectionId;
        }

        public static string GetCurrentSectionGroupId()
        {
            return App.Windows.CurrentWindow.CurrentSectionGroupId;
        }

        public static string GetCurrentNotebookId()
        {
            return App.Windows.CurrentWindow.CurrentNotebookId;
        }

        public static void ShowPageSelectionWindow()
        {
            IQuickFilingDialog dialog = App.QuickFiling();
            dialog.Title = "Please select a page: ";
            dialog.TreeDepth = HierarchyElement.hePages;
        }

        public static DateTime ConvertStringToDateTime(string s)
        {
            //"2011-05-18T12:33:50.000Z"
        }

        public string ConvertDateTimeToString(DateTime dt)
        {

        }
    }
}
