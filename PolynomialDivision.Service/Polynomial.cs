using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using PolynomialDivision.Resources;
using PolynomialDivision.Service.Exceptions;
using PolynomialDivision.Service.Interfaces;

namespace PolynomialDivision.Service
{
    public class Polynomial : IPolynomial
    {
        private const int MinimumOperandCountDefault = 2;
        private const int MaximumOperandCountDefault = 10;
        private const int MinimumValueDefault = -10;
        private const int MaximumValueDefault = 10;
        private const string SingleTermRegexPattern = @"^[+-]?\d*(?:[,.]\d+)?(?:x(?:\^\d+)?)?$";
        private const string PolynomialRegexPattern = @"^(?:[+-]?\d*(?:[,.]\d+)?(?:x(?:\^\d+)?)?)+$";
        private const string PowerRegexPattern = @"\^(?<power>\d)";
        private const string SingleTermWithCoefficientRegexPattern = @"^(?<coeff>[+-]?\d*(?:[,.]\d+)?)(?:x(?:\^\d+)?)?$";
        private static readonly Random random = new Random();

        /// <summary>
        /// Gets or sets the coefficient at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the coefficient to get or set.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>The coefficient at a specified index.</returns>
        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= Coefficients.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                return Coefficients[index];
            }
            set
            {
                if (index < 0 || index >= Coefficients.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                Coefficients[index] = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Polynomial"/> class that has only one coefficient set to zero.
        /// </summary>
        public Polynomial() : this(0)
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="Polynomial"/> class with the given parameters set as coefficients.
        /// The value at a specific parameter index indicates the value of the coefficient where the term's power is equal to the index.
        /// Examples:
        /// <code>new Polynomial(1) => 1</code>
        /// <code>new Polynomial(0, 2) => 2x</code>
        /// <code>new Polynomial(1, 2) => 2x + 1</code>
        /// <code>new Polynomial(1, 2, 3) => 3x^2 + 2x + 1</code>
        /// <code>new Polynomial(-3, 0, 1, 5) => 5x^3 + x^2 - 3</code>
        /// </summary>
        /// <param name="coeffs">The coefficients' values.</param>
        public Polynomial(params double[] coeffs)
        {
            Coefficients = coeffs;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Polynomial"/> class with the given collection of numbers set as coefficients.
        /// The value at a specific parameter index indicates the value of the coefficient where the term's power is equal to the index.
        /// Examples:
        /// <code>new Polynomial(1) => 1</code>
        /// <code>new Polynomial(0, 2) => 2x</code>
        /// <code>new Polynomial(1, 2) => 2x + 1</code>
        /// <code>new Polynomial(1, 2, 3) => 3x^2 + 2x + 1</code>
        /// <code>new Polynomial(-3, 0, 1, 5) => 5x^3 + x^2 - 3</code>
        /// </summary>
        /// <param name="coeffs">The coefficients' values.</param>
        public Polynomial(ICollection<double> coeffs) : this(coeffs.ToArray())
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="Polynomial"/> class with the given polynomial string and sets its coefficients.
        /// Whitespaces are ignored.
        /// Examples:
        /// <code>"1" => {1}</code>
        /// <code>"2x" => {0, 2}</code>
        /// <code>"2x + 1" => {1, 2}</code>
        /// <code>"3x^2 + 2x + 1" => {1, 2, 3}</code>
        /// <code>"5x^3 + x^2 - 3" => {-3, 0, 1, 5}</code>
        /// </summary>
        /// <param name="polynomial">A string that represents a valid polynomial. Whitespaces are ignored. For example:
        /// <code>"8x^2 - 6x + 1"</code>
        /// </param>
        public Polynomial(string polynomial)
        {
            Coefficients = GetCoefficients(polynomial);
        }

        /// <summary>
        /// The coefficients of the polynomial.
        /// The value at a specific index indicates the value of the coefficient where the term's power is equal to the index.
        /// For example: <c>{1, 2, 3} = 3x^2 + 2x + 1</c>
        /// </summary>
        public IList<double> Coefficients { get; set; } = new List<double>();

        /// <summary>
        /// Returns <see langword="true"/>, if all of the coefficients of this polynomial instance are zero;
        /// otherwise <see langword="false"/>.
        /// </summary>
        public bool HasOnlyZeroCoefficients => Coefficients.All(coeff => coeff == 0);

        /// <summary>
        /// Gets the degree (the highest power) of the polynomial.
        /// </summary>
        public int Degree
        {
            get
            {
                int i = Coefficients.Count - 1;

                while (i >= 0 && Coefficients[i] == 0)
                {
                    i--;
                }

                return i >= 0 ? i : 0;
            }
        }

        /// <summary>
        /// Executes a polynomial division on this polynomial instance with the specified divisor polynomial.
        /// </summary>
        /// <param name="divisorPolynomial">The polynomial which we want to divide the current polynomial instance with.</param>
        /// <returns>A <see cref="PolynomialDivisionResult"/> instance with the result of the division,
        /// containing the quotient and the remainder.</returns>
        public IPolynomialDivisionResult DivideByPolynomial(IPolynomial divisorPolynomial)
        {
            return PolynomialDivision(Coefficients.ToArray(), divisorPolynomial.Coefficients.ToArray());
        }

        /// <summary>
        /// Returns a string that represents the polynomial with or without spaces.
        /// </summary>
        /// <param name="insertSpaces">(optional) If <see langword="true"/>, each operator will be surrounded by a space
        /// for better readability; otherwise <see langword="false"/> (default).</param>
        /// <returns>
        /// A string that represents the polynomial with or without spaces, for example:
        /// <list type="table">
        /// <item>
        /// <description>With spaces:</description>
        /// <description><c>"6x^2 - 3x + 4"</c></description>
        /// </item>
        /// <item>
        /// <description>Without spaces:</description>
        /// <description><c>"6x^2-3x+4"</c></description>
        /// </item>
        /// </list>
        /// </returns>
        public string Print(bool insertSpaces = false)
        {
            return PrintPolynomial(this, insertSpaces);
        }

        /// <summary>
        /// Returns a string representation of the polynomial (without spaces).
        /// </summary>
        /// <returns>A string that represents the polynomial (without spaces), for example: <code>"6x^2-3x+4"</code></returns>
        public override string ToString()
        {
            return PrintPolynomial(this, insertSpaces: false);
        }


        #region [ Static methods ]

        /// <summary>
        /// Wraps the specified polynomial string into parentheses if it has multiple terms or starts with a negative sign;
        /// otherwise returns the original string untouched.
        /// </summary>
        /// <param name="polynomial">The polynomial string to parenthesize.</param>
        /// <returns>The input string wrapped in parentheses or unmodified.</returns>
        public static string Parenthesize(string polynomial)
        {
            if (!IsSingleTerm(polynomial) || polynomial.StartsWith("-"))
            {
                return $"({polynomial})";
            }

            return polynomial;
        }

        /// <summary>
        /// Creates a polynomial with randomly generated coefficient values.
        /// The count of operands and the range of values can be specified.
        /// It can also be set if full zero polynomials are allowed or not.
        /// </summary>
        /// <param name="minimumOperandCount">(optional) The minimum count of operands to generate.</param>
        /// <param name="maximumOperandCount">(optional) The maximum count of operands to generate.</param>
        /// <param name="minimumValue">(optional) The smallest value each operand can have.</param>
        /// <param name="maximumValue">(optional) The largest value each operand can have.</param>
        /// <param name="fullZeroCoefficientsAllowed">
        /// (optional) If <see langword="true"/>, the result may contain zero coefficients
        /// (it may also happen that all of them will be zero); if <see langword="false"/>, a zero value can never occur.
        /// </param>
        /// <returns>A <see cref="Polynomial"/> instance that meets the requirements of the specified parameters.</returns>
        public static IPolynomial CreateRandomPolynomial(
            int minimumOperandCount = MinimumOperandCountDefault,
            int maximumOperandCount = MaximumOperandCountDefault,
            int minimumValue = MinimumValueDefault,
            int maximumValue = MaximumValueDefault,
            bool fullZeroCoefficientsAllowed = true
        )
        {
            IList<double> coeffs = GetRandomCoefficients(
                minimumOperandCount,
                maximumOperandCount,
                minimumValue,
                maximumValue,
                fullZeroCoefficientsAllowed
            );

            return new Polynomial(coeffs);
        }

        /// <summary>
        /// Returns the values of the given array as a standard polynomial formula
        /// (terms with a zero coefficient will be omitted; 1's are not written either).
        /// </summary>
        /// <param name="polynomial">The <see cref="Polynomial"/> instance to print.</param>
        /// <param name="insertSpaces">(optional) If <see langword="true"/>, each operator will be surrounded by a space
        /// for better readability; otherwise <see langword="false"/> (default).</param>
        /// <returns>
        /// A string that represents the polynomial with or without spaces, for example:
        /// <list type="table">
        /// <item>
        /// <description>With spaces:</description>
        /// <description><c>"6x^2 - 3x + 4"</c></description>
        /// </item>
        /// <item>
        /// <description>Without spaces:</description>
        /// <description><c>"6x^2-3x+4"</c></description>
        /// </item>
        /// </list>
        /// </returns>
        public static string PrintPolynomial(IPolynomial polynomial, bool insertSpaces = false)
        {
            if (!IsValidPolynomial(polynomial))
            {
                throw new InvalidPolynomialException(polynomial.ToString());
            }

            if (polynomial.HasOnlyZeroCoefficients)
            {
                return "0";
            }

            var stringBuilder = new StringBuilder();
            for (int i = polynomial.Coefficients.Count - 1; i >= 0; i--)
            {
                if (polynomial.Coefficients[i] != 0)
                {
                    stringBuilder.AppendFormat("+{0,0:0.##}", polynomial.Coefficients[i]);
                    stringBuilder.Append(i > 1 ? "x^" + i : (i == 1 ? "x" : ""));
                }
            }

            stringBuilder
                .Replace("+-", "-")
                .Replace("+1x", "+x")
                .Replace("-1x", "-x");

            string result = stringBuilder.ToString().TrimStart('+');
            return insertSpaces ? StretchPolynomial(result) : result;
        }

        /// <summary>
        /// Stretches the given polynomial by inserting a single space character before and after each plus and minus sign.
        /// </summary>
        /// <param name="polynomial">The polynomial string to be stretched.</param>
        /// <returns>The input polynomial string with spaces around each plus and minus sign. Any other spaces will be removed.</returns>
        public static string StretchPolynomial(string polynomial)
        {
            return new StringBuilder(polynomial)
                .Replace(" ", "")
                .Replace("+", " + ")
                .Replace("-", " - ")
                .ToString()
                .Trim();
        }

        /// <summary>
        /// Returns a boolean value indicating whether the given polynomial contains only one term.
        /// Single terms are for example: <c>-6, 8x, 3x^2</c> etc.
        /// Empty string ("") and <see langword="null"/> are not considered single terms.
        /// (Note that this method does NOT group <paramref name="polynomial"/>'s terms if they have the same power.
        /// If you want to check that, use GroupTerms method first.)
        /// </summary>
        /// <param name="polynomial">The polynomial to check.</param>
        /// <returns><see langword="true"/>, if the given polynomial has only a single term; otherwise <see langword="false"/>.</returns>
        private static bool IsSingleTerm(string polynomial)
        {
            if (string.IsNullOrEmpty(polynomial))
            {
                return false;
            }

            return Regex.IsMatch(polynomial.Trim(), SingleTermRegexPattern);
        }

        /// <summary>
        /// Retrieves the coefficients from the given polynomial.
        /// The similar terms (i.e. terms with the same power) will be grouped.
        /// </summary>
        /// <param name="polynomial">A polynomial whose coefficients we would like to get.</param>
        /// <returns>A list of numbers containing the polynomial's coefficients, in reversed order.</returns>
        public static IList<double> GetCoefficients(string polynomial)
        {
            return GroupTerms(polynomial).Coefficients;
        }

        /// <summary>
        /// Executes a polynomial division on the given polynomials.
        /// If the <paramref name="divisor"/>'s degree is greater than <paramref name="dividend"/>'s degree,
        /// the input parameters are returned.
        /// <para>
        /// If either polynomial is invalid, an <see cref="InvalidPolynomialException"/> is thrown.
        /// </para>
        /// </summary>
        /// <exception cref="InvalidPolynomialException"></exception>
        /// <param name="dividend">The polynomial to be divided.</param>
        /// <param name="divisor">The polynomial we want the <paramref name="dividend"/> to be divided with.</param>
        /// <returns>
        /// The result of the polynomial division as a <see cref="PolynomialDivisionResult"/> instance
        /// that contains both the quotient and the remainder.
        /// </returns>
        public static IPolynomialDivisionResult PolynomialDivision(IPolynomial dividend, IPolynomial divisor)
        {
            return PolynomialDivision(dividend.Coefficients?.ToArray(), divisor.Coefficients?.ToArray());
        }

        /// <summary>
        /// Executes a polynomial division on the given polynomials.
        /// If the <paramref name="divisor"/>'s degree is greater than <paramref name="dividend"/>'s degree,
        /// the input parameters are returned.
        /// <para>
        /// If either polynomial is invalid, an <see cref="InvalidPolynomialException"/> is thrown.
        /// </para>
        /// </summary>
        /// <exception cref="InvalidPolynomialException"></exception>
        /// <param name="dividend">The polynomial as a string to be divided.</param>
        /// <param name="divisor">The polynomial as a string we want the <paramref name="dividend"/> to be divided with.</param>
        /// <returns>
        /// The result of the polynomial division as a <see cref="PolynomialDivisionResult"/> instance
        /// that contains both the quotient and the remainder.
        /// </returns>
        public static IPolynomialDivisionResult PolynomialDivision(string dividend, string divisor)
        {
            string tempDividend = PrintPolynomial(GroupTerms(dividend));
            string tempDivisor = PrintPolynomial(GroupTerms(divisor));

            if (!IsValidPolynomial(tempDividend))
            {
                throw new InvalidPolynomialException(ErrorMessages.InvalidDividend);
            }

            if (!IsValidPolynomial(tempDivisor))
            {
                throw new InvalidPolynomialException(ErrorMessages.InvalidDivisor);
            }

            int degreeOfDividend = GetDegree(tempDividend);
            int degreeOfDivisor = GetDegree(tempDivisor);

            if (degreeOfDividend < degreeOfDivisor)
            {
                return null;
            }

            double[] dividendCoeffs = GetCoefficients(tempDividend).ToArray();
            double[] divisorCoeffs = GetCoefficients(tempDivisor).ToArray();

            return PolynomialDivision(dividendCoeffs, divisorCoeffs);
        }

        /// <summary>
        /// Executes a polynomial division on the given polynomials.
        /// If <paramref name="dividend"/>'s length is smaller than <paramref name="divisor"/>'s length,
        /// the input parameters are returned.
        /// <para>
        /// If any of the parameters is <see langword="null"/>, an <see cref="ArgumentNullException"/> is thrown.
        /// </para>
        /// <para>
        /// If divisor contains only zeros, a <see cref="DivideByZeroException"/> is thrown.
        /// </para>
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DivideByZeroException"></exception>
        /// <param name="dividend">The polynomial to be divided.</param>
        /// <param name="divisor">The polynomial to divide with.</param>
        /// <returns>
        /// The result of the polynomial division as a <see cref="PolynomialDivisionResult"/> instance
        /// that contains both the quotient and the remainder.
        /// </returns>
        public static IPolynomialDivisionResult PolynomialDivision(double[] dividend, double[] divisor)
        {
            if (dividend == null)
            {
                throw new ArgumentNullException(nameof(dividend));
            }

            if (divisor == null)
            {
                throw new ArgumentNullException(nameof(divisor));
            }

            if (divisor.All(coeff => coeff == 0))
            {
                throw new DivideByZeroException();
            }

            if (dividend.All(coeff => coeff == 0))
            {
                return new PolynomialDivisionResult(new Polynomial(0), new Polynomial(0));
            }

            if (dividend.Length < divisor.Length)
            {
                return new PolynomialDivisionResult(new Polynomial(dividend), new Polynomial(divisor));
            }

            double[] tempDividend = (double[])dividend.Clone();
            int dividendPointer = dividend.Length - 1;
            int divisorPointer = divisor.Length - 1;
            IList<double> resultList = new List<double>();

            while (dividendPointer >= divisor.Length - 1)
            {
                double newTerm = tempDividend[dividendPointer] / divisor[divisorPointer];
                if (double.IsInfinity(newTerm) || double.IsNaN(newTerm))
                {
                    string message = string.Format(ErrorMessages.InfinityOrNaN,
                        PrintCoefficients(dividend),
                        PrintCoefficients(divisor),
                        dividendPointer,
                        divisorPointer,
                        tempDividend[dividendPointer],
                        divisor[divisorPointer],
                        newTerm
                    );

                    throw new ArithmeticException(message);
                }

                resultList.Insert(0, newTerm);

                double[] subtrahend = new double[dividendPointer + 1];
                for (int i = subtrahend.Length - 1; i >= 0 && divisorPointer >= 0; i--)
                {
                    subtrahend[i] = divisor[divisorPointer] * newTerm;
                    divisorPointer--;
                }

                tempDividend = SubtractPolynomials(tempDividend, subtrahend);
                dividendPointer--;
                divisorPointer = divisor.Length - 1;
            }

            double[] tempRemainder = new double[Math.Max(dividendPointer, 0) + 1];
            for (int i = 0; i < tempRemainder.Length; i++)
            {
                tempRemainder[i] = tempDividend[i];
            }

            return new PolynomialDivisionResult(resultList, tempRemainder);
        }

        /// <summary>
        /// Specifies whether the given polynomial is valid or not.
        /// Empty string ("") and <see langword="null"/> are not considered valid.
        /// Whitespace characters are ignored.
        /// </summary>
        /// <param name="polynomial">The polynomial as a string (whitespace characters are ignored),
        /// for example: <c>"-8x^3 + 4x - 6"</c></param>
        /// <returns>
        /// <see langword="true"/>, if <paramref name="polynomial"/> (without parentheses and whitespaces)
        /// matches the following regex pattern:
        /// <code>^(?:[+-]?\d*(?:[,.]\d+)?(?:x(?:\^\d+)?)?)+$</code>
        /// ; otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsValidPolynomial(string polynomial)
        {
            if (string.IsNullOrEmpty(polynomial))
            {
                return false;
            }

            string input = polynomial.Trim('(', ')').Replace(" ", "");
            return Regex.IsMatch(input, PolynomialRegexPattern);
        }

        /// <summary>
        /// Specifies whether the given polynomial instance is valid or not.
        /// </summary>
        /// <param name="polynomial">The <see cref="Polynomial"/> instance to check.</param>
        /// <returns>
        /// <see langword="true"/>, if neither the <paramref name="polynomial"/> nor its Coefficients property are <see langword="null"/>;
        /// otherwise <see langword="false"/>.
        /// </returns>
        public static bool IsValidPolynomial(IPolynomial polynomial)
        {
            return polynomial != null && polynomial.Coefficients != null;
        }

        /// <summary>
        /// Returns the highest power in the given polynomial.
        /// <para>
        /// If the polynomial is not valid, an <see cref="InvalidPolynomialException"/> is thrown.
        /// </para>
        /// </summary>
        /// <exception cref="InvalidPolynomialException"></exception>
        /// <param name="polynomial">The polynomial as a string (whitespace characters are ignored),
        /// for example: <c>"-8x^3 + 4x - 6"</c></param>
        /// <returns></returns>
        public static int GetDegree(string polynomial)
        {
            if (!IsValidPolynomial(polynomial))
            {
                throw new InvalidPolynomialException(polynomial);
            }

            if (polynomial.Contains('^')) // degree is probably at least 2 (may have x^1 or x^0 also, though)
            {
                int highestPower = 0;
                MatchCollection matches = Regex.Matches(polynomial.Replace(" ", ""), PowerRegexPattern);

                foreach (Match match in matches)
                {
                    int currentPower = Convert.ToInt32(match.Groups["power"].ToString());
                    if (currentPower > highestPower)
                    {
                        highestPower = currentPower;
                    }
                }

                return highestPower;
            }
            else // degree is at most 1
            {
                return polynomial.Contains('x') ? 1 : 0;
            }
        }

        /// <summary>
        /// Simplifies the given polynomial by grouping the terms with the same power and returns a new polynomial as result.
        /// For example:
        /// <list type="table">
        /// <item>Before: <code>3x^2 + 2x - 4x^2 + 7x^3 - 6 + 8x</code></item>
        /// <item>After: <code>7x^3 - x^2 + 10x - 6</code></item>
        /// </list>
        /// so the returned array will look like this: {-6, 10, -1, 7}
        /// <para>
        /// If the polynomial is not valid, an <see cref="InvalidPolynomialException"/> is thrown.
        /// </para>
        /// </summary>
        /// <exception cref="InvalidPolynomialException"></exception>
        /// <param name="polynomial">The polynomial as a string (whitespaces are ignored), for example: <c>"-8x^3 + 4x - 6"</c></param>
        /// <returns>A new <see cref="Polynomial"/> instance with the simplified coefficients.</returns>
        public static IPolynomial GroupTerms(string polynomial)
        {
            string preprocessedPolynomial = PreprocessStringInput(polynomial);

            if (!IsValidPolynomial(preprocessedPolynomial))
            {
                throw new InvalidPolynomialException(polynomial);
            }

            return SumCoefficientsOfSimilarTerms(preprocessedPolynomial);
        }

        #endregion

        #region [ Helper methods ]

        /// <summary>
        /// Preprocesses the given polynomial string by applying various string operations on it,
        /// so that later it can be split by '+' characters.
        /// </summary>
        /// <param name="polynomial">The input string to be preprocessed, for example: <c>"-8x^3 + 4x - 6"</c></param>
        /// <returns>
        /// A new string that is modified as follows:
        /// <list type="number">
        /// <item>Any '(' or ')' characters are removed from both ends;</item>
        /// <item>Any whitespaces are removed;</item>
        /// <item>The '-' characters are replaced with "+-";</item>
        /// <item>The "+x" substrings are replaced with "+1x";</item>
        /// <item>The "-x" substrings are replaced with "-1x".</item>
        /// </list>
        /// </returns>
        private static string PreprocessStringInput(string polynomial)
        {
            if (string.IsNullOrEmpty(polynomial))
            {
                return String.Empty;
            }

            // Prepocess steps - example:
            // "(3x^2 + 2x - x^2 + 7x^3 - 6 + 8x)" -->
            // "3x^2 + 2x - x^2 + 7x^3 - 6 + 8x" -->
            // "3x^2+2x-x^2+7x^3-6+8x" -->
            // "3x^2+2x+-1x^2+7x^3+-6+8x"
            var stringBuilder = new StringBuilder($"+{ polynomial.Trim('(', ')') }")
                .Replace(" ", "")
                .Replace("\t", "")
                .Replace("-", "+-")
                .Replace("+x", "+1x")
                .Replace("-x", "-1x");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Simplifies the given polynomial by grouping the terms with the same power and returns a new polynomial as result.
        /// The input is assumed to be valid.
        /// For example:
        /// <list type="table">
        /// <item>Before: <code>3x^2 + 2x - 4x^2 + 7x^3 - 6 + 8x</code></item>
        /// <item>After: <code>7x^3 - x^2 + 10x - 6</code></item>
        /// </list>
        /// so the returned array will look like this: {-6, 10, -1, 7}
        /// </summary>
        /// <param name="polynomial">The polynomial as a string (whitespaces are ignored), for example: <c>"-8x^3 + 4x - 6"</c></param>
        /// <returns>A new <see cref="Polynomial"/> instance with the simplified coefficients.</returns>
        private static IPolynomial SumCoefficientsOfSimilarTerms(string polynomial)
        {
            int degree = GetDegree(polynomial);
            double[] summedCoeffs = new double[degree + 1];
            string[] terms = polynomial.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string term in terms)
            {
                int index = GetDegree(term);
                Match match = Regex.Match(term, SingleTermWithCoefficientRegexPattern);
                if (match.Success)
                {
                    summedCoeffs[index] += Convert.ToDouble(match.Groups["coeff"].Value);
                }
                else
                {
                    throw new InvalidPolynomialException(polynomial);
                }
            }

            return new Polynomial(summedCoeffs);
        }

        // [a0, a1, a2, ..., aN]
        // [b0, b1, b2, ..., bM]

        // minuend.Length = subtrahend.Length
        //                0. 1. 2. 3.
        //    minuend = [ 9, 8, 7, 6]
        // subtrahend = [ 2, 3, 4, 5]
        // --------------------------
        // difference = [ 7, 5, 3, 1]

        // minuend.Length > subtrahend.Length
        //                0. 1. 2. 3.
        //    minuend = [ 9, 8, 7, 6]
        // subtrahend = [ 2, 3, 4][0]
        // --------------------------
        // difference = [ 7, 5, 3, 6]

        // minuend.Length < subtrahend.Length
        //                0. 1. 2. 3.
        //    minuend = [ 9, 8, 7][0]
        // subtrahend = [ 2, 3, 4, 5]
        // --------------------------
        // difference = [ 7, 5, 3,-5]
        /// <summary>
        /// Subtracts the coefficients of the subtrahend polynomial from the coefficients of the minuend polynomial.
        /// If the degree of the polynomials are not the same, the shorter one will be expanded by zeros.
        /// </summary>
        /// <param name="minuend">An array of coefficients of the minuend polynomial.</param>
        /// <param name="subtrahend">An array of coefficients of the subtrahend polynomial.</param>
        /// <returns>The difference of the coefficients one by one.</returns>
        private static double[] SubtractPolynomials(double[] minuend, double[] subtrahend)
        {
            double[] tempMinuend = minuend;
            double[] tempSubtrahend = subtrahend;

            int minuendLength = minuend.Length;
            int subtrahendLength = subtrahend.Length;

            if (minuendLength > subtrahendLength)
            {
                tempSubtrahend = new double[minuendLength];
                subtrahend.CopyTo(tempSubtrahend, 0);
            }
            else if (minuendLength < subtrahendLength)
            {
                tempMinuend = new double[subtrahendLength];
                minuend.CopyTo(tempMinuend, 0);
            }

            double[] difference = new double[tempMinuend.Length];
            for (int i = 0; i < tempMinuend.Length; i++)
            {
                difference[i] = tempMinuend[i] - tempSubtrahend[i];
            }

            return difference;
        }

        /// <summary>
        /// Generates a polynomial with random count of terms, specified by the parameters.
        /// The count and value range of the coefficients can be set manually.
        /// <para>
        /// If either <paramref name="minimumOperandCount"/> or <paramref name="maximumOperandCount"/> is negative,
        /// an <see cref="ArgumentOutOfRangeException"/> is thrown.
        /// </para>
        /// <para>
        /// If both <paramref name="minimumValue"/> and <paramref name="maximumValue"/> are set to zero,
        /// but <paramref name="fullZeroCoefficientsAllowed"/> is set to <see langword="false"/>,
        /// an <see cref="ArgumentOutOfRangeException"/> is thrown.
        /// </para>
        /// </summary>
        /// <param name="minimumOperandCount">(optional) The minimum number of operands to generate.</param>
        /// <param name="maximumOperandCount">(optional) The maximum number of operands to generate.</param>
        /// <param name="minimumValue">(optional) The minimum value of each coefficient.</param>
        /// <param name="maximumValue">(optional) The maximum value of each coefficient.</param>
        /// <param name="fullZeroCoefficientsAllowed">(optional) If <see langword="true"/>,
        /// the result may be a list that contain only zero coefficients;
        /// if <see langword="false"/>, at least one coefficient will be set to a non-zero value.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>A list of numbers with randomly generated values that all meet the requirements specified by the parameters.</returns>
        private static IList<double> GetRandomCoefficients(
            int minimumOperandCount = MinimumOperandCountDefault,
            int maximumOperandCount = MaximumOperandCountDefault,
            int minimumValue = MinimumValueDefault,
            int maximumValue = MaximumValueDefault,
            bool fullZeroCoefficientsAllowed = true
        )
        {
            IfNegativeThenThrowException(minimumOperandCount);
            IfNegativeThenThrowException(maximumOperandCount);

            if (minimumValue == 0 && maximumValue == 0)
            {
                if (!fullZeroCoefficientsAllowed)
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format(
                            ErrorMessages.FullZeroAllowedWithInvalidRange,
                            nameof(minimumValue), nameof(maximumValue), nameof(fullZeroCoefficientsAllowed)
                        )
                    );
                }

                return new double[] { 0 };
            }

            int minCount = Math.Min(minimumOperandCount, maximumOperandCount);
            int maxCount = Math.Max(minimumOperandCount, maximumOperandCount);
            int minValue = Math.Min(minimumValue, maximumValue);
            int maxValue = Math.Max(minimumValue, maximumValue);
            int numberOfOperands = random.Next(minCount, maxCount + 1);
            double[] coeffs = new double[numberOfOperands];

            // *: intervals that contain zero
            // [+]..[+]  
            // [-]..[-]  
            // [-]..[+] *
            // [-].. 0  *
            //  0 ..[+] *
            //  0 .. 0  * (already checked above)
            bool hasGeneratedNonZeroValue = false;
            for (int i = 0; i < numberOfOperands; i++)
            {
                coeffs[i] = random.Next(minValue, maxValue + 1);

                if (coeffs[i] != 0)
                {
                    hasGeneratedNonZeroValue = true;
                }
            }

            if (!fullZeroCoefficientsAllowed && !hasGeneratedNonZeroValue)
            {
                int randomIndex = random.Next(0, coeffs.Length);
                coeffs[randomIndex] = GenerateRandomValueExcludingZero(minValue, maxValue);
            }

            return coeffs;
        }

        /// <summary>
        /// Generates a random number within the range of <paramref name="minValue"/> and <paramref name="maxValue"/>,
        /// but even when the range would contain zero, the result will never be zero.
        /// The method presumes that <paramref name="minValue"/> and <paramref name="maxValue"/> cannot be zero at the same time.
        /// </summary>
        /// <param name="minValue">The lower bound of the range to pick a random non-zero value from.</param>
        /// <param name="maxValue">The upper bound of the range to pick a random non-zero value from.</param>
        /// <returns>A non-zero value within the specified range.</returns>
        private static int GenerateRandomValueExcludingZero(int minValue, int maxValue)
        {
            int randomNegativeValue = random.Next(minValue, -1);
            int randomPositiveValue = random.Next(1, maxValue + 1);

            if (minValue == 0) // 0..[+]
            {
                return randomPositiveValue;
            }
            else if (maxValue == 0) // [-]..0
            {
                return randomNegativeValue;
            }
            else // [-]..[+]
            {
                return random.Next(0, 2) == 0 ? randomNegativeValue : randomPositiveValue;
            }
        }

        /// <summary>
        /// If the specified value is negative, an <see cref="ArgumentOutOfRangeException"/> is thrown; otherwise nothing happens.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <param name="value">Value to check.</param>
        private static void IfNegativeThenThrowException(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), ErrorMessages.ValueCannotBeNegative);
            }
        }

        /// <summary>
        /// Returns the values of the given coefficients as a string.
        /// </summary>
        /// <param name="coeffs">A collection of coefficients.</param>
        /// <param name="reverse">(optional) If <see langword="true"/>, the coefficients are returned in descending order
        /// (i.e. from the highest power to the lowest);
        /// if <see langword="false"/>, the coefficients are returned in ascending order (default).</param>
        /// <returns>
        /// A string containing every item of <paramref name="coeffs"/>, in the specified order, like this:
        /// <code>"{1, 2, 3, 4, 5}"</code>
        /// </returns>
        private static string PrintCoefficients(ICollection<double> coeffs, bool reverse = false)
        {
            return $"{{{(reverse ? string.Join(", ", coeffs.Reverse()) : string.Join(", ", coeffs))}}}";
        }

        #endregion
    }
}
