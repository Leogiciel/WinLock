namespace WinLock.Engine
{
    public interface ILockStatable
    {
        void LockState();
        void ListenState();
        void NotifyLock();
    }
}