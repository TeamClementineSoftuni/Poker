namespace Poker
{
    using System.ComponentModel;
    using System.Windows.Forms;

    partial class AddChipsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private Label ranOutOfChipsLabel;
        private Button buttonAddChips;
        private Button buttonExit;
        private NumericUpDown addChipsUpDown;

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
            this.ranOutOfChipsLabel = new System.Windows.Forms.Label();
            this.buttonAddChips = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.addChipsUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.addChipsUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ranOutOfChipsLabel
            // 
            this.ranOutOfChipsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ranOutOfChipsLabel.Location = new System.Drawing.Point(48, 49);
            this.ranOutOfChipsLabel.Name = "ranOutOfChipsLabel";
            this.ranOutOfChipsLabel.Size = new System.Drawing.Size(176, 23);
            this.ranOutOfChipsLabel.TabIndex = 0;
            this.ranOutOfChipsLabel.Text = "You ran out of chips !";
            this.ranOutOfChipsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonAddChips
            // 
            this.buttonAddChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddChips.Location = new System.Drawing.Point(12, 226);
            this.buttonAddChips.Name = "buttonAddChips";
            this.buttonAddChips.Size = new System.Drawing.Size(75, 23);
            this.buttonAddChips.TabIndex = 1;
            this.buttonAddChips.Text = "Add Chips";
            this.buttonAddChips.UseVisualStyleBackColor = true;
            this.buttonAddChips.Click += new System.EventHandler(this.ButtonAddChips_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExit.Location = new System.Drawing.Point(197, 226);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // addChipsUpDown
            // 
            this.addChipsUpDown.Location = new System.Drawing.Point(91, 226);
            this.addChipsUpDown.Name = "addChipsUpDown";
            this.addChipsUpDown.Size = new System.Drawing.Size(100, 20);
            this.addChipsUpDown.TabIndex = 4;
            // 
            // AddChipsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.addChipsUpDown);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonAddChips);
            this.Controls.Add(this.ranOutOfChipsLabel);
            this.Name = "AddChipsForm";
            this.Text = "You Ran Out Of Chips";
            ((System.ComponentModel.ISupportInitialize)(this.addChipsUpDown)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion


    }
}