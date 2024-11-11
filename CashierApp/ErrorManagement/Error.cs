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

            int commandLine = Console.CursorTop; 
            int errorLine = commandLine - 1; 
            Console.SetCursorPosition(leftPadding, errorLine);

           
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(fullMessage);
            Console.ResetColor();

            
            Console.SetCursorPosition(0, commandLine);
        }

    }
}
