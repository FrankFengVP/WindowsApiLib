using System;

namespace HookLib
{
    public class KeyPressedEvent : EventArgs
    {
        public bool Handled { get; set; }
        public int KeyCode { get; set; }
    }
}