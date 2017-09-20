using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApiWrapper
{
    public static class Keyboard
    {

        public static void TypeMe(this string message)
        {
            TypeThisString(message);
        }

        public static void TypeThisString(string strMessage)
        {
            char[] cs = strMessage.ToCharArray();
            foreach (char c in cs)
            {
                bool up = false;
                char t = MappingKeyCode(c, ref up);
                if (up)
                    Api.keybd_event(16, 0, Api.KeyEventFlag.KEYEVENTF_KEYDOWN, 0);
                Api.keybd_event(t, 0, Api.KeyEventFlag.KEYEVENTF_KEYDOWN, 0);
                Api.keybd_event(t, 0, Api.KeyEventFlag.KEYEVENTF_KEYUP, 0);
                if (up)
                    Api.keybd_event(16, 0, Api.KeyEventFlag.KEYEVENTF_KEYUP, 0);
            }
        }
        private static char MappingKeyCode(char o, ref bool isUpper)
        {
            //Capital letters
            if (o <= 90 && o >= 65)
            {
                isUpper = true;
                return o;
            }

            if (o >= 48 && o <= 57)
                return (char)(o + 48);
            if (o >= 97 && o <= 122)
                return (char)(o - 32);

            if (o == '-')
            {
                return (char)109;
            }

            if (o == '@')
            {
                isUpper = true;
                return (char)50;
            }
            if (o == '!')
            {
                isUpper = true;
                return (char)49;
            }
            if (o == '#')
            {
                isUpper = true;
                return (char)51;
            }

            if (o == '$')
            {
                isUpper = true;
                return (char)52;
            }

            if (o == '%')
            {
                isUpper = true;
                return (char)53;
            }

            if (o == '^')
            {
                isUpper = true;
                return (char)54;
            }

            if (o == '_')
            {
                isUpper = true;
                return (char) 189;
            }

            if (o == '"')
            {
                isUpper = true;
                return (char) 222;
            }

            if (o == ':')
            {
                isUpper = true;
                return (char)186;
            }

            if (o == '.')
            {
                return (char) 190;
            }

            if (o == ' ')
            {
                return (char) 32;
            }

            return (char)0;
        }

        public static void Tap(Keys key, Keys assicts = Keys.None)
        {
            if (assicts.HasFlag(Keys.Alt))
            {
                Api.keybd_event(18, 0, Api.KeyEventFlag.KEYEVENTF_KEYDOWN, 0);
            }

            if (assicts.HasFlag(Keys.Control))
            {
                Api.keybd_event(17, 0, Api.KeyEventFlag.KEYEVENTF_KEYDOWN, 0);
            }

            Api.keybd_event((int)key, 0, Api.KeyEventFlag.KEYEVENTF_KEYDOWN, 0);
            Thread.Sleep(20);
            Api.keybd_event((int)key, 0, Api.KeyEventFlag.KEYEVENTF_KEYUP, 0);
            if (assicts.HasFlag(Keys.Alt))
            {
                Api.keybd_event(18, 0, Api.KeyEventFlag.KEYEVENTF_KEYUP, 0);
            }

            if (assicts.HasFlag(Keys.Control))
            {
                Api.keybd_event(17, 0, Api.KeyEventFlag.KEYEVENTF_KEYUP, 0);
            }
        }
    }
}
