namespace Poker
{
    using System.Windows.Forms;
    using System.Drawing;

    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Button buttonFold;
        private Button buttonCheck;
        private Button buttonCall;
        private Button buttonRaise;
        private Button buttonAddChips;
        private Button buttonOptions;
        private Button buttonBigBlind;
        private Button buttonSmallBlind;
        private ProgressBar timerProgressBar;
        private TextBox raiseAmountTextBox;
        private TextBox addChipsTextBox;
        private TextBox humanChipsTextBox;
        private TextBox smallBlindTextBox;
        private TextBox bigBlindTextBox;
        private TextBox bot5ChipsTextBox;
        private TextBox bot4ChipsTextBox;
        private TextBox bot3ChipsTextBox;
        private TextBox bot2ChipsTextBox;
        private TextBox bot1ChipsTextBox;
        private TextBox potTextBox;
        private Label potLabel;
        private Label bot5StatusLabel;
        private Label bot4StatusLabel;
        private Label bot3StatusLabel;
        private Label bot1StatusLabel;
        private Label humanStatusLabel;
        private Label bot2StatusLabel;
        private NumericUpDown numericUpDown1;

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
            this.buttonFold = new System.Windows.Forms.Button();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.buttonCall = new System.Windows.Forms.Button();
            this.buttonRaise = new System.Windows.Forms.Button();
            this.buttonOptions = new System.Windows.Forms.Button();
            this.buttonBigBlind = new System.Windows.Forms.Button();
            this.buttonSmallBlind = new System.Windows.Forms.Button();
            this.buttonAddChips = new System.Windows.Forms.Button();
            this.timerProgressBar = new System.Windows.Forms.ProgressBar();
            this.potLabel = new System.Windows.Forms.Label();
            this.addChipsTextBox = new System.Windows.Forms.TextBox();
            this.potTextBox = new System.Windows.Forms.TextBox();
            this.bigBlindTextBox = new System.Windows.Forms.TextBox();
            this.smallBlindTextBox = new System.Windows.Forms.TextBox();
            this.raiseAmountTextBox = new System.Windows.Forms.TextBox();
            this.bot5StatusLabel = new System.Windows.Forms.Label();
            this.bot4StatusLabel = new System.Windows.Forms.Label();
            this.bot3StatusLabel = new System.Windows.Forms.Label();
            this.bot2StatusLabel = new System.Windows.Forms.Label();
            this.bot1StatusLabel = new System.Windows.Forms.Label();
            this.humanStatusLabel = new System.Windows.Forms.Label();
            this.bot5ChipsTextBox = new System.Windows.Forms.TextBox();
            this.bot4ChipsTextBox = new System.Windows.Forms.TextBox();
            this.bot3ChipsTextBox = new System.Windows.Forms.TextBox();
            this.bot2ChipsTextBox = new System.Windows.Forms.TextBox();
            this.bot1ChipsTextBox = new System.Windows.Forms.TextBox();
            this.humanChipsTextBox = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonFold
            // 
            this.buttonFold.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonFold.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFold.Location = new System.Drawing.Point(335, 660);
            this.buttonFold.Name = "buttonFold";
            this.buttonFold.Size = new System.Drawing.Size(130, 62);
            this.buttonFold.TabIndex = 0;
            this.buttonFold.Text = "Fold";
            this.buttonFold.UseVisualStyleBackColor = true;
            this.buttonFold.Click += new System.EventHandler(this.ButtonFold_Click);
            // 
            // buttonCheck
            // 
            this.buttonCheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCheck.Location = new System.Drawing.Point(494, 660);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(134, 62);
            this.buttonCheck.TabIndex = 2;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.ButtonCheck_Click);
            // 
            // buttonCall
            // 
            this.buttonCall.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCall.Location = new System.Drawing.Point(667, 661);
            this.buttonCall.Name = "buttonCall";
            this.buttonCall.Size = new System.Drawing.Size(126, 62);
            this.buttonCall.TabIndex = 3;
            this.buttonCall.Text = "Call";
            this.buttonCall.UseVisualStyleBackColor = true;
            this.buttonCall.Click += new System.EventHandler(this.ButtonCall_Click);
            // 
            // buttonRaise
            // 
            this.buttonRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonRaise.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRaise.Location = new System.Drawing.Point(835, 661);
            this.buttonRaise.Name = "buttonRaise";
            this.buttonRaise.Size = new System.Drawing.Size(124, 62);
            this.buttonRaise.TabIndex = 4;
            this.buttonRaise.Text = "Raise";
            this.buttonRaise.UseVisualStyleBackColor = true;
            this.buttonRaise.Click += new System.EventHandler(this.ButtonRaise_Click);
            // 
            // buttonOptions
            // 
            this.buttonOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOptions.Location = new System.Drawing.Point(12, 12);
            this.buttonOptions.Name = "buttonOptions";
            this.buttonOptions.Size = new System.Drawing.Size(75, 36);
            this.buttonOptions.TabIndex = 15;
            this.buttonOptions.Text = "BB/SB";
            this.buttonOptions.UseVisualStyleBackColor = true;
            this.buttonOptions.Click += new System.EventHandler(this.ButtonOptions_Click);
            // 
            // buttonBigBlind
            // 
            this.buttonBigBlind.Location = new System.Drawing.Point(12, 254);
            this.buttonBigBlind.Name = "buttonBigBlind";
            this.buttonBigBlind.Size = new System.Drawing.Size(75, 23);
            this.buttonBigBlind.TabIndex = 16;
            this.buttonBigBlind.Text = "Big Blind";
            this.buttonBigBlind.UseVisualStyleBackColor = true;
            this.buttonBigBlind.Click += new System.EventHandler(this.ButtonBigBlind_Click);
            // 
            // buttonSmallBlind
            // 
            this.buttonSmallBlind.Location = new System.Drawing.Point(12, 199);
            this.buttonSmallBlind.Name = "buttonSmallBlind";
            this.buttonSmallBlind.Size = new System.Drawing.Size(75, 23);
            this.buttonSmallBlind.TabIndex = 18;
            this.buttonSmallBlind.Text = "Small Blind";
            this.buttonSmallBlind.UseVisualStyleBackColor = true;
            this.buttonSmallBlind.Click += new System.EventHandler(this.ButtonSmallBlind_Click);
            // 
            // buttonAddChips
            // 
            this.buttonAddChips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddChips.Location = new System.Drawing.Point(12, 697);
            this.buttonAddChips.Name = "buttonAddChips";
            this.buttonAddChips.Size = new System.Drawing.Size(75, 25);
            this.buttonAddChips.TabIndex = 7;
            this.buttonAddChips.Text = "AddChips";
            this.buttonAddChips.UseVisualStyleBackColor = true;
            this.buttonAddChips.Click += new System.EventHandler(this.ButtonAddChips_Click);
            // 
            // timerProgressBar
            // 
            this.timerProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.timerProgressBar.BackColor = System.Drawing.SystemColors.Control;
            this.timerProgressBar.Location = new System.Drawing.Point(335, 631);
            this.timerProgressBar.Maximum = 1000;
            this.timerProgressBar.Name = "timerProgressBar";
            this.timerProgressBar.Size = new System.Drawing.Size(667, 23);
            this.timerProgressBar.TabIndex = 5;
            this.timerProgressBar.Value = 1000;
            // 
            // potLabel
            // 
            this.potLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.potLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.potLabel.Location = new System.Drawing.Point(654, 188);
            this.potLabel.Name = "potLabel";
            this.potLabel.Size = new System.Drawing.Size(31, 21);
            this.potLabel.TabIndex = 0;
            this.potLabel.Text = "Pot";
            // 
            // addChipsTextBox
            // 
            this.addChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addChipsTextBox.Location = new System.Drawing.Point(93, 700);
            this.addChipsTextBox.Name = "addChipsTextBox";
            this.addChipsTextBox.Size = new System.Drawing.Size(125, 20);
            this.addChipsTextBox.TabIndex = 8;
            // 
            // potTextBox
            // 
            this.potTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.potTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.potTextBox.Location = new System.Drawing.Point(606, 212);
            this.potTextBox.Name = "potTextBox";
            this.potTextBox.ReadOnly = true;
            this.potTextBox.Size = new System.Drawing.Size(125, 23);
            this.potTextBox.TabIndex = 14;
            this.potTextBox.Text = "0";
            // 
            // bigBlindTextBox
            // 
            this.bigBlindTextBox.Location = new System.Drawing.Point(12, 283);
            this.bigBlindTextBox.Name = "bigBlindTextBox";
            this.bigBlindTextBox.Size = new System.Drawing.Size(75, 20);
            this.bigBlindTextBox.TabIndex = 19;
            this.bigBlindTextBox.Text = "500";
            // 
            // smallBlindTextBox
            // 
            this.smallBlindTextBox.Location = new System.Drawing.Point(12, 228);
            this.smallBlindTextBox.Name = "smallBlindTextBox";
            this.smallBlindTextBox.Size = new System.Drawing.Size(75, 20);
            this.smallBlindTextBox.TabIndex = 17;
            this.smallBlindTextBox.Text = "250";
            // 
            // raiseAmountTextBox
            // 
            this.raiseAmountTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.raiseAmountTextBox.Location = new System.Drawing.Point(965, 703);
            this.raiseAmountTextBox.Name = "raiseAmountTextBox";
            this.raiseAmountTextBox.Size = new System.Drawing.Size(108, 20);
            this.raiseAmountTextBox.TabIndex = 0;
            // 
            // bot5StatusLabel
            // 
            this.bot5StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bot5StatusLabel.Location = new System.Drawing.Point(1012, 579);
            this.bot5StatusLabel.Name = "bot5StatusLabel";
            this.bot5StatusLabel.Size = new System.Drawing.Size(152, 32);
            this.bot5StatusLabel.TabIndex = 26;
            // 
            // bot4StatusLabel
            // 
            this.bot4StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bot4StatusLabel.Location = new System.Drawing.Point(970, 107);
            this.bot4StatusLabel.Name = "bot4StatusLabel";
            this.bot4StatusLabel.Size = new System.Drawing.Size(123, 32);
            this.bot4StatusLabel.TabIndex = 27;
            // 
            // bot3StatusLabel
            // 
            this.bot3StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bot3StatusLabel.Location = new System.Drawing.Point(755, 107);
            this.bot3StatusLabel.Name = "bot3StatusLabel";
            this.bot3StatusLabel.Size = new System.Drawing.Size(125, 32);
            this.bot3StatusLabel.TabIndex = 28;
            // 
            // bot2StatusLabel
            // 
            this.bot2StatusLabel.Location = new System.Drawing.Point(276, 107);
            this.bot2StatusLabel.Name = "bot2StatusLabel";
            this.bot2StatusLabel.Size = new System.Drawing.Size(133, 32);
            this.bot2StatusLabel.TabIndex = 31;
            // 
            // bot1StatusLabel
            // 
            this.bot1StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bot1StatusLabel.Location = new System.Drawing.Point(181, 579);
            this.bot1StatusLabel.Name = "bot1StatusLabel";
            this.bot1StatusLabel.Size = new System.Drawing.Size(142, 32);
            this.bot1StatusLabel.TabIndex = 29;
            // 
            // humanStatusLabel
            // 
            this.humanStatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.humanStatusLabel.Location = new System.Drawing.Point(755, 579);
            this.humanStatusLabel.Name = "humanStatusLabel";
            this.humanStatusLabel.Size = new System.Drawing.Size(163, 32);
            this.humanStatusLabel.TabIndex = 30;
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
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(965, 677);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 32;
            this.numericUpDown1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numericUpDown1_KeyUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.raiseAmountTextBox);
            this.Controls.Add(this.potLabel);
            this.Controls.Add(this.bot2StatusLabel);
            this.Controls.Add(this.humanStatusLabel);
            this.Controls.Add(this.bot1StatusLabel);
            this.Controls.Add(this.bot3StatusLabel);
            this.Controls.Add(this.bot4StatusLabel);
            this.Controls.Add(this.bot5StatusLabel);
            this.Controls.Add(this.bigBlindTextBox);
            this.Controls.Add(this.buttonSmallBlind);
            this.Controls.Add(this.smallBlindTextBox);
            this.Controls.Add(this.buttonBigBlind);
            this.Controls.Add(this.buttonOptions);
            this.Controls.Add(this.potTextBox);
            this.Controls.Add(this.bot1ChipsTextBox);
            this.Controls.Add(this.bot2ChipsTextBox);
            this.Controls.Add(this.bot3ChipsTextBox);
            this.Controls.Add(this.bot4ChipsTextBox);
            this.Controls.Add(this.bot5ChipsTextBox);
            this.Controls.Add(this.addChipsTextBox);
            this.Controls.Add(this.buttonAddChips);
            this.Controls.Add(this.humanChipsTextBox);
            this.Controls.Add(this.timerProgressBar);
            this.Controls.Add(this.buttonRaise);
            this.Controls.Add(this.buttonCall);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.buttonFold);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "GLS Texas Poker";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Layout_Change);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}

