namespace Poker.Interfaces
{
    using System.Drawing;
    using System.Windows.Forms;
    using Models;
    
    /// <summary>
    /// Interface for player in the game.
    /// </summary>
   public interface IPlayer
    {
        /// <summary>
        /// Gets and sets player location on screen. 
        /// </summary>
       Point Location { get; set; }

        /// <summary>
        /// Gets and sets the picture box of the first card.
        /// </summary>
        PictureBox Card1PictureBox { get; set; }

        /// <summary>
        /// Gets and sets the picture box of the second card.
        /// </summary>
        PictureBox Card2PictureBox { get; set; }

        /// <summary>
        /// Gets and sets the name of the player.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets and sets the hand of the player.
        /// </summary>
        IHand Hand { get; set; }

        /// <summary>
        /// Gets and sets the panel of the player.
        /// </summary>
        Panel Panel { get; set; }

        /// <summary>
        /// Gets and sets the textbox for the player's chips.
        /// </summary>
        TextBox ChipsTextBox { get; set; }

        /// <summary>
        /// Gets and sets player's chips set .
        /// </summary>
        ChipsSet ChipsSet { get; set; }

        /// <summary>
        /// Gets and set player's status in label.
        /// </summary>
        Label StatusLabel { get; set; }

        /// <summary>
        /// Gets and sets player's result.
        /// </summary>
        IResult Result { get; set; }

        /// <summary>
        /// Shows if the player is folded.
        /// </summary>
        bool IsFolded { get; set; }

        /// <summary>
        /// Gets and sets if it is player's turn.
        /// </summary>
        bool Turn { get; set; }

        /// <summary>
        ///  Holds player's called amount.
        /// </summary>
        int CallAmount { get; set; }

        /// <summary>
        /// Holds player's raised amount.
        /// </summary>
        int RaiseAmount { get; set; }

        /// <summary>
        /// Shows player's previous raised amount.
        /// </summary>
        int PrevRaise { get; set; }

        /// <summary>
        /// Holds all in amount.
        /// </summary>
        int AllInAmount { get; set; }

        /// <summary>
        /// Shows if player's turn is folded.
        /// </summary>
        bool FoldedTurn { get; set; }
    }
}
