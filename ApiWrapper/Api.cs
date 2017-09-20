using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ApiWrapper
{
    class Api
    {
        [DllImport("user32.dll")]
        public static extern void SendClickMessage(IntPtr m, int x, int y);
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        public static extern void mouse_event(MouseClickType flags, int dx, int dy, uint data, UIntPtr extraInfo);

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(int bVk, byte bScan, KeyEventFlag dwFlags, int dwExtraInfo);
        public enum KeyEventFlag : int
        {
            KEYEVENTF_KEYUP = 0x2,
            KEYEVENTF_KEYDOWN = 0x00
        }

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern int GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport("gdi32.dll")]
        public static extern int SetPixel(IntPtr hdc, int nXPos, int nYPos, int color);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int GetLastError();


        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern int ResetDC(IntPtr hWnd, ref int hDC);

        [DllImportAttribute("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, //目的上下文设备的句柄
                    int nXDest, //目的图形的左上角的x坐标
                    int nYDest, //目的图形的左上角的y坐标
                    int nWidth, //目的图形的矩形宽度   
                    int nHeight, //目的图形的矩形高度
                    IntPtr hdcSrc, //源上下文设备的句柄
                    int nXSrc, //源图形的左上角的x坐标
                    int nYSrc, //源图形的左上角的x坐标 
                    System.Int32 dwRop //光栅操作代码
                    );
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos", SetLastError = true)]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        public static extern IntPtr MoveWindow(IntPtr win, int x, int y, int width, int height, bool repaint);


        [DllImport("user32.dll", EntryPoint = "BringWindowToTop", SetLastError = true)]
        public static extern bool BringWindowToTop(IntPtr m_hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr parentHWnd, IntPtr childAfterHWnd, string className, string windowTitle);
    }
}
