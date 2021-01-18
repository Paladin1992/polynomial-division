using System;

using PolynomialDivision.Service;

namespace PolynomialDivision.ConsoleUI
{
    /// <summary>
    /// A helper class to handle the messages displayed on the console.
    /// </summary>
    class MessageHandler
    {        
        /// <summary>
        /// Prints the specified application title onto the console with a line beneath it.
        /// </summary>
        /// <param name="appTitle">The application title to print onto the screen.</param>
        public static void PrintTitle(string appTitle)
        {
            Console.WriteLine($"{appTitle}:");
            Console.WriteLine(DrawLineOfLength(appTitle.Length + 1));
        }

        /// <summary>
        /// Draws a line by repeating the optionally specified <paramref name="lineCharacter"/> <paramref name="length"/> times.
        /// </summary>
        /// <param name="length">The desired length of the line.</param>
        /// <param name="lineCharacter">(optional) The desired character to repeat. '-' is used by default.</param>
        /// <returns>A string that contains <paramref name="lineCharacter"/> repeated <paramref name="length"/> times.</returns>
        public static string DrawLineOfLength(int length, char lineCharacter = '-')
        {
            return new string(lineCharacter, length);
        }

        /// <summary>
        /// Prints the dividend prompt text onto the console.
        /// </summary>
        public static void PrintDividendPrompt()
        {
            Console.Write("Dividend: ");
        }

        /// <summary>
        /// Prints the divisor prompt text onto the console.
        /// </summary>
        public static void PrintDivisorPrompt()
        {
            Console.Write("Divisor : ");
        }

        /// <summary>
        /// Prints the result of the polynomial division onto the console (i.e. dividend, divisor, quotient, and remainder).
        /// </summary>
        /// <param name="dividend">The dividend <see cref="Polynomial"/> instance that was used for the polynomial division.</param>
        /// <param name="divisor">The divisor <see cref="Polynomial"/> instance that was used for the polynomial division.</param>
        /// <param name="result">The result of the polynomial division.</param>
        public static void PrintResults(Polynomial dividend, Polynomial divisor, PolynomialDivisionResult result)
        {
            string dividendOutput = dividend.ToString();
            string divisorOutput = divisor.ToString();
            string quotient = Polynomial.PrintPolynomial(result.Quotient);
            string remainder = Polynomial.PrintPolynomial(result.Remainder);

            Console.WriteLine();
            Console.WriteLine($"Dividend : {Polynomial.StretchPolynomial(dividendOutput)}");
            Console.WriteLine($"Divisor  : {Polynomial.StretchPolynomial(divisorOutput)}");
            Console.WriteLine($"Quotient : {Polynomial.StretchPolynomial(quotient)}");
            Console.WriteLine($"Remainder: {Polynomial.StretchPolynomial(remainder)}");

            Console.WriteLine();
            Console.WriteLine("{0} = {1}*{2} + {3}",
                dividendOutput,
                Polynomial.Parenthesize(divisorOutput),
                Polynomial.Parenthesize(quotient),
                Polynomial.Parenthesize(remainder)
            );
        }

        /// <summary>
        /// Prints a prompt onto the console whether the user wants a new polynomial division or not.
        /// </summary>
        public static void PrintNewDivisionPrompt()
        {
            Console.Write("\nNew division? (Y/N) ");
        }

        /// <summary>
        /// Prints the error message of the specified <see cref="Exception"/> instance.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> instance used to retrieve error-specific info.</param>
        public static void PrintErrorMessage(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: " + ex.Message);
            Console.ForegroundColor = originalForegroundColor;
        }
    }
}
