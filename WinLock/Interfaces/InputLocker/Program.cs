using System;

namespace WinLock.Winforms
{
    internal static class Program
    {
        public static MainWinformsController Controller { get; set; }

        /// <summary>
        ///   Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Controller = new MainWinformsController();
        }
    }
}