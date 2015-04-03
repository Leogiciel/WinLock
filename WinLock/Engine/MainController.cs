using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Hook;
using WinLock.Engine.Properties;

namespace WinLock.Engine
{
    public abstract class MainController
    {
        protected ILockStatable Interface;
        private bool Processing { get; set; }
        private bool Locking { get; set; }

        private bool _ctrlPressed;
        private bool _firstPressed;
        private bool _secondPressed;
        
        private bool _autoLock;
        private decimal _timer;
        
        private System.Threading.Timer _autoLockTimer;
        private System.Threading.Timer _waitOnActionTimer;
        private System.Threading.Timer _waitToggleTimer;
        private bool _waitingOnToggle;
        private bool _waitingOnAction;
        private Hook.Hook _mKeyHook;
        private Hook.Hook _mMouseHook;

        private static bool IsFirstKey(KeyEventArgs e)
        {
            return e.KeyCode.ToString().ToUpper() ==
                   (((Keys[])Enum.GetValues(typeof(Keys))).ToList().Where(k => k.ToString().Length == 1).ToArray())[
                       Settings.Default.FirstLetter].ToString().ToUpper();
        }

        private static bool IsSecondKey(KeyEventArgs e)
        {
            return e.KeyCode.ToString().ToUpper() ==
                   (((Keys[])Enum.GetValues(typeof(Keys))).ToList().Where(k => k.ToString().Length == 1).ToArray())[
                       Settings.Default.SecondLetter].ToString().ToUpper();
        }

        public void StartProcess()
        {
            Processing = true;
            _mKeyHook = new Hook.Hook(Hook.Hook.HookType.KeyBoard, Hook.Hook.HookVisibility.Global);
            _mKeyHook.OnKeyDown += OnKeydownAction;
            _mKeyHook.OnKeyUp += OnKeyupAction;
            _mKeyHook.Start();
            _mMouseHook = new Hook.Hook(Hook.Hook.HookType.Mouse, Hook.Hook.HookVisibility.Global);
            _mMouseHook.OnMouseClick += OnMouseAction;
            _mMouseHook.Start();
            _autoLock = Settings.Default.AutoLock;
            _timer = Settings.Default.Timer;
            if (!_autoLock) return;
            InitTimer();
        }

        private void InitTimer()
        {
            if (_autoLockTimer != null)
                _autoLockTimer.Dispose();
            _autoLockTimer = new System.Threading.Timer(LockFromTimer, null,Convert.ToInt32(_timer*1000),Timeout.Infinite );
        }

        private void LockFromTimer(object state)
        {
            Lock();
        }

        public void StopProcess()
        {
            Processing = false;
            if (_mKeyHook != null)
            {
                _mKeyHook.Stop();
            }
            if (_mMouseHook != null)
            {
                _mMouseHook.Stop();
            }
        }

        private void OnMouseAction(object sender, MouseEventArgs e)
        {
            if (Processing)
            {
                if (_autoLock)
                {
                    OnAction();
                }
            }
        }

        private void OnAction()
        {
            if (Locking)
            {
                if (Settings.Default.LockScreen)
                {
                    ScreenCommandsHook.ShutdownScreen();
                }
                Interface.NotifyLock();
            }
            else if (!_waitingOnAction && Processing && _autoLock && _autoLockTimer != null)
            {
                _waitingOnAction = true;
                _autoLockTimer.Change(Convert.ToInt32(_timer*1000),Timeout.Infinite);
                _waitOnActionTimer = new System.Threading.Timer(ReactivateOnActionEvents,null,1000,Timeout.Infinite);
            }
        }

        private void ReactivateOnActionEvents(object state)
        {
            _waitingOnAction = false;
        }

        private void OnKeyupAction(object sender, KeyEventArgs e)
        {
            if (!Processing) return;

            if (IsFirstKey(e))
            {
                _firstPressed = false;
            }

            else if (IsSecondKey(e))
            {
                _secondPressed = false;
            }

            else if (e.KeyValue == 162 || e.KeyValue == 163)
            {
                _ctrlPressed = false;
            }
        }

        private void OnKeydownAction(object sender, KeyEventArgs e)
        {
            if (!Processing) return;
            if (_autoLock)
            {
                OnAction();
            }
            if (e.KeyValue == 162 || e.KeyValue == 163)
            {
                _ctrlPressed = true;
            }
            if (_ctrlPressed)
            {
                if (IsFirstKey(e))
                {
                    _firstPressed = true;
                    e.Handled = true;
                }
                if (IsSecondKey(e))
                {
                    _secondPressed = true;
                }
                if (_ctrlPressed && _firstPressed && _secondPressed && !_waitingOnToggle)
                {
                    ToggleLock();
                    e.Handled = true;
                }
            }
            if (Processing && Locking && Settings.Default.LockKeyboard)
            {
                e.Handled = true;
            }
        }

        private void ToggleLock()
        {
            if (!_waitingOnToggle)
            {
                if (!Locking)
                {
                    Lock();
                }
                else
                {
                    Unlock();
                }
                _waitingOnToggle = true;
                _waitToggleTimer = new System.Threading.Timer(EnableToggle,null,600,Timeout.Infinite);
            }
        }

        private void EnableToggle(object state)
        {
            _waitingOnToggle = false;
        }

        private void Unlock()
        {
            _mMouseHook.StopMouseLocks();
            Interface.ListenState();
            if (Settings.Default.LockScreen)
            {
                ScreenCommandsHook.WakeScreen();
            }
            if (Settings.Default.AutoLock)
            {
                InitTimer();
            }
            Locking = false;
        }

        private void Lock()
        {
            if (Processing)
            {
                _mMouseHook.StartMouseLocks(Settings.Default.LockMouseClick, Settings.Default.LockMouseMove);
                Interface.LockState();
                if (Settings.Default.LockScreen)
                {
                    ScreenCommandsHook.ShutdownScreen();
                }
                if (Settings.Default.AutoLock)
                {
                    _autoLockTimer.Dispose();
                }
                Locking = true;
            }
        }

        public void SaveSettings(LockerSettings settings)
        {
            Settings.Default.FirstLetter = settings.FirstLetter;
            Settings.Default.SecondLetter = settings.SecondLetter;
            Settings.Default.LockKeyboard = settings.LockKeyboard;
            Settings.Default.LockMouseClick = settings.LockMouseClick;
            Settings.Default.LockMouseMove = settings.LockMouseMove; 
            Settings.Default.LockScreen = settings.LockScreen;
            Settings.Default.AutoLock = settings.AutoLock;
            Settings.Default.Timer = settings.LockTimer;
            Settings.Default.HideMoment = settings.HideMoment;

            Settings.Default.Save();
        }

        public LockerSettings LoadSettings()
        {
            Settings.Default.Reload();

            var settings = new LockerSettings();

            settings.FirstLetter = Settings.Default.FirstLetter;
            settings.SecondLetter = Settings.Default.SecondLetter;
            settings.LockKeyboard = Settings.Default.LockKeyboard;
            settings.LockMouseClick = Settings.Default.LockMouseClick;
            settings.LockMouseMove = Settings.Default.LockMouseMove;
            settings.LockScreen = Settings.Default.LockScreen;
            settings.AutoLock = Settings.Default.AutoLock;
            settings.LockTimer = (int)Settings.Default.Timer;
            settings.HideMoment = Settings.Default.HideMoment;

            return settings;
        }
    }
}