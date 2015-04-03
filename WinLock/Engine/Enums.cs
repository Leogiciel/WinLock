namespace WinLock.Engine
{
    public enum HideMoment
    {
        Never,
        OnStart,
        OnLock
    }

    public enum LockerState
    {
        Stopped,
        Listening,
        Locking
    }
}