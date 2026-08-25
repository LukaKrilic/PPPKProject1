using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Ui
{
    internal class ConsoleInput
    {
        private const string DateFormat = "dd.MM.yyyy";
        private const string DateTimeFormat = "dd.MM.yyyy HH:mm";

        public static string Required(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();
                Console.WriteLine("Unos ne smije biti prazan. Pokusajte ponovno.");
            }
        }

        public static string? Optional(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return null;
            return input.Trim();
        }

        public static DateOnly ReadDate(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                if (DateOnly.TryParseExact(Console.ReadLine()?.Trim(), DateFormat, null, DateTimeStyles.None, out var date))
                    return date;
                Console.WriteLine($"Format datuma: {DateFormat}");
            }
        }

        public static DateOnly? OptionalDate(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    return null;
                if (DateOnly.TryParseExact(input.Trim(), DateFormat, null, DateTimeStyles.None, out var date))
                    return date;
                Console.WriteLine($"Format datuma: {DateFormat}");
            }
        }

        public static DateTime ReadUtcDateTime(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParseExact(Console.ReadLine()?.Trim(), DateTimeFormat,
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                    return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                Console.WriteLine($"Format: {DateTimeFormat}");
            }
        }

        public static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine()?.Trim().Replace(',', '.');
                if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var value))
                    return value;
                Console.WriteLine("Unesite broj (npr. 2.5).");
            }
        }

        public static decimal? OptionalDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine()?.Trim().Replace(',', '.');
                if (string.IsNullOrWhiteSpace(s)) return null;
                if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var value))
                    return value;
                Console.WriteLine("Unesite broj (npr. 2.5).");
            }
        }
        public static long ReadLong(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (long.TryParse(Console.ReadLine()?.Trim(), out var value)) return value;
                Console.WriteLine("Unesite cijeli broj.");
            }
        }

        public static bool Confirm(string prompt)
        {
            Console.Write(prompt);
            return string.Equals(Console.ReadLine()?.Trim(), "d", StringComparison.OrdinalIgnoreCase);

        }
    }
}
