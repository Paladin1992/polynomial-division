using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PolynomialDivision.Service;
using PolynomialDivision.Service.Interfaces;

namespace PolynomialDivision.Test.ServiceTests.PolynomialTests.InstanceMethodTests
{
    [TestClass]
    public class Degree
    {
        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(0, 0)]
        [DataRow(1, 0)]
        public void WhenPolynomialIsConstantThenZeroShouldBeReturned(params double[] coeffs)
        {
            // Arrange
            IPolynomial polynomial = new Polynomial(coeffs);
            int result;

            // Act
            result = polynomial.Degree;

            // Assert
            result.Should().Be(0);
        }

        [TestMethod]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 2)]
        [DataRow(2, 1, 2, 3)]
        [DataRow(2, 0, 0, 3)]
        public void WhenPolynomialIsOfHigherDegreeThenThatDegreeShouldBeReturned(int expectedDegree, params double[] coeffs)
        {
            // Arrange
            IPolynomial polynomial = new Polynomial(coeffs);
            int result;

            // Act
            result = polynomial.Degree;

            // Assert
            result.Should().Be(expectedDegree);
        }

        [TestMethod]
        [DataRow("0x + 1 + 2 + 3", 0)]
        [DataRow("x + 1 + 2x + 3x", 1)]
        [DataRow("x^2 + 1 + 2x^2 + 3x", 2)]
        [DataRow("x^2 + 1 + 2x^4 + 3x^3", 4)]
        public void WhenPolynomialIsMixedThenCorrectDegreeShouldBeReturned(string polynomialString, int expectedDegree)
        {
            // Arrange
            IPolynomial polynomial = new Polynomial(polynomialString);
            int result;

            // Act
            result = polynomial.Degree;

            // Assert
            result.Should().Be(expectedDegree);
        }
    }
}
