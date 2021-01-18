using System;

namespace PolynomialDivision.Service.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a polynomial is invalid.
    /// A polynomial is considered invalid, if it does not match the following regular expression:
    /// <code>^(?:[+-]?\d*(?:[,.]\d+)?(?:x(?:\^\d+)?)?)+$</code>
    /// </summary>
    public class InvalidPolynomialException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPolynomialException"/> class.
        /// </summary>
        public InvalidPolynomialException() : base("Invalid polynomial.")
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPolynomialException"/> class with a specified error message.
        /// </summary>
        /// <param name="polynomial">The polynomial that causes the error.</param>
        public InvalidPolynomialException(string polynomial)
            : base("Invalid polynomial: " + polynomial, new InvalidPolynomialException())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPolynomialException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception,
        /// or a null reference if no inner exception is specified.</param>
        public InvalidPolynomialException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}