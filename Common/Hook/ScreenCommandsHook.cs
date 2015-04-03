using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Hook
{
    public static class ScreenCommandsHook
    {
        public static int WM_SYSCOMMAND = 0x0112;
        public static int SC_MONITORPOWER = 0xF170;
        //Using the system pre-defined MSDN constants that can be used by the SendMessage() function .


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(int hWnd, int wMsg, int wParam, int lParam);

        public static void ShutdownScreen()
        {
            Form f = new Form();
            bool turnOff = true; //set true if you want to turn off, true if on
            SendMessage(f.Handle.ToInt32(), WM_SYSCOMMAND, SC_MONITORPOWER, turnOff ? 2 : -1);
        }

        public static void WakeScreen()
        {
            Form f = new Form();
            bool turnOff = false; //set true if you want to turn off, true if on
            SendMessage(f.Handle.ToInt32(), WM_SYSCOMMAND, SC_MONITORPOWER, turnOff ? 2 : -1);
        }
    }
}
