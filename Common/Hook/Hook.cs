using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Hook
{
    public class Hook
    {
        #region Attributs

        private readonly HookType _mHookType;
        private readonly HookVisibility _mVisibility;
        private bool _lockClick;
        private bool _lockMove;
        private IntPtr _mHook = (IntPtr) 0;
        private KeyEventHandler _mOnKeyDown;
        private KeyPressEventHandler _mOnKeyPress;
        private KeyEventHandler _mOnKeyUp;
        private MouseEventHandler _mOnMouseClick;
        private HookProc _mProc;

        #region Déléguées

        private delegate IntPtr HookProc(int nCode, int wParam, IntPtr lParam);

        # endregion

        #endregion

        #region Constructeurs

        public Hook(HookType hookType, HookVisibility hookVisibility)
        {
            _mHookType = hookType;
            _mVisibility = hookVisibility;
            _lockMove = _lockClick = false;
        }

        public bool LockMove
        {
            get { return _lockMove; }
            set { _lockMove = value; }
        }

        public bool LockClick
        {
            get { return _lockClick; }
            set { _lockClick = value; }
        }

        #endregion

        #region Méthodes

        public bool Start()
        {
            switch (_mHookType)
            {
                case HookType.KeyBoard:
                    _mProc = KeyProc;
                    break;
                case HookType.Mouse:
                    _mProc = MouseProc;
                    break;
            }

            switch (_mVisibility)
            {
                case HookVisibility.Global:
                    _mHook = SetWindowsHookEx(getHookType(_mHookType, _mVisibility), _mProc,
                                              GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
                    break;
                case HookVisibility.Local:
                    _mHook = SetWindowsHookEx(getHookType(_mHookType, _mVisibility), _mProc, GetModuleHandle((IntPtr) 0),
                                              (int) GetCurrentThreadId());
                    break;
            }

            return _mHook != (IntPtr) 0;
        }

        public bool Stop()
        {
            return UnhookWindowsHookEx(_mHook);
        }


        public void StopMouseLocks()
        {
            _lockClick = _lockMove = false;
        }

        public void StartMouseLocks(bool lockClick, bool lockMouse)
        {
            _lockClick = lockClick;
            _lockMove = lockMouse;
        }

        #endregion

        # region Accesseurs

        public event MouseEventHandler OnMouseClick
        {
            add { _mOnMouseClick += value; }
            remove { if (_mOnMouseClick != null) _mOnMouseClick -= value; }
        }

        public event KeyPressEventHandler OnKeyPress
        {
            add { _mOnKeyPress += value; }
            remove { if (_mOnKeyPress != null) _mOnKeyPress -= value; }
        }

        public event KeyEventHandler OnKeyUp
        {
            add { _mOnKeyUp += value; }
            remove { if (_mOnKeyUp != null) _mOnKeyUp -= value; }
        }

        public event KeyEventHandler OnKeyDown
        {
            add { _mOnKeyDown += value; }
            remove { if (_mOnKeyDown != null) _mOnKeyDown -= value; }
        }

        #endregion

        #region Méthodes privées

        private int getHookType(HookType H, HookVisibility V)
        {
            if (H == HookType.Mouse && V == HookVisibility.Local)
                return 7;

            if (H == HookType.Mouse && V == HookVisibility.Global)
                return 14;

            if (H == HookType.KeyBoard && V == HookVisibility.Local)
                return 2;

            if (H == HookType.KeyBoard && V == HookVisibility.Global)
                return 13;

            else return -1;
        }

        private int getMouseEventType(MouseEventType M)
        {
            if (M == MouseEventType.MouseMoved)
                return 0x200;

            if (M == MouseEventType.LeftButtonDown)
                return 0x201;

            if (M == MouseEventType.LeftButtonUp)
                return 0x202;

            if (M == MouseEventType.LeftButtonClick)
                return 0x203;

            if (M == MouseEventType.RightButtonDown)
                return 0x204;

            if (M == MouseEventType.RightButtonUp)
                return 0x205;

            if (M == MouseEventType.RightButtonClick)
                return 0x206;

            if (M == MouseEventType.MiddleButtonDown)
                return 0x207;

            if (M == MouseEventType.MiddleButtonUp)
                return 0x208;

            if (M == MouseEventType.MiddleButtonClick)
                return 0x209;

            if (M == MouseEventType.Wheel)
                return 0x020A;

            else return -1;
        }

        private int getKeyBoardEventType(KeyBoardEventType K)
        {
            if (K == KeyBoardEventType.KeyDown)
                return 0x100;

            if (K == KeyBoardEventType.KeyUp)
                return 0x101;

            if (K == KeyBoardEventType.SysKeyDown)
                return 0x104;

            if (K == KeyBoardEventType.SysKeyUp)
                return 0x105;

            if (K == KeyBoardEventType.KeyShift)
                return 0x10;

            if (K == KeyBoardEventType.KeyCapital)
                return 0x14;

            if (K == KeyBoardEventType.NumLock)
                return 0x90;

            else return -1;
        }

        private IntPtr KeyProc(int nCode, int wParam, IntPtr lParam)
        {
            bool handled = false;
            KeyEventArgs e;

            //On verifie si tous est ok
            if ((nCode >= 0) && (_mOnKeyDown != null || _mOnKeyUp != null || _mOnKeyPress != null))
            {
                //Remplissage de la structure KeyboardHookStruct a partir d'un pointeur
                KeyboardHookStruct MyKeyboardHookStruct =
                    (KeyboardHookStruct) Marshal.PtrToStructure(lParam, typeof (KeyboardHookStruct));

                Keys keyData = (Keys) MyKeyboardHookStruct.vkCode;
                e = new KeyEventArgs(keyData);

                //KeyDown
                if (_mOnKeyDown != null && (wParam == 0x100 || wParam == 0x104))
                {
                    _mOnKeyDown(this, e);
                    handled = handled || e.Handled;
                }

                // KeyPress
                if (_mOnKeyPress != null && wParam == 0x100)
                {
                    // Si la touche Shift est appuyée
                    bool isShift = ((GetKeyState(0x10) & 0x80) == 0x80 ? true : false);
                    // Si la touche CapsLock est appuyée
                    bool isCapslock = (GetKeyState(0x14) != 0 ? true : false);

                    byte[] keyState = new byte[256];
                    GetKeyboardState(keyState);
                    byte[] inBuffer = new byte[2];
                    if (ToAscii(MyKeyboardHookStruct.vkCode,
                                MyKeyboardHookStruct.scanCode,
                                keyState,
                                inBuffer,
                                MyKeyboardHookStruct.flags) == 1)
                    {
                        char key = (char) inBuffer[0];
                        if ((isCapslock ^ isShift) && Char.IsLetter(key))
                            key = Char.ToUpper(key);
                        KeyPressEventArgs f = new KeyPressEventArgs(key);
                        _mOnKeyPress(this, f);
                        handled = handled || e.Handled;
                    }
                }

                // KeyUp
                if (_mOnKeyUp != null && (wParam == 0x101 || wParam == 0x105))
                {
                    _mOnKeyUp(this, e);
                    handled = handled || e.Handled;
                }
            }


            // si handled est a true, on ne transmet pas le message au destinataire
            if (handled)
            {
                return (IntPtr) 1;
            }
            return CallNextHookEx(_mHook, nCode, (IntPtr) wParam, lParam);
        }

        private IntPtr MouseProc(int nCode, int wParam, IntPtr lParam)
        {
            // Verifions si nCode est different de 0 et que nos evenements sont bien attachés
            if ((nCode >= 0) && (_mOnMouseClick != null))
            {
                //Remplissage de la structure MouseLLHookStruct a partir d'un pointeur
                MouseLLHookStruct mouseHookStruct =
                    (MouseLLHookStruct) Marshal.PtrToStructure(lParam, typeof (MouseLLHookStruct));

                //Detection du bouton clicker
                MouseButtons button = MouseButtons.None;

                switch (wParam)
                {
                    case 0x201:
                        button = MouseButtons.Left;
                        break;

                    case 0x204:
                        button = MouseButtons.Right;
                        break;
                }

                //parametre de notre event
                MouseEventArgs e = new MouseEventArgs(button, 1, mouseHookStruct.pt.x, mouseHookStruct.pt.y, 0);

                //On appelle notre event
                _mOnMouseClick(this, e);
            }


            //Si le lock est actif pour l'évènement, on le bloque
            if ((LockMove && wParam == 512) || (LockClick && wParam != 512))
            {
                return (IntPtr) 1;
            }
            else
                return CallNextHookEx(_mHook, nCode, (IntPtr) wParam, lParam);
        }

        #endregion

        #region Enums

        #region HookType enum

        public enum HookType
        {
            Mouse,
            KeyBoard
        };

        #endregion

        #region HookVisibility enum

        public enum HookVisibility
        {
            Global,
            Local
        };

        #endregion

        #region KeyBoardEventType enum

        public enum KeyBoardEventType
        {
            KeyDown,
            KeyUp,
            SysKeyDown,
            SysKeyUp,
            KeyShift,
            KeyCapital,
            NumLock
        };

        #endregion

        #region MouseEventType enum

        public enum MouseEventType
        {
            MouseMoved,
            LeftButtonDown,
            RightButtonDown,
            MiddleButtonDown,
            LeftButtonUp,
            RightButtonUp,
            MiddleButtonUp,
            LeftButtonClick,
            RightButtonClick,
            MiddleButtonClick,
            Wheel
        };

        #endregion

        #endregion

        # region Imports

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
            SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hmod, int dwThreadId);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
            SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hHook);

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(IntPtr lpModuleName);

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(String lpModuleName);

        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetCurrentThreadId();

        [DllImport("user32")]
        private static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey,
                                          int fuState);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(int vKey);

        [DllImport("user32")]
        private static extern int GetKeyboardState(byte[] pbKeyState);

        # endregion

        # region Structs

        #region Nested type: KeyboardHookStruct

        [StructLayout(LayoutKind.Sequential)]
        private class KeyboardHookStruct
        {
            /// <summary>
            ///   Key code virtuel, la valeur doit etre entre 1 et 254.
            /// </summary>
            public int vkCode;

            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        #endregion

        #region Nested type: MouseHookStruct

        [StructLayout(LayoutKind.Sequential)]
        private class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        #endregion

        #region Nested type: MouseLLHookStruct

        [StructLayout(LayoutKind.Sequential)]
        private class MouseLLHookStruct
        {
            /// <summary>
            ///   Structure POINT.
            /// </summary>
            public POINT pt;

            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        #endregion

        #region Nested type: POINT

        private struct POINT
        {
            public int x;
            public int y;
        }

        #endregion

        #endregion
    }
}