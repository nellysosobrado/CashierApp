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

         
            int windowHeight = Console.WindowHeight;
            int topPadding = windowHeight / 2;
            Console.SetCursorPosition(leftPadding, topPadding);

            Console.ForegroundColor = ConsoleColor.Red;
  

            Console.WriteLine(fullMessage);
            Console.ResetColor();


        }


    }
}
