using System;

using PolynomialDivision.ConsoleUI;
using PolynomialDivision.Service;

namespace PolynomialDivision
{
    class Program
    {
        static readonly string AppTitle = "Polynomial Division";

        // 8x^3 + 18x^2 - 15x - 16
        // 4x^2 + 3x - 12
        static void Main(string[] args)
        {
            MessageHandler.PrintTitle(AppTitle);
            bool exit;

            do
            {
                try
                {
                    Polynomial dividend = ConsoleManager.GetDividendFromUser();
                    Polynomial divisor = ConsoleManager.GetDivisorFromUser();
                    PolynomialDivisionResult result = Polynomial.PolynomialDivision(dividend, divisor);
                    MessageHandler.PrintResults(dividend, divisor, result);
                }
                catch (Exception ex)
                {
                    MessageHandler.PrintErrorMessage(ex);
                }
                finally
                {
                    exit = ConsoleManager.GetUserExitResponse();
                }
            } while (!exit);
        }
    }
}
