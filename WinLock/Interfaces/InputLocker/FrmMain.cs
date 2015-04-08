using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinLock.Engine;
using WinLock.Engine.Properties;

namespace WinLock.Winforms
{
    public partial class FrmMain : Form, ILockStatable
    {
        //Champs privés
        private readonly MainWinformsController _parent;
        private LockerState _lockerState;
        private LockerSettings _settings;
        private List<Keys> _letters;
        private List<Keys> _letters2;

        //Constructeur
        public FrmMain(MainWinformsController parent)
        {
            InitializeComponent();
            FillComboboxes();
            _parent = parent;
            LoadSettings();
            _lockerState = LockerState.Stopped;
        }

        private void LoadSettings()
        {
            _settings = _parent.LoadSettings();

            cbxFirstLetter.SelectedItem = (Keys)_settings.FirstLetter;
            cbxSecondLetter.SelectedItem = (Keys)_settings.SecondLetter;
            
            chkKeyboard.Checked = _settings.LockKeyboard;
            chkMouseClicks.Checked = _settings.LockMouseClick;
            chkMouseMove.Checked = _settings.LockMouseMove;
            chkAutoLock.Checked = _settings.AutoLock;
            nudTimer.Value = _settings.LockTimer;
            chkShutScreen.Checked = _settings.LockScreen;
            switch (_settings.HideMoment)
            {
                case 0:
                    rbtNever.Checked = true;
                    break;
                case 1:
                    rbtOnLock.Checked = true;
                    break;
                case 2:
                    rbtOnStart.Checked = true;
                    break;

            }
        }

        #region ILockStatable Members

        public void ListenState()
        {
            _lockerState = LockerState.Listening;
            notifyIcon1.BalloonTipText = notifyIcon1.Text = notifyIcon1.BalloonTipText == Resources.Locking
                                                                ? Resources.LockStopped
                                                                : Resources.ServiceStarted;
            btnStart.Text = Resources.Stop;
            btnStart.Enabled = true;
            if (rbtOnLock.Checked)
            {
                ShowAgain();
            }
            else
            {
                notifyIcon1.ShowBalloonTip(1500);
            }
        }

        public void LockState()
        {
            Invoke(new DoInvoke(InnerLockState));
        }

        #endregion

        //Initialisation
        private void FillComboboxes()
        {
            _letters = new List<Keys>();
            foreach (Keys key in Enum.GetValues(typeof (Keys)))
            {
                if (key.ToString().Length == 1)
                {
                    _letters.Add(key);
                }
            }

            _letters2 = new List<Keys>();
            _letters2.AddRange(_letters);
            
            cbxFirstLetter.DataSource = _letters;
            cbxSecondLetter.DataSource = _letters2;
            
            cbxFirstLetter.SelectedItem = Keys.I;
            cbxSecondLetter.SelectedItem = Keys.L;
        }

        //Méthodes visuelles

        /// <summary>
        ///   Réduit la fenêtre dans la zone de notification et affiche le ballontip
        /// </summary>
        private void HideInNotif()
        {
            ShowInTaskbar = false;
            notifyIcon1.Visible = true;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            Visible = false;
            notifyIcon1.ShowBalloonTip(1500);
        }

        /// <summary>
        ///   Affiche la fenêtre et retire l'icône de la zone de notifications
        /// </summary>
        private void ShowAgain()
        {
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            Visible = true;
        }

        private void InnerLockState()
        {
            _lockerState = LockerState.Locking;
            notifyIcon1.BalloonTipText = notifyIcon1.Text = Resources.Locking;
            btnStart.Text = Resources.Locking;
            btnStart.Enabled = false;
            if (!rbtNever.Checked)
            {
                notifyIcon1.ShowBalloonTip(1500);
                HideInNotif();
            }
        }

        private void OnStartProcess()
        {
            _lockerState = LockerState.Listening;
            btnStart.Text = Resources.Stop;
            cbxFirstLetter.Enabled =
                gbxAutoLock.Enabled =
                gbxCombo.Enabled =
                gbxLockedEvents.Enabled =
                gbxHide.Enabled = false;

            //
            notifyIcon1.BalloonTipText = Resources.ServiceStarted;
            if (rbtOnStart.Checked)
            {
                HideInNotif();
            }
        }

        private void OnStopProcess()
        {
            _lockerState = LockerState.Stopped;
            btnStart.Text = Resources.Start;
            cbxFirstLetter.Enabled =
                gbxAutoLock.Enabled =
                gbxCombo.Enabled =
                gbxLockedEvents.Enabled =
                gbxHide.Enabled = true;
            //
            notifyIcon1.BalloonTipText = Resources.ServiceStopped;
            ShowAgain();
        }

        #region Evènements

        private void BtnStartClick(object sender, EventArgs e)
        {
            if (_lockerState == LockerState.Stopped)
            {
                UpdateSettings();
                //B/Appel au parent
                _parent.StartProcess(_settings);
                //B/Modifications IHM
                OnStartProcess();
            }
            else
            {
                //B/Appel au parent
                _parent.StopProcess();
                //B/Modifications IHM
                OnStopProcess();
            }
        }

        private void UpdateSettings()
        {
            _settings.FirstLetter = (int) cbxFirstLetter.SelectedItem;
            _settings.SecondLetter = (int) cbxSecondLetter.SelectedItem;
            _settings.LockKeyboard = chkKeyboard.Checked;
            _settings.LockMouseClick = chkMouseClicks.Checked;
            _settings.LockMouseMove = chkMouseMove.Checked;
            _settings.AutoLock = chkAutoLock.Checked;
            _settings.LockTimer = (int)nudTimer.Value;
            _settings.LockScreen = chkShutScreen.Checked;
            int moment;
            if (rbtNever.Checked)
            {
                moment = 0;
            }
            else if (rbtOnLock.Checked)
            {
                moment = 1;
            }
            else
            {
                moment = 2;
            }
            _settings.HideMoment = moment;
        }


        /// <summary>
        ///   nudTimer enabled si chkAutoLock checked
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"> </param>
        private void ChkAutoLockCheckedChanged(object sender, EventArgs e)
        {
            nudTimer.Enabled = chkAutoLock.Checked;
        }

        private void AfficherLaFenêtreToolStripMenuItemClick(object sender, EventArgs e)
        {
            ShowAgain();
        }

        #endregion

        #region Nested type: DoInvoke

        private delegate void DoInvoke();

        #endregion

        public void NotifyLock()
        {
            //TODO Show bubble;
        }
    }
}