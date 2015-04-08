using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using WinLock.Engine;
using Application = System.Windows.Application;

namespace WinLock.WPF
{
    /// <summary>
    ///   Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ILockStatable
    {
        #region Fields

        private readonly ToolStripMenuItem _showWindowToolStripMenuItem;
        private readonly ContextMenuStrip _contextMenuStrip1;
        private readonly MainController _controller;
        private readonly NotifyIcon _notifyIcon1;
        private LockerSettings _settings;
        internal LockerState LockerState;
        public List<Key> Letters { get; set; }

        #endregion

        #region Delegate

        private delegate void DoInvoke();

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            FillComboboxes();

            _controller = new MainWPFController(this);

            _settings = _controller.LoadSettings();

            MainGrid.DataContext = _settings;
            
            //init a context menu
            _contextMenuStrip1 = new ContextMenuStrip();
            //init a menuitem
            _showWindowToolStripMenuItem = new ToolStripMenuItem { Text = Engine.Resources.ShowWindow };
            _showWindowToolStripMenuItem.Click += ShowWindowToolStripMenuItemClick;
            //add item to menu
            _contextMenuStrip1.Items.AddRange(new ToolStripItem[]{_showWindowToolStripMenuItem});
            //init a notifyIcon
            _notifyIcon1 = new NotifyIcon
                               {
                                   Icon = Engine.Resources._1368108324_Logout,
                                   BalloonTipTitle = Application.ResourceAssembly.GetName().ToString().Split(',')[0],
                                   ContextMenuStrip = _contextMenuStrip1
                               };
            //add menu to icon
            LockerState = LockerState.Stopped;
        }

        #endregion

        #region Methods

            #region Initialize
        
            private void FillComboboxes()
            {
                CbxFirstLetter.ItemsSource =
                    CbxSecondLetter.ItemsSource = ((Key[])Enum.GetValues(typeof(Key))).Where(k => k.ToString().Length == 1);
            }

            #endregion

            #region ILockStatable Members

            public void ListenState()
            {
                LockerState = LockerState.Listening;
                _notifyIcon1.BalloonTipText =
                    _notifyIcon1.Text = _notifyIcon1.BalloonTipText == Engine.Resources.Locking
                                            ? Engine.Resources.LockStopped
                                            : Engine.Resources.ServiceStarted;
                BtnStart.Content = Engine.Resources.Stop;
                BtnStart.IsEnabled = true;
                if (RbtOnLock.IsChecked == true)
                {
                    ShowAgain();
                }
                else
                {
                    _notifyIcon1.ShowBalloonTip(1500);
                }
            }

        public void NotifyLock()
        {
            _notifyIcon1.ShowBalloonTip(1500);
        }

        public void LockState()
            {
                if (Dispatcher.CheckAccess())
                {
                    InnerLockState();
                }
                else
                {
                    Dispatcher.Invoke(DispatcherPriority.Send, new DoInvoke(InnerLockState));
                }
            }

            #endregion

            #region Visual

            private void InnerLockState()
            {
                LockerState = LockerState.Locking;
                _notifyIcon1.BalloonTipText = _notifyIcon1.Text = Engine.Resources.Locking;
                BtnStart.Content = Engine.Resources.Locking;
                BtnStart.IsEnabled = false;
                if (RbtNever.IsChecked != false) return;
                _notifyIcon1.ShowBalloonTip(1500);
                HideInNotif();
            }
        
            /// <summary>
            ///   Réduit la fenêtre dans la zone de notification et affiche le ballontip
            /// </summary>
            private void HideInNotif()
            {
                ShowInTaskbar = false;
                Visibility = Visibility.Hidden;
                _notifyIcon1.Visible = true;
                //notifyIcon1.ContextMenuStrip = contextMenuStrip1;
                _notifyIcon1.ShowBalloonTip(1500);
            }

            /// <summary>
            ///   Affiche la fenêtre et retire l'icône de la zone de notifications
            /// </summary>
            private void ShowAgain()
            {
                ShowInTaskbar = true;
                Visibility = Visibility.Visible;
                _notifyIcon1.Visible = false;
            }

            private void OnStartProcess()
            {
                LockerState = LockerState.Listening;
                BtnStart.Content = Engine.Resources.Stop;
                CbxFirstLetter.IsEnabled =
                    GbxAutoLock.IsEnabled =
                    GbxCombo.IsEnabled =
                    GbxLockedEvents.IsEnabled =
                    GbxHide.IsEnabled = false;
                _notifyIcon1.BalloonTipText = Engine.Resources.ServiceStarted;
                if (RbtOnStart.IsChecked == true)
                {
                    HideInNotif();
                }
            }

            private void OnStopProcess()
            {
                LockerState = LockerState.Stopped;
                BtnStart.Content = Engine.Resources.Start;
                CbxFirstLetter.IsEnabled =
                    GbxAutoLock.IsEnabled =
                    GbxCombo.IsEnabled =
                    GbxLockedEvents.IsEnabled =
                    GbxHide.IsEnabled = true;
                _notifyIcon1.BalloonTipText = Engine.Resources.ServiceStopped;
                ShowAgain();
            }
            #endregion

            #region Events

            private void BtnStartClick(object sender, RoutedEventArgs e)
            {
                switch (LockerState)
                {
                    case LockerState.Stopped:
                        {
                            _controller.StartProcess(_settings);
                            OnStartProcess();
                        }
                        break;
                    default:
                        _controller.StopProcess();
                        OnStopProcess();
                        break;
                }
            }

            /// <summary>
            /// nudTimer enabled if chkAutoLock checked
            /// </summary>
            /// <param name="sender"> </param>
            /// <param name="e"> </param>
            private void ChkAutoLockChecked(object sender, EventArgs e)
            {
                if (TimerSlider != null)
                    TimerSlider.IsEnabled = ChkAutoLock.IsChecked == true;
            }

            private void ShowWindowToolStripMenuItemClick(object sender, EventArgs e)
            {
                ShowAgain();
            }

            #endregion

        #endregion
    }
}