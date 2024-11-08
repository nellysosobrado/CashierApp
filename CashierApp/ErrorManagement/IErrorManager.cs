using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.ErrorManagement
{
    //Implements error message loose coupled
    public interface IErrorManager
    {
        void DisplayError(string errorMessage);

    }
}
