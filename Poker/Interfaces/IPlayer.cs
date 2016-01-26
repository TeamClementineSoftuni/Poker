namespace Poker.Interfaces
{
    using System.Drawing;
    using System.Windows.Forms;

    using Poker.Models;

    public interface IPlayer
    {
        /// <summary>
        /// Get and set player location on screen. 
        /// </summary>
       Point Location { get; set; }

        /// <summary>
        /// Gets and set first card.
        /// </summary>
        PictureBox Card1PictureBox{ get; set; }

        /// <summary>
        /// Gets and set second card.
        /// </summary>
        PictureBox Card2PictureBox{ get; set; }

        /// <summary>
        /// Gets and set name of the player.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets and sets hand of the player.
        /// </summary>
        IHand Hand { get; set; }

        /// <summary>
        /// Gets and set the panel.
        /// </summary>
        Panel Panel { get; set; }

        /// <summary>
        /// Gets and set textbox for chips.
        /// </summary>
        TextBox ChipsTextBox { get; set; }

        /// <summary>
        /// Gets and set player chips.
        /// </summary>
        ChipsSet ChipsSet { get; set; }

        /// <summary>
        /// Gets and set player status.
        /// </summary>
        Label StatusLabel { get; set; }

        /// <summary>
        /// Get and set player result.
        /// </summary>
        IResult Result { get; set; }

        /// <summary>
        /// Show is player folded.
        /// </summary>
        bool IsFolded { get; set; }

        /// <summary>
        /// Gets and set player turn.
        /// </summary>
        bool Turn { get; set; }

        /// <summary>
        ///  Holds call amount of player.
        /// </summary>
        int CallAmount { get; set; }

        /// <summary>
        /// Holds raise amount of player.
        /// </summary>
        int RaiseAmount { get; set; }

        /// <summary>
        /// Show previous raise of the player.
        /// </summary>
        int PrevRaise { get; set; }

        /// <summary>
        /// Holds all in amount.
        /// </summary>
        int AllInAmount { get; set; }

        /// <summary>
        /// Show is player turn is folded.
        /// </summary>
        bool FoldedTurn { get; set; }
    }
}
