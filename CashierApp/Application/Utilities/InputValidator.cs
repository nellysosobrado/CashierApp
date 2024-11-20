using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Application.Utilities
{
    public class InputValidator
    {
        public DateTime GetValidatedDate(string prompt, Func<DateTime, bool> validator, string errorMessage)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                string input = Console.ReadLine() ?? string.Empty;
                if (DateTime.TryParse(input, out DateTime date) && validator(date)) return date;

                Console.WriteLine(errorMessage);
            }
        }
        public string GetValidatedInput(string prompt, Func<string, bool> validator, string errorMessage)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                string input = Console.ReadLine() ?? string.Empty;
                if (validator(input)) return input;

                Console.WriteLine(errorMessage);
            }
        }

        public int GetUniqueId(string prompt, Func<int, bool> validator, string errorMessage)
        {
            while (true)
            {
                string input = GetValidatedInput(prompt, input => int.TryParse(input, out _), "Invalid input. Must be a number.");
                int id = int.Parse(input);

                if (validator(id)) return id;

                Console.WriteLine(errorMessage);
            }
        }

        public decimal GetValidatedDecimal(string prompt, Func<decimal, bool> validator, string errorMessage)
        {
            while (true)
            {
                string input = GetValidatedInput(prompt, input => decimal.TryParse(input, out _), "Invalid input. Must be a number.");
                decimal value = decimal.Parse(input);

                if (validator(value)) return value;

                Console.WriteLine(errorMessage);
            }
        }
    }
}
