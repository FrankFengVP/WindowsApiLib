using System.Runtime.InteropServices;

namespace HookLib
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Kbdllhookstruct
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public int dwExtraInfo;
    }
}