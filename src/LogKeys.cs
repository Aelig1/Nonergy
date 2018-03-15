using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace Nonergy
{
    class LogKeys
    {
        IKeyboardMouseEvents GlobalHook;

        public void Start()
        {
            GlobalHook = Hook.GlobalEvents();

            GlobalHook.KeyDown += KeyDown;
            GlobalHook.KeyUp += KeyUp;
            GlobalHook.MouseDown += MouseDown;
            GlobalHook.MouseUp += MouseUp;
            GlobalHook.MouseMove += MouseMove;
            GlobalHook.MouseWheel += MouseWheel;
        }

        public void Stop()
        {
            GlobalHook.KeyDown -= KeyDown;
            GlobalHook.KeyUp -= KeyUp;
            GlobalHook.MouseDown -= MouseDown;
            GlobalHook.MouseUp -= MouseUp;
            GlobalHook.MouseMove -= MouseMove;
            GlobalHook.MouseWheel -= MouseWheel;

            GlobalHook.Dispose();
        }

        void KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("KeyDown: {0}", e.KeyCode);
        }

        void KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine("KeyUp: {0}", e.KeyCode);
        }

        void MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseDown: {0}, {1}", e.Button, e.Clicks);
        }

        void MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseUp: {0}", e.Button);
        }

        void MouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseMove: ({0}, {1})", e.X, e.Y);
        }

        void MouseWheel(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseWheel: {0}", e.Delta);
        }
    }
}