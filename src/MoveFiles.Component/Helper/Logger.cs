using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace MoveFiles.Component.Helper
{
    public class Logger
    {
        public static async ValueTask MessageLog(string message)
        {
            Console.Write($"{DateTime.UtcNow} - INFO - ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{message}");
            Console.ResetColor();

            return;
        }

        public static async ValueTask ErrorLog(string message)
        {
            Console.Write($"{DateTime.UtcNow} - ERROR - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{message}");
            Console.ResetColor();

            return;
        }

        public static async ValueTask GeneralLog(string message)
        {
            Console.WriteLine(message);

            return;
        }
    }
}
