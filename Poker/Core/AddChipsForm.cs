namespace Poker
{
    using System;
    using System.Windows.Forms;

    public partial class AddChipsForm : Form
    {
        public int AddChips { get; set; }

        public AddChipsForm()
        {
            this.InitializeComponent();
            this.ControlBox = false;
            this.ranOutOfChipsLabel.BorderStyle = BorderStyle.FixedSingle;
        }

        public void ButtonAddChips_Click(object sender, EventArgs e)
        {
            this.AddChips = (int)this.addChipsUpDown.Value;
            this.Close();

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