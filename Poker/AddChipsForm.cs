using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Windows.Forms;

namespace Poker
{
    public partial class AddChipsForm : Form
    {
        public int a=0;

        public AddChipsForm()
        {
            InitializeComponent();
            ControlBox = false;
            ranOutOfChipsLabel.BorderStyle = BorderStyle.FixedSingle;
        }

        public void ButtonAddChips_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (int.Parse(addChipsTextBox.Text) > 100000000)
            {
                MessageBox.Show("The maximium chips you can add is 100000000");
                return;
            }

            if (!int.TryParse(addChipsTextBox.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            else if (int.TryParse(addChipsTextBox.Text, out parsedValue) && int.Parse(addChipsTextBox.Text) <= 100000000)
            {
                a = int.Parse(addChipsTextBox.Text);
                this.Close();
            }
        }
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            var message = "Are you sure?";
            var title = "Quit";
            var result = MessageBox.Show(message,title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    Application.Exit();
                    break;
            }
        }
    }
}
