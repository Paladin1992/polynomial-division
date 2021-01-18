using System;

using PolynomialDivision.Service;

namespace PolynomialDivision.ConsoleUI
{
    /// <summary>
    /// A helper class to simplify the console logic flow.
    /// </summary>
    class ConsoleManager
    {
        private const char DefaultYesKeyChar = 'Y';

        /// <summary>
        /// Reads the user's input from the console and returns it as a <see cref="Polynomial"/> instance.
        /// </summary>
        /// <returns>A <see cref="Polynomial"/> instance based on the user input.</returns>
        private static Polynomial ReadPolynomialFromConsole()
        {
            string input = Console.ReadLine();
            return new Polynomial(input);
        }

        /// <summary>
        /// Prints a prompt onto the console, reads the user's input from the console that is supposed to be the divisor polynomial,
        /// and returns it as a <see cref="Polynomial"/> instance.
        /// </summary>
        /// <returns>A <see cref="Polynomial"/> instance based on the user input.</returns>
        public static Polynomial GetDivisorFromUser()
        {
            MessageHandler.PrintDivisorPrompt();
            return ReadPolynomialFromConsole();
        }

        /// <summary>
        /// Prints a prompt onto the console, reads the user's input from the console that is supposed to be the dividend polynomial,
        /// and returns it as a <see cref="Polynomial"/> instance.
        /// </summary>
        /// <returns>A <see cref="Polynomial"/> instance based on the user input.</returns>
        public static Polynomial GetDividendFromUser()
        {
            MessageHandler.PrintDividendPrompt();
            return ReadPolynomialFromConsole();
        }

        /// <summary>
        /// Prints a prompt onto the console and waits for a key to be pressed by the user.
        /// Returns true, if the user pressed the specified <paramref name="yesKeyChar"/> (Y is used by default); otherwise false.
        /// </summary>
        /// <param name="yesKeyChar">The key character that marks the "yes" answer. The Y key is used by default.</param>
        /// <returns>true, if the user wants to exit; otherwise false.</returns>
        public static bool GetUserExitResponse(char yesKeyChar = DefaultYesKeyChar)
        {
            MessageHandler.PrintNewDivisionPrompt();
            ConsoleKeyInfo key = Console.ReadKey();

            if (Char.ToUpper(key.KeyChar) == Char.ToUpper(yesKeyChar))
            {
                Console.WriteLine();
                return false;
            }

            return true;
        }
    }
}
