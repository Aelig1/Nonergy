using System;
using System.Windows.Forms;

namespace Nonergy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Nonergy!");

            LogKeys keyListener = new LogKeys();
            keyListener.Start();

            Console.WriteLine("Listening...");

            SendKeys keySender = new SendKeys();

            Application.Run(new ApplicationContext());

            keyListener.Stop();
        }
    }
}
