namespace Poker
{
    using System;
    using System.Windows.Forms;

    public partial class AddChipsForm : Form
    {
        public int a;

        public AddChipsForm()
        {
            this.InitializeComponent();
            this.ControlBox = false;
            this.ranOutOfChipsLabel.BorderStyle = BorderStyle.FixedSingle;
        }

        public void ButtonAddChips_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (int.Parse(this.addChipsTextBox.Text) > 100000000)
            {
                MessageBox.Show("The maximium chips you can add is 100000000");
                return;
            }

            if (!int.TryParse(this.addChipsTextBox.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
            }
            else if (int.TryParse(this.addChipsTextBox.Text, out parsedValue)
                     && int.Parse(this.addChipsTextBox.Text) <= 100000000)
            {
                this.a = int.Parse(this.addChipsTextBox.Text);
                this.Close();
            }
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            var message = "Are you sure?";
            var title = "Quit";
            var result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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