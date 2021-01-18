using System.Collections.Generic;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PolynomialDivision.Service;
using PolynomialDivision.Service.Interfaces;

namespace PolynomialDivision.Test.ServiceTests.PolynomialDivisionResultTests
{
    [TestClass]
    public class Constructor
    {
        [TestMethod]
        public void WhenCalledParameterlessThenPropertiesShouldBeNull()
        {
            // Arrange
            IPolynomialDivisionResult result;

            // Act
            result = new PolynomialDivisionResult();

            // Assert
            result.Quotient.Should().BeNull();
            result.Remainder.Should().BeNull();
        }

        [TestMethod]
        public void WhenCalledWithPolynomialParametersThenPropertiesShouldBeSet()
        {
            // Arrange
            IPolynomialDivisionResult result;
            IPolynomial quotient = new Polynomial(1, 2, 3);
            IPolynomial remainder = new Polynomial(4, 5, 6);

            // Act
            result = new PolynomialDivisionResult(quotient, remainder);

            // Assert
            result.Quotient.Should().Be(quotient);
            result.Remainder.Should().Be(remainder);
        }

        [TestMethod]
        public void WhenCalledWithNumericListsThenPropertiesShouldBeSet()
        {
            // Arrange
            IPolynomialDivisionResult result;
            IList<double> quotientList = new List<double>() { 1, 2, 3 };
            IList<double> remainderList = new List<double>() { 4, 5, 6 };

            // Act
            result = new PolynomialDivisionResult(quotientList, remainderList);

            // Assert
            result.Quotient.Coefficients.Should().ContainInOrder(quotientList);
            result.Remainder.Coefficients.Should().ContainInOrder(remainderList);
        }
    }
}
