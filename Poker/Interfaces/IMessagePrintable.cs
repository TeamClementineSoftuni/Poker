namespace Poker.Interfaces
{
    /// <summary>
    /// Interface for print messages.
    /// </summary>
    public interface IMessagePrintable
    {
        /// <summary>
        /// Print message.
        /// </summary>
        /// <param name="message">Message to print.</param>
        void PrintMessage(string message);
    }
}
