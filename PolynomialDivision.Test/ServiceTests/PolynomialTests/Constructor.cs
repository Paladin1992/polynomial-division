using System;
using System.Collections.Generic;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PolynomialDivision.Service;
using PolynomialDivision.Service.Exceptions;
using PolynomialDivision.Service.Interfaces;

namespace PolynomialDivision.Test.ServiceTests.PolynomialTests
{
    [TestClass]
    public class Constructor
    {
        [TestMethod]
        public void WhenCalledParameterlessThenPropertiesShouldBeSetProperly()
        {
            // Arrange
            IPolynomial polynomial;

            // Act
            polynomial = new Polynomial();

            // Assert
            polynomial.Coefficients.Should().ContainSingle(coeff => coeff == 0);
            polynomial.Degree.Should().Be(0);
            polynomial.HasOnlyZeroCoefficients.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCalledWithParameterListThenPropertiesShouldBeSetProperly()
        {
            // Arrange
            IPolynomial polynomial;

            // Act
            polynomial = new Polynomial(1, 2, 3);

            // Assert
            polynomial.Coefficients.Should().ContainInOrder(1, 2, 3);
            polynomial.Degree.Should().Be(2);
            polynomial.HasOnlyZeroCoefficients.Should().BeFalse();
        }

        [TestMethod]
        public void WhenCalledWithListThenPropertiesShouldBeSetProperly()
        {
            // Arrange
            IPolynomial polynomial;
            IList<double> coeffs = new List<double>() { 1, 2, 3 };

            // Act
            polynomial = new Polynomial(coeffs);

            // Assert
            polynomial.Coefficients.Should().ContainInOrder(coeffs);
            polynomial.Degree.Should().Be(2);
            polynomial.HasOnlyZeroCoefficients.Should().BeFalse();
        }

        [TestMethod]
        [DataRow("1", 1)]
        [DataRow("2x + 1", 1, 2)]
        [DataRow("x", 0, 1)]
        [DataRow("3x^2 - 4x^5", 0, 0, 3, 0, 0, -4)]
        [DataRow("-3x^2 - 1", -1, 0, -3)]
        public void WhenCalledWithValidPolynomialStringThenPropertiesShouldBeSetProperly(
            string polynomialString,
            params double[] expectedCoeffs
        )
        {
            // Arrange
            IPolynomial polynomial;

            // Act
            polynomial = new Polynomial(polynomialString);

            // Assert
            polynomial.Coefficients.Should().ContainInOrder(expectedCoeffs);
            polynomial.Degree.Should().Be(expectedCoeffs.Length - 1);
            polynomial.HasOnlyZeroCoefficients.Should().BeFalse();
        }
        
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("hello")]
        [DataRow("xxx")]
        [DataRow("x^x")]
        [DataRow("2^x")]
        public void WhenCalledWithInvalidPolynomialStringThenExceptionShouldBeThrown(string polynomialString)
        {
            // Arrange
            Action action;

            // Act
            action = () => new Polynomial(polynomialString);

            // Assert
            action.Should().ThrowExactly<InvalidPolynomialException>();
        }
    }
}
