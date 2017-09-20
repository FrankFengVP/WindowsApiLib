using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ApiWrapper
{
    public class Window
    {
        public static List<IntPtr> SearchHandle(string likeTitle)
        {
            IntPtr hWnd = IntPtr.Zero;
            var lst = new List<IntPtr>();
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle.Contains(likeTitle))
                {
                    hWnd = pList.MainWindowHandle;
                    lst.Add(hWnd);
                }
            }
            return lst;
        }

        public static void BringToFront(IntPtr win)
        {
            var result = Api.BringWindowToTop(win);
            if (!result)
            {
                int err = Marshal.GetLastWin32Error();
                throw new Exception("Fail to bring to front." + err);
            }
        }

        public static IntPtr FindControl(IntPtr parent, string cls, string title)
        {
            return Api.FindWindowEx(parent, IntPtr.Zero, cls, title);
        }

        public static void MoveWindow(IntPtr window, int x, int y)
        {
            const short SWP_NOSIZE = 0x0001;
            const short SWP_NOZORDER = 0x0004;
            const int SWP_SHOWWINDOW = 0x0040;
            if (window != IntPtr.Zero)
            {
                var res = Api.SetWindowPos(window, 0, x, y, 0, 0, SWP_NOSIZE | SWP_NOZORDER | SWP_SHOWWINDOW);
                if ((int)res == 0)
                {
                    int err = Marshal.GetLastWin32Error();
                    throw new Exception("Fail to set window pos of error code " + err);
                }

            }
        }
    }
}
