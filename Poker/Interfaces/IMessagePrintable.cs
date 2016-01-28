namespace Poker.Interfaces
{
    using Microsoft.SqlServer.Server;

    /// <summary>
    /// Interface for print messages.
    /// </summary>
    public interface IMessagePrintable
    {
        /// <summary>
        /// This method will print message.
        /// </summary>
        /// <param name="message">Message to print.</param>
        void PrintMessage(string message);

        /// <summary>
        /// This method will print message with using string format with given paramethers
        /// </summary>
        /// <param name="format">Gets message in string format</param>
        /// <param name="args">Paramethers of the formatted message</param>
        void PrintMessage(string format, params object[] args);
    }
}
