﻿using Spectre.Console;
using System.Globalization;

namespace cacheMe512.CodeTracker
{
    internal class Validation
    {
        public static DateTime GetDateTimeInput(string promptMessage)
        {
            DateTime dateTime;
            string input;

            do
            {
                input = AnsiConsole.Ask<string>(promptMessage);
                if (!DateTime.TryParseExact(input, "dd-MM-yy HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out dateTime))
                {
                    AnsiConsole.MarkupLine("[red]Invalid date and time format! Please use the format (dd-MM-yy HH:mm).[/]");
                }
            } while (!DateTime.TryParseExact(input, "dd-MM-yy HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out dateTime));

            return dateTime;
        }

        public static int GetNumberInput(string message)
        {
            int number;
            do
            {
                var input = AnsiConsole.Ask<string>(message);
                if (!int.TryParse(input, out number) || number < 0)
                {
                    AnsiConsole.MarkupLine("[red]Invalid number. Please enter a positive number.[/]");
                }
            } while (number < 0);

            return number;
        }

        public static string GetStringInput(string message)
        {
            return AnsiConsole.Ask<string>(message);
        }
    }
}
