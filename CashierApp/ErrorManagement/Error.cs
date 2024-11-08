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
            Console.WriteLine($"ERROR: {errorMessage}");
        }
    }
}
