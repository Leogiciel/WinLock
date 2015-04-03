using WinLock.Engine;

namespace WinLock.WPF
{
    public class MainWPFController:MainController
    {
        public MainWPFController(ILockStatable lockStatableInterface)
        {
            Interface = lockStatableInterface;
        }
    }
}
