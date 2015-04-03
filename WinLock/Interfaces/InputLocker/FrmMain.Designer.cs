using System.ComponentModel;
using System.Windows.Forms;

namespace WinLock.Winforms
{
    partial class FrmMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected new void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnStart = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.afficherLaFenêtreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblControl = new System.Windows.Forms.Label();
            this.cbxFirstLetter = new System.Windows.Forms.ComboBox();
            this.cbxSecondLetter = new System.Windows.Forms.ComboBox();
            this.lblPlus = new System.Windows.Forms.Label();
            this.chkAutoLock = new System.Windows.Forms.CheckBox();
            this.gbxCombo = new System.Windows.Forms.GroupBox();
            this.gbxAutoLock = new System.Windows.Forms.GroupBox();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.nudTimer = new System.Windows.Forms.NumericUpDown();
            this.gbxLockedEvents = new System.Windows.Forms.GroupBox();
            this.chkMouseMove = new System.Windows.Forms.CheckBox();
            this.chkMouseClicks = new System.Windows.Forms.CheckBox();
            this.chkKeyboard = new System.Windows.Forms.CheckBox();
            this.gbxHide = new System.Windows.Forms.GroupBox();
            this.rbtOnLock = new System.Windows.Forms.RadioButton();
            this.rbtOnStart = new System.Windows.Forms.RadioButton();
            this.rbtNever = new System.Windows.Forms.RadioButton();
            this.chkShutScreen = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1.SuspendLayout();
            this.gbxCombo.SuspendLayout();
            this.gbxAutoLock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimer)).BeginInit();
            this.gbxLockedEvents.SuspendLayout();
            this.gbxHide.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(12, 378);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(295, 55);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Démarrer";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStartClick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "Le verrouillage est activé.";
            this.notifyIcon1.BalloonTipTitle = "InputLocker";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "InputLocker - Service démarré";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.afficherLaFenêtreToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 26);
            // 
            // afficherLaFenêtreToolStripMenuItem
            // 
            this.afficherLaFenêtreToolStripMenuItem.Name = "afficherLaFenêtreToolStripMenuItem";
            this.afficherLaFenêtreToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.afficherLaFenêtreToolStripMenuItem.Text = "Afficher la fenêtre";
            this.afficherLaFenêtreToolStripMenuItem.Click += new System.EventHandler(this.AfficherLaFenêtreToolStripMenuItemClick);
            // 
            // lblControl
            // 
            this.lblControl.AutoSize = true;
            this.lblControl.Location = new System.Drawing.Point(6, 25);
            this.lblControl.Name = "lblControl";
            this.lblControl.Size = new System.Drawing.Size(31, 13);
            this.lblControl.TabIndex = 3;
            this.lblControl.Text = "Ctrl +";
            // 
            // cbxFirstLetter
            // 
            this.cbxFirstLetter.FormattingEnabled = true;
            this.cbxFirstLetter.Location = new System.Drawing.Point(43, 22);
            this.cbxFirstLetter.Name = "cbxFirstLetter";
            this.cbxFirstLetter.Size = new System.Drawing.Size(30, 21);
            this.cbxFirstLetter.TabIndex = 4;
            this.cbxFirstLetter.Text = "L";
            // 
            // cbxSecondLetter
            // 
            this.cbxSecondLetter.FormattingEnabled = true;
            this.cbxSecondLetter.Location = new System.Drawing.Point(97, 22);
            this.cbxSecondLetter.Name = "cbxSecondLetter";
            this.cbxSecondLetter.Size = new System.Drawing.Size(30, 21);
            this.cbxSecondLetter.TabIndex = 5;
            this.cbxSecondLetter.Text = "I";
            // 
            // lblPlus
            // 
            this.lblPlus.AutoSize = true;
            this.lblPlus.Location = new System.Drawing.Point(78, 25);
            this.lblPlus.Name = "lblPlus";
            this.lblPlus.Size = new System.Drawing.Size(13, 13);
            this.lblPlus.TabIndex = 6;
            this.lblPlus.Text = "+";
            // 
            // chkAutoLock
            // 
            this.chkAutoLock.AutoSize = true;
            this.chkAutoLock.Location = new System.Drawing.Point(9, 19);
            this.chkAutoLock.Name = "chkAutoLock";
            this.chkAutoLock.Size = new System.Drawing.Size(59, 17);
            this.chkAutoLock.TabIndex = 7;
            this.chkAutoLock.Text = "Activer";
            this.chkAutoLock.UseVisualStyleBackColor = true;
            this.chkAutoLock.CheckedChanged += new System.EventHandler(this.ChkAutoLockCheckedChanged);
            // 
            // gbxCombo
            // 
            this.gbxCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxCombo.Controls.Add(this.lblControl);
            this.gbxCombo.Controls.Add(this.cbxFirstLetter);
            this.gbxCombo.Controls.Add(this.cbxSecondLetter);
            this.gbxCombo.Controls.Add(this.lblPlus);
            this.gbxCombo.Location = new System.Drawing.Point(12, 12);
            this.gbxCombo.Name = "gbxCombo";
            this.gbxCombo.Size = new System.Drawing.Size(295, 56);
            this.gbxCombo.TabIndex = 8;
            this.gbxCombo.TabStop = false;
            this.gbxCombo.Text = "Combinaison de touches";
            // 
            // gbxAutoLock
            // 
            this.gbxAutoLock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxAutoLock.Controls.Add(this.lblMinutes);
            this.gbxAutoLock.Controls.Add(this.lblTime);
            this.gbxAutoLock.Controls.Add(this.nudTimer);
            this.gbxAutoLock.Controls.Add(this.chkAutoLock);
            this.gbxAutoLock.Location = new System.Drawing.Point(13, 198);
            this.gbxAutoLock.Name = "gbxAutoLock";
            this.gbxAutoLock.Size = new System.Drawing.Size(295, 68);
            this.gbxAutoLock.TabIndex = 9;
            this.gbxAutoLock.TabStop = false;
            this.gbxAutoLock.Text = "Verrouillage automatique";
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(120, 39);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(53, 13);
            this.lblMinutes.TabIndex = 10;
            this.lblMinutes.Text = "secondes";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(8, 39);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(65, 13);
            this.lblTime.TabIndex = 9;
            this.lblTime.Text = "Au bout de :";
            // 
            // nudTimer
            // 
            this.nudTimer.Enabled = false;
            this.nudTimer.Location = new System.Drawing.Point(74, 37);
            this.nudTimer.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nudTimer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimer.Name = "nudTimer";
            this.nudTimer.Size = new System.Drawing.Size(40, 20);
            this.nudTimer.TabIndex = 8;
            this.nudTimer.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // gbxLockedEvents
            // 
            this.gbxLockedEvents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxLockedEvents.Controls.Add(this.chkShutScreen);
            this.gbxLockedEvents.Controls.Add(this.chkMouseMove);
            this.gbxLockedEvents.Controls.Add(this.chkMouseClicks);
            this.gbxLockedEvents.Controls.Add(this.chkKeyboard);
            this.gbxLockedEvents.Location = new System.Drawing.Point(12, 74);
            this.gbxLockedEvents.Name = "gbxLockedEvents";
            this.gbxLockedEvents.Size = new System.Drawing.Size(295, 118);
            this.gbxLockedEvents.TabIndex = 10;
            this.gbxLockedEvents.TabStop = false;
            this.gbxLockedEvents.Text = "Réglages";
            // 
            // chkMouseMove
            // 
            this.chkMouseMove.AutoSize = true;
            this.chkMouseMove.Location = new System.Drawing.Point(9, 65);
            this.chkMouseMove.Name = "chkMouseMove";
            this.chkMouseMove.Size = new System.Drawing.Size(207, 17);
            this.chkMouseMove.TabIndex = 2;
            this.chkMouseMove.Text = "Verrouiller les mouvements de la souris";
            this.chkMouseMove.UseVisualStyleBackColor = true;
            // 
            // chkMouseClicks
            // 
            this.chkMouseClicks.AutoSize = true;
            this.chkMouseClicks.Checked = true;
            this.chkMouseClicks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMouseClicks.Location = new System.Drawing.Point(9, 42);
            this.chkMouseClicks.Name = "chkMouseClicks";
            this.chkMouseClicks.Size = new System.Drawing.Size(157, 17);
            this.chkMouseClicks.TabIndex = 1;
            this.chkMouseClicks.Text = "Verrouiller les clics de souris";
            this.chkMouseClicks.UseVisualStyleBackColor = true;
            // 
            // chkKeyboard
            // 
            this.chkKeyboard.AutoSize = true;
            this.chkKeyboard.Checked = true;
            this.chkKeyboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKeyboard.Location = new System.Drawing.Point(9, 19);
            this.chkKeyboard.Name = "chkKeyboard";
            this.chkKeyboard.Size = new System.Drawing.Size(117, 17);
            this.chkKeyboard.TabIndex = 0;
            this.chkKeyboard.Text = "Verrouiller le clavier";
            this.chkKeyboard.UseVisualStyleBackColor = true;
            // 
            // gbxHide
            // 
            this.gbxHide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxHide.Controls.Add(this.rbtOnLock);
            this.gbxHide.Controls.Add(this.rbtOnStart);
            this.gbxHide.Controls.Add(this.rbtNever);
            this.gbxHide.Location = new System.Drawing.Point(13, 272);
            this.gbxHide.Name = "gbxHide";
            this.gbxHide.Size = new System.Drawing.Size(295, 100);
            this.gbxHide.TabIndex = 11;
            this.gbxHide.TabStop = false;
            this.gbxHide.Text = "Masquer cette fenêtre :";
            // 
            // rbtOnLock
            // 
            this.rbtOnLock.AutoSize = true;
            this.rbtOnLock.Checked = true;
            this.rbtOnLock.Location = new System.Drawing.Point(9, 66);
            this.rbtOnLock.Name = "rbtOnLock";
            this.rbtOnLock.Size = new System.Drawing.Size(146, 17);
            this.rbtOnLock.TabIndex = 2;
            this.rbtOnLock.TabStop = true;
            this.rbtOnLock.Text = "Au verrouillage seulement";
            this.rbtOnLock.UseVisualStyleBackColor = true;
            // 
            // rbtOnStart
            // 
            this.rbtOnStart.AutoSize = true;
            this.rbtOnStart.Location = new System.Drawing.Point(9, 43);
            this.rbtOnStart.Name = "rbtOnStart";
            this.rbtOnStart.Size = new System.Drawing.Size(143, 17);
            this.rbtOnStart.TabIndex = 1;
            this.rbtOnStart.Text = "Au démarrage du service";
            this.rbtOnStart.UseVisualStyleBackColor = true;
            // 
            // rbtNever
            // 
            this.rbtNever.AutoSize = true;
            this.rbtNever.Location = new System.Drawing.Point(9, 20);
            this.rbtNever.Name = "rbtNever";
            this.rbtNever.Size = new System.Drawing.Size(57, 17);
            this.rbtNever.TabIndex = 0;
            this.rbtNever.Text = "Jamais";
            this.rbtNever.UseVisualStyleBackColor = true;
            // 
            // chkShutScreen
            // 
            this.chkShutScreen.AutoSize = true;
            this.chkShutScreen.Location = new System.Drawing.Point(9, 88);
            this.chkShutScreen.Name = "chkShutScreen";
            this.chkShutScreen.Size = new System.Drawing.Size(132, 17);
            this.chkShutScreen.TabIndex = 3;
            this.chkShutScreen.Text = "Mettre l\'écran en veille";
            this.chkShutScreen.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AcceptButton = this.btnStart;
            this.ClientSize = new System.Drawing.Size(320, 446);
            this.Controls.Add(this.gbxHide);
            this.Controls.Add(this.gbxLockedEvents);
            this.Controls.Add(this.gbxAutoLock);
            this.Controls.Add(this.gbxCombo);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(256, 453);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input Locker";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.contextMenuStrip1.ResumeLayout(false);
            this.gbxCombo.ResumeLayout(false);
            this.gbxCombo.PerformLayout();
            this.gbxAutoLock.ResumeLayout(false);
            this.gbxAutoLock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimer)).EndInit();
            this.gbxLockedEvents.ResumeLayout(false);
            this.gbxLockedEvents.PerformLayout();
            this.gbxHide.ResumeLayout(false);
            this.gbxHide.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnStart;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private Label lblControl;
        private ComboBox cbxFirstLetter;
        private ComboBox cbxSecondLetter;
        private Label lblPlus;
        private CheckBox chkAutoLock;
        private GroupBox gbxCombo;
        private GroupBox gbxAutoLock;
        private Label lblMinutes;
        private Label lblTime;
        private NumericUpDown nudTimer;
        private GroupBox gbxLockedEvents;
        private CheckBox chkMouseMove;
        private CheckBox chkMouseClicks;
        private CheckBox chkKeyboard;
        private GroupBox gbxHide;
        private RadioButton rbtOnLock;
        private RadioButton rbtOnStart;
        private RadioButton rbtNever;
        private ToolStripMenuItem afficherLaFenêtreToolStripMenuItem;
        private CheckBox chkShutScreen;
    }
}

