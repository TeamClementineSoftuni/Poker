using System.Windows.Forms;
using System.Drawing;

namespace Poker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bFold = new System.Windows.Forms.Button();
            this.bCheck = new System.Windows.Forms.Button();
            this.bCall = new System.Windows.Forms.Button();
            this.bRaise = new System.Windows.Forms.Button();
            this.bOptions = new System.Windows.Forms.Button();
            this.bBB = new System.Windows.Forms.Button();
            this.bSB = new System.Windows.Forms.Button();
            this.bAdd = new System.Windows.Forms.Button();
            this.pbTimer = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAdd = new System.Windows.Forms.TextBox();
            this.tbPot = new System.Windows.Forms.TextBox();
            this.tbBB = new System.Windows.Forms.TextBox();
            this.tbSB = new System.Windows.Forms.TextBox();
            this.tbRaise = new System.Windows.Forms.TextBox();
            this.b5Status = new System.Windows.Forms.Label();
            this.b4Status = new System.Windows.Forms.Label();
            this.b3Status = new System.Windows.Forms.Label();
            this.b2Status = new System.Windows.Forms.Label();
            this.b1Status = new System.Windows.Forms.Label();
            this.pStatus = new System.Windows.Forms.Label();
            this.bot5ChipsTextBox = new System.Windows.Forms.TextBox();
            this.bot4ChipsTextBox = new System.Windows.Forms.TextBox();
            this.bot3ChipsTextBox = new System.Windows.Forms.TextBox();
            this.bot2ChipsTextBox = new System.Windows.Forms.TextBox();
            this.bot1ChipsTextBox = new System.Windows.Forms.TextBox();
            this.humanChipsTextBox = new System.Windows.Forms.TextBox();
            this.humanPlayerPanel = new System.Windows.Forms.Panel();
            this.bot1Panel = new System.Windows.Forms.Panel();
            this.bot2Panel = new System.Windows.Forms.Panel();
            this.bot3Panel = new System.Windows.Forms.Panel();
            this.bot4Panel = new System.Windows.Forms.Panel();
            this.bot5Panel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // bFold
            // 
            this.bFold.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bFold.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bFold.Location = new System.Drawing.Point(335, 660);
            this.bFold.Name = "bFold";
            this.bFold.Size = new System.Drawing.Size(130, 62);
            this.bFold.TabIndex = 0;
            this.bFold.Text = "Fold";
            this.bFold.UseVisualStyleBackColor = true;
            this.bFold.Click += new System.EventHandler(this.bFold_Click);
            // 
            // bCheck
            // 
            this.bCheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCheck.Location = new System.Drawing.Point(494, 660);
            this.bCheck.Name = "bCheck";
            this.bCheck.Size = new System.Drawing.Size(134, 62);
            this.bCheck.TabIndex = 2;
            this.bCheck.Text = "Check";
            this.bCheck.UseVisualStyleBackColor = true;
            this.bCheck.Click += new System.EventHandler(this.bCheck_Click);
            // 
            // bCall
            // 
            this.bCall.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCall.Location = new System.Drawing.Point(667, 661);
            this.bCall.Name = "bCall";
            this.bCall.Size = new System.Drawing.Size(126, 62);
            this.bCall.TabIndex = 3;
            this.bCall.Text = "Call";
            this.bCall.UseVisualStyleBackColor = true;
            this.bCall.Click += new System.EventHandler(this.bCall_Click);
            // 
            // bRaise
            // 
            this.bRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bRaise.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bRaise.Location = new System.Drawing.Point(835, 661);
            this.bRaise.Name = "bRaise";
            this.bRaise.Size = new System.Drawing.Size(124, 62);
            this.bRaise.TabIndex = 4;
            this.bRaise.Text = "Raise";
            this.bRaise.UseVisualStyleBackColor = true;
            this.bRaise.Click += new System.EventHandler(this.bRaise_Click);
            // 
            // bOptions
            // 
            this.bOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOptions.Location = new System.Drawing.Point(12, 12);
            this.bOptions.Name = "bOptions";
            this.bOptions.Size = new System.Drawing.Size(75, 36);
            this.bOptions.TabIndex = 15;
            this.bOptions.Text = "BB/SB";
            this.bOptions.UseVisualStyleBackColor = true;
            this.bOptions.Click += new System.EventHandler(this.bOptions_Click);
            // 
            // bBB
            // 
            this.bBB.Location = new System.Drawing.Point(12, 254);
            this.bBB.Name = "bBB";
            this.bBB.Size = new System.Drawing.Size(75, 23);
            this.bBB.TabIndex = 16;
            this.bBB.Text = "Big Blind";
            this.bBB.UseVisualStyleBackColor = true;
            this.bBB.Click += new System.EventHandler(this.bBB_Click);
            // 
            // bSB
            // 
            this.bSB.Location = new System.Drawing.Point(12, 199);
            this.bSB.Name = "bSB";
            this.bSB.Size = new System.Drawing.Size(75, 23);
            this.bSB.TabIndex = 18;
            this.bSB.Text = "Small Blind";
            this.bSB.UseVisualStyleBackColor = true;
            this.bSB.Click += new System.EventHandler(this.bSB_Click);
            // 
            // bAdd
            // 
            this.bAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAdd.Location = new System.Drawing.Point(12, 697);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 25);
            this.bAdd.TabIndex = 7;
            this.bAdd.Text = "AddChips";
            this.bAdd.UseVisualStyleBackColor = true;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // pbTimer
            // 
            this.pbTimer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pbTimer.BackColor = System.Drawing.SystemColors.Control;
            this.pbTimer.Location = new System.Drawing.Point(335, 631);
            this.pbTimer.Maximum = 1000;
            this.pbTimer.Name = "pbTimer";
            this.pbTimer.Size = new System.Drawing.Size(667, 23);
            this.pbTimer.TabIndex = 5;
            this.pbTimer.Value = 1000;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(654, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pot";
            // 
            // tbAdd
            // 
            this.tbAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbAdd.Location = new System.Drawing.Point(93, 700);
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Size = new System.Drawing.Size(125, 20);
            this.tbAdd.TabIndex = 8;
            // 
            // tbPot
            // 
            this.tbPot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbPot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbPot.Location = new System.Drawing.Point(606, 212);
            this.tbPot.Name = "tbPot";
            this.tbPot.Size = new System.Drawing.Size(125, 23);
            this.tbPot.TabIndex = 14;
            this.tbPot.Text = "0";
            // 
            // tbBB
            // 
            this.tbBB.Location = new System.Drawing.Point(12, 283);
            this.tbBB.Name = "tbBB";
            this.tbBB.Size = new System.Drawing.Size(75, 20);
            this.tbBB.TabIndex = 19;
            this.tbBB.Text = "500";
            // 
            // tbSB
            // 
            this.tbSB.Location = new System.Drawing.Point(12, 228);
            this.tbSB.Name = "tbSB";
            this.tbSB.Size = new System.Drawing.Size(75, 20);
            this.tbSB.TabIndex = 17;
            this.tbSB.Text = "250";
            // 
            // tbRaise
            // 
            this.tbRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbRaise.Location = new System.Drawing.Point(965, 703);
            this.tbRaise.Name = "tbRaise";
            this.tbRaise.Size = new System.Drawing.Size(108, 20);
            this.tbRaise.TabIndex = 0;
            // 
            // b5Status
            // 
            this.b5Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b5Status.Location = new System.Drawing.Point(1012, 579);
            this.b5Status.Name = "b5Status";
            this.b5Status.Size = new System.Drawing.Size(152, 32);
            this.b5Status.TabIndex = 26;
            // 
            // b4Status
            // 
            this.b4Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b4Status.Location = new System.Drawing.Point(970, 107);
            this.b4Status.Name = "b4Status";
            this.b4Status.Size = new System.Drawing.Size(123, 32);
            this.b4Status.TabIndex = 27;
            // 
            // b3Status
            // 
            this.b3Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b3Status.Location = new System.Drawing.Point(755, 107);
            this.b3Status.Name = "b3Status";
            this.b3Status.Size = new System.Drawing.Size(125, 32);
            this.b3Status.TabIndex = 28;
            // 
            // b2Status
            // 
            this.b2Status.Location = new System.Drawing.Point(276, 107);
            this.b2Status.Name = "b2Status";
            this.b2Status.Size = new System.Drawing.Size(133, 32);
            this.b2Status.TabIndex = 31;
            // 
            // b1Status
            // 
            this.b1Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.b1Status.Location = new System.Drawing.Point(181, 579);
            this.b1Status.Name = "b1Status";
            this.b1Status.Size = new System.Drawing.Size(142, 32);
            this.b1Status.TabIndex = 29;
            // 
            // pStatus
            // 
            this.pStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pStatus.Location = new System.Drawing.Point(755, 579);
            this.pStatus.Name = "pStatus";
            this.pStatus.Size = new System.Drawing.Size(163, 32);
            this.pStatus.TabIndex = 30;
            // 
            // bot5ChipsTextBox
            // 
            this.bot5ChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bot5ChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bot5ChipsTextBox.Location = new System.Drawing.Point(1012, 553);
            this.bot5ChipsTextBox.Name = "bot5ChipsTextBox";
            this.bot5ChipsTextBox.Size = new System.Drawing.Size(152, 23);
            this.bot5ChipsTextBox.TabIndex = 9;
            this.bot5ChipsTextBox.Text = "Chips : 0";
            // 
            // bot4ChipsTextBox
            // 
            this.bot4ChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bot4ChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bot4ChipsTextBox.Location = new System.Drawing.Point(970, 81);
            this.bot4ChipsTextBox.Name = "bot4ChipsTextBox";
            this.bot4ChipsTextBox.Size = new System.Drawing.Size(123, 23);
            this.bot4ChipsTextBox.TabIndex = 10;
            this.bot4ChipsTextBox.Text = "Chips : 0";
            // 
            // bot3ChipsTextBox
            // 
            this.bot3ChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bot3ChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bot3ChipsTextBox.Location = new System.Drawing.Point(755, 81);
            this.bot3ChipsTextBox.Name = "bot3ChipsTextBox";
            this.bot3ChipsTextBox.Size = new System.Drawing.Size(125, 23);
            this.bot3ChipsTextBox.TabIndex = 11;
            this.bot3ChipsTextBox.Text = "Chips : 0";
            // 
            // bot2ChipsTextBox
            // 
            this.bot2ChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bot2ChipsTextBox.Location = new System.Drawing.Point(276, 81);
            this.bot2ChipsTextBox.Name = "bot2ChipsTextBox";
            this.bot2ChipsTextBox.Size = new System.Drawing.Size(133, 23);
            this.bot2ChipsTextBox.TabIndex = 12;
            this.bot2ChipsTextBox.Text = "Chips : 0";
            // 
            // bot1ChipsTextBox
            // 
            this.bot1ChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bot1ChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bot1ChipsTextBox.Location = new System.Drawing.Point(181, 553);
            this.bot1ChipsTextBox.Name = "bot1ChipsTextBox";
            this.bot1ChipsTextBox.Size = new System.Drawing.Size(142, 23);
            this.bot1ChipsTextBox.TabIndex = 13;
            this.bot1ChipsTextBox.Text = "Chips : 0";
            // 
            // humanChipsTextBox
            // 
            this.humanChipsTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.humanChipsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.humanChipsTextBox.Location = new System.Drawing.Point(755, 553);
            this.humanChipsTextBox.Name = "humanChipsTextBox";
            this.humanChipsTextBox.Size = new System.Drawing.Size(163, 23);
            this.humanChipsTextBox.TabIndex = 6;
            this.humanChipsTextBox.Text = "Chips : 0";
            // 
            // humanPlayerPanel
            // 
            this.humanPlayerPanel.BackColor = System.Drawing.Color.DarkBlue;
            this.humanPlayerPanel.Location = new System.Drawing.Point(569, 461);
            this.humanPlayerPanel.Name = "humanPlayerPanel";
            this.humanPlayerPanel.Size = new System.Drawing.Size(180, 150);
            this.humanPlayerPanel.TabIndex = 33;
            this.humanPlayerPanel.Visible = false;
            // 
            // bot1Panel
            // 
            this.bot1Panel.BackColor = System.Drawing.Color.DarkBlue;
            this.bot1Panel.Location = new System.Drawing.Point(-5, 409);
            this.bot1Panel.Name = "bot1Panel";
            this.bot1Panel.Size = new System.Drawing.Size(180, 150);
            this.bot1Panel.TabIndex = 32;
            this.bot1Panel.Visible = false;
            // 
            // bot2Panel
            // 
            this.bot2Panel.BackColor = System.Drawing.Color.DarkBlue;
            this.bot2Panel.Location = new System.Drawing.Point(73, 59);
            this.bot2Panel.Name = "bot2Panel";
            this.bot2Panel.Size = new System.Drawing.Size(180, 150);
            this.bot2Panel.TabIndex = 35;
            this.bot2Panel.Visible = false;
            // 
            // bot3Panel
            // 
            this.bot3Panel.BackColor = System.Drawing.Color.DarkBlue;
            this.bot3Panel.Location = new System.Drawing.Point(569, 12);
            this.bot3Panel.Name = "bot3Panel";
            this.bot3Panel.Size = new System.Drawing.Size(180, 150);
            this.bot3Panel.TabIndex = 36;
            this.bot3Panel.Visible = false;
            // 
            // bot4Panel
            // 
            this.bot4Panel.BackColor = System.Drawing.Color.DarkBlue;
            this.bot4Panel.Location = new System.Drawing.Point(1113, 59);
            this.bot4Panel.Name = "bot4Panel";
            this.bot4Panel.Size = new System.Drawing.Size(180, 150);
            this.bot4Panel.TabIndex = 37;
            this.bot4Panel.Visible = false;
            // 
            // bot5Panel
            // 
            this.bot5Panel.BackColor = System.Drawing.Color.DarkBlue;
            this.bot5Panel.Location = new System.Drawing.Point(1158, 397);
            this.bot5Panel.Name = "bot5Panel";
            this.bot5Panel.Size = new System.Drawing.Size(180, 150);
            this.bot5Panel.TabIndex = 34;
            this.bot5Panel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.bot4Panel);
            this.Controls.Add(this.bot3Panel);
            this.Controls.Add(this.bot2Panel);
            this.Controls.Add(this.bot5Panel);
            this.Controls.Add(this.humanPlayerPanel);
            this.Controls.Add(this.bot1Panel);
            this.Controls.Add(this.tbRaise);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.b2Status);
            this.Controls.Add(this.pStatus);
            this.Controls.Add(this.b1Status);
            this.Controls.Add(this.b3Status);
            this.Controls.Add(this.b4Status);
            this.Controls.Add(this.b5Status);
            this.Controls.Add(this.tbBB);
            this.Controls.Add(this.bSB);
            this.Controls.Add(this.tbSB);
            this.Controls.Add(this.bBB);
            this.Controls.Add(this.bOptions);
            this.Controls.Add(this.tbPot);
            this.Controls.Add(this.bot1ChipsTextBox);
            this.Controls.Add(this.bot2ChipsTextBox);
            this.Controls.Add(this.bot3ChipsTextBox);
            this.Controls.Add(this.bot4ChipsTextBox);
            this.Controls.Add(this.bot5ChipsTextBox);
            this.Controls.Add(this.tbAdd);
            this.Controls.Add(this.bAdd);
            this.Controls.Add(this.humanChipsTextBox);
            this.Controls.Add(this.pbTimer);
            this.Controls.Add(this.bRaise);
            this.Controls.Add(this.bCall);
            this.Controls.Add(this.bCheck);
            this.Controls.Add(this.bFold);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "GLS Texas Poker";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Layout_Change);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button bFold;
        private Button bCheck;
        private Button bCall;
        private Button bRaise;
        private Button bAdd;
        private Button bOptions;
        private Button bBB;
        private Button bSB;

        private ProgressBar pbTimer;

        private TextBox tbRaise;
        private TextBox tbAdd;
        private TextBox humanChipsTextBox;
        private TextBox tbSB;
        private TextBox tbBB;

        private TextBox bot5ChipsTextBox;
        private TextBox bot4ChipsTextBox;
        private TextBox bot3ChipsTextBox;
        private TextBox bot2ChipsTextBox;
        private TextBox bot1ChipsTextBox;
        private TextBox tbPot;
        
        private Label label1;
        
        private Label b5Status;
        private Label b4Status;
        private Label b3Status;
        private Label b1Status;
        private Label pStatus;
        private Label b2Status;
        
        private Panel humanPlayerPanel;
        private Panel bot1Panel;
        private Panel bot2Panel;
        private Panel bot3Panel;
        private Panel bot4Panel;
        private Panel bot5Panel;

    }
}

