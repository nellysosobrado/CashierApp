using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.ErrorManagement
{
    public class Error : IErrorManager
    {
        public void DisplayError(string errorMessage)
        {
            string fullMessage = $"ERROR: {errorMessage}";

            int windowWidth = Console.WindowWidth;
            int leftPadding = (windowWidth - fullMessage.Length) / 2;
            if (leftPadding < 0) leftPadding = 0;

            // Flytta kursorn till raden precis ovanför där "Command:" visas
            int commandLine = Console.CursorTop; // Spara den nuvarande positionen (där "Command:" är)
            int errorLine = commandLine - 1; // Visa felmeddelandet precis ovanför
            Console.SetCursorPosition(leftPadding, errorLine);

            // Skriv ut felmeddelandet
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(fullMessage);
            Console.ResetColor();

            // Flytta tillbaka kursorn till "Command:"-raden för användarens input
            Console.SetCursorPosition(0, commandLine);
        }

    }
}
