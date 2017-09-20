using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Collections;

namespace HookLib
{
    public class HookLib
    {
        public delegate void EnableHookHandler();

        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public event EventHandler<KeyPressedEvent> KeyPressed;

        public event EnableHookHandler OnKeyTriggered;

        private const int KeyBoardHookEventId = 13;
        const int TriggerKey = 145;
        private GCHandle _hookProcHandle;
        private IntPtr _hookHandle = IntPtr.Zero;
        private Kbdllhookstruct kbdllhs;

        public bool Enabled
        {
            get;
            set;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(int hookid, HookProc pfnhook, IntPtr hinst, int threadid);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int CallNextHookEx(IntPtr hhook, int code, IntPtr wparam, IntPtr lparam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        public static extern void CopyMemory(ref Kbdllhookstruct Source, IntPtr Destination, int Length);

        public void DisableKeyBoardHook()
        {
            try
            {
                if (_hookHandle != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(_hookHandle);
                }
                _hookProcHandle.Free();
                _hookHandle = IntPtr.Zero;
            }
            catch
            {
                return;
            }
        }

        public void EnableKeyBoardHook()
        {
            _hookProcHandle = GCHandle.Alloc((HookProc)KBDDelegate);
            _hookHandle = SetWindowsHookEx(KeyBoardHookEventId, KBDDelegate, GetModuleHandle("HookLib.dll"), 0);

            if (_hookHandle == IntPtr.Zero)
            {
                throw new System.Exception("Fail to set hook.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public int KBDDelegate(int iCode, IntPtr wParam, IntPtr lParam)
        {
            // wParam = 256:keydown; wParam = 257:keyup
            int iHookCode = GetKeyCode(lParam);
            
            if (iHookCode == TriggerKey && (int)wParam != 257)
            {
                if (OnKeyTriggered != null)
                    OnKeyTriggered();
                return CallNextHookEx(_hookHandle, iCode, wParam, lParam);
            }

            Debug.WriteLine(iHookCode + "," + wParam);

            if ((int)wParam == 257 || !Enabled)
            {
                return CallNextHookEx(_hookHandle, iCode, wParam, lParam);
            }

            if (OnOnKeyPressed(iHookCode))
            {
                return 3;
            }

            return CallNextHookEx(_hookHandle, iCode, wParam, lParam);
        }

        protected virtual bool OnOnKeyPressed(int keyCode)
        {
            if (KeyPressed != null)
            {
                this.Enabled = false;
                var keyPressedEvent = new KeyPressedEvent { KeyCode = keyCode };
                KeyPressed(this, keyPressedEvent);
                this.Enabled = true;
                return keyPressedEvent.Handled;
            }
            return false;
        }

        private int GetKeyCode(IntPtr oPare)
        {
            kbdllhs = new Kbdllhookstruct();
            CopyMemory(ref kbdllhs, oPare, 20);
            return kbdllhs.vkCode;
        }
    }
}