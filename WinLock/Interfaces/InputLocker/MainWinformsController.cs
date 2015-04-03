using System.Windows.Forms;
using WinLock.Engine;

namespace WinLock.Winforms
{
    public class MainWinformsController : MainController
    {
        //Constructeur
        public MainWinformsController()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Interface = new FrmMain(this);
            Application.Run((FrmMain) Interface);
            
        }
    }
}