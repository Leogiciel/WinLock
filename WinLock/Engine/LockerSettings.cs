using System;
using System.Collections;
using System.Configuration;

namespace WinLock.Engine
{
    public class LockerSettings
    {
        public int FirstLetter { get; set; }
        public int SecondLetter { get; set; }
        public bool LockKeyboard { get; set; }
        public bool LockMouseClick { get; set; }
        public bool LockMouseMove { get; set; }
        public bool LockScreen { get; set; }
        public bool AutoLock { get; set; }
        public int LockTimer { get; set; }
        public int HideMoment { get; set; }
    }
}