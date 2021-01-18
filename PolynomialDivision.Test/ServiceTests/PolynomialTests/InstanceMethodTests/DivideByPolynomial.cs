using System;
using System.Collections.Generic;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PolynomialDivision.Service;
using PolynomialDivision.Service.Interfaces;

namespace PolynomialDivision.Test.ServiceTests.PolynomialTests.InstanceMethodTests
{
    [TestClass]
    public class DivideByPolynomial
    {
        private static IEnumerable<object[]> PolynomialTestData => new List<object[]>
        {
            new object[] // (6x + 4) : 2 = (3x + 2) + 0
            {
                new Polynomial(4, 6),
                new Polynomial(2),
                new PolynomialDivisionResult(new Polynomial(2, 3), new Polynomial(0))
            },
            new object[] // (x^2 - 1) : (x - 1) = (x + 1) + 0
            {
                new Polynomial(-1, 0, 1),
                new Polynomial(-1, 1),
                new PolynomialDivisionResult(new Polynomial(1, 1), new Polynomial(0))
            },
            new object[] // 0 : (3x^2 + 2x + 1) = 0
            {
                new Polynomial(0),
                new Polynomial(1, 2, 3),
                new PolynomialDivisionResult(new Polynomial(0), new Polynomial(0))
            },
            new object[] // (8x^3 + 18x^2 - 15x - 16) : (4x^2 + 3x - 12) = (2x + 3) + 20
            {
                new Polynomial(-16, -16, 18, 8),
                new Polynomial(-12, 3, 4),
                new PolynomialDivisionResult(new Polynomial(3, 2), new Polynomial(20))
            },
        };

        [TestMethod]
        [DynamicData(nameof(PolynomialTestData), DynamicDataSourceType.Property)]
        public void WhenParametersAreValidThenCorrectResultIsReturned(
            IPolynomial dividend,
            IPolynomial divisor,
            IPolynomialDivisionResult expectedResult
        )
        {
            // Arrange
            IPolynomialDivisionResult result;

            // Act
            result = dividend.DivideByPolynomial(divisor);

            // Assert
            result.Quotient.Coefficients.Should().ContainInOrder(expectedResult.Quotient.Coefficients);
            result.Remainder.Coefficients.Should().ContainInOrder(expectedResult.Remainder.Coefficients);
        }

        [TestMethod]
        public void WhenDivisorIsZeroThenExceptionIsThrown()
        {
            // Arrange
            IPolynomial dividend = new Polynomial(1, 2, 3);
            IPolynomial divisor = new Polynomial(0);
            Action action;

            // Act
            action = () => dividend.DivideByPolynomial(divisor);

            // Assert
            action.Should().ThrowExactly<DivideByZeroException>();
        }
    }
}
