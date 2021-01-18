using System.Collections.Generic;

using PolynomialDivision.Service.Interfaces;

namespace PolynomialDivision.Service
{
    /// <summary>
    /// A container for storing both the quotient and the remainder polynomial after an executed polynomial division.
    /// </summary>
    public class PolynomialDivisionResult : IPolynomialDivisionResult
    {
        /// <summary>
        /// The quotient polynomial without the remainder.
        /// </summary>
        public IPolynomial Quotient { get; set; }

        /// <summary>
        /// The remainder polynomial without the quotient.
        /// </summary>
        public IPolynomial Remainder { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolynomialDivisionResult"/> class. All properties will be set to null.
        /// </summary>
        public PolynomialDivisionResult()
        {
            Quotient = null;
            Remainder = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolynomialDivisionResult"/> class with the specified quotient and remainder.
        /// </summary>
        /// <param name="quotient">The result of the polynomial division without the remainder.</param>
        /// <param name="remainder">The remainder of the polynomial division without the quotient.</param>
        public PolynomialDivisionResult(IPolynomial quotient, IPolynomial remainder)
        {
            Quotient = quotient;
            Remainder = remainder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolynomialDivisionResult"/> class with the specified quotient and remainder.
        /// </summary>
        /// <param name="quotient">The result of the polynomial division as a collection of coefficients without the remainder.</param>
        /// <param name="remainder">The remainder of the polynomial division as a collection of coefficients without the quotient.</param>
        public PolynomialDivisionResult(ICollection<double> quotient, ICollection<double> remainder)
        {
            Quotient = new Polynomial(quotient);
            Remainder = new Polynomial(remainder);
        }
    }
}
