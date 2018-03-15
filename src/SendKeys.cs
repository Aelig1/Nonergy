using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Nonergy
{
    class SendKeys
    {
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        const uint KEYEVENTF_KEYUP = 0x0002;
        
        const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_XDOWN = 0x0080;
        const uint MOUSEEVENTF_XUP = 0x0100;
        const uint MOUSEEVENTF_WHEEL = 0x0800;
        const uint MOUSEEVENTF_HWHEEL = 0x01000;

        Dictionary<MouseButtons, uint> MouseDownButton = new Dictionary<MouseButtons, uint>
        {
            { MouseButtons.Left, MOUSEEVENTF_LEFTDOWN },
            { MouseButtons.Middle, MOUSEEVENTF_MIDDLEDOWN },
            { MouseButtons.Right, MOUSEEVENTF_RIGHTDOWN },
            { MouseButtons.XButton1, MOUSEEVENTF_XDOWN }
        };
        Dictionary<MouseButtons, uint> MouseUpButton = new Dictionary<MouseButtons, uint>
        {
            { MouseButtons.Left, MOUSEEVENTF_LEFTUP },
            { MouseButtons.Middle, MOUSEEVENTF_MIDDLEUP },
            { MouseButtons.Right, MOUSEEVENTF_RIGHTUP },
            { MouseButtons.XButton1, MOUSEEVENTF_XUP }
        };

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);
        [DllImport("user32.dll")]
        static extern int SetCursorPos(int x, int y);

        public void KeyDown(KeyEventArgs e)
        {
            keybd_event((byte)e.KeyCode, 0, KEYEVENTF_EXTENDEDKEY, 0);
        }

        public void KeyUp(KeyEventArgs e)
        {
            keybd_event((byte)e.KeyCode, 0, KEYEVENTF_KEYUP, 0);
        }

        public void MouseDown(MouseEventArgs e)
        {
            MouseDownButton.TryGetValue(e.Button, out uint downButton);
            MouseUpButton.TryGetValue(e.Button, out uint upButton);

            mouse_event(downButton, 0, 0, 0, 0);

            for (int i = 1; i < e.Clicks; i++)
            {
                mouse_event(upButton, 0, 0, 0, 0);
                mouse_event(downButton, 0, 0, 0, 0);
            }
        }

        public void MouseUp(MouseEventArgs e)
        {
            MouseUpButton.TryGetValue(e.Button, out uint upButton);
            mouse_event(upButton, 0, 0, 0, 0);
        }

        public void MouseMove(MouseEventArgs e)
        {
            SetCursorPos(e.X, e.Y);
        }

        public void MouseWheel(MouseEventArgs e)
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (uint)e.Delta, 0);
        }
    }
}