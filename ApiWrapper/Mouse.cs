using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApiWrapper
{
    public class Mouse
    {

        public static void ClickLeft()
        {
            ClickMouse(ClickType.Left);
        }

        public static void ClickRight()
        {
            ClickMouse(ClickType.Right);
        }


        public static void Hilight(int x, int y, int endx, int endy)
        {
            MoveTo(x, y);
            Press(ClickType.Left);
            Thread.Sleep(500);
            MoveTo(endx, endy);
            Thread.Sleep(500);
            Release(ClickType.Left);
        }

        private static void Release(ClickType clickType)
        {
            switch (clickType)
            {
                case ClickType.None:
                    break;
                case ClickType.Left:
                    Api.mouse_event(MouseClickType.LeftUp, 259, 334, 0, UIntPtr.Zero);
                    break;
                case ClickType.Right:
                    Api.mouse_event(MouseClickType.RightUp, 259, 334, 0, UIntPtr.Zero);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        public static void Press(ClickType type)
        {
            switch (type)
            {
                case ClickType.None:
                    break;
                case ClickType.Left:
                    Api.mouse_event(MouseClickType.LeftDown, 259, 334, 0, UIntPtr.Zero);
                    break;
                case ClickType.Right:
                    Api.mouse_event(MouseClickType.RightDown, 259, 334, 0, UIntPtr.Zero);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        public static void ClickLeft(int x, int y)
        {
            MoveTo(x, y);
            Thread.Sleep(600);
            ClickMouse(ClickType.Left);
        }

        public static void ClickRight(int x, int y)
        {
            MoveTo(x, y);
            Thread.Sleep(600);
            ClickMouse(ClickType.Right);
        }

        public static void MoveTo(int x, int y)
        {
            Api.SetCursorPos(x, y);
        }

        public static void ClickMouse(ClickType type)
        {

            switch (type)
            {
                case ClickType.Left:
                    Api.mouse_event(MouseClickType.LeftDown, 259, 334, 0, UIntPtr.Zero);
                    Api.mouse_event(MouseClickType.LeftUp, 259, 334, 0, UIntPtr.Zero);
                    break;
                case ClickType.Right:
                    Api.mouse_event(MouseClickType.RightDown, 259, 334, 0, UIntPtr.Zero);
                    Api.mouse_event(MouseClickType.RightUp, 259, 334, 0, UIntPtr.Zero);
                    break;
            }
        }
    }
}
