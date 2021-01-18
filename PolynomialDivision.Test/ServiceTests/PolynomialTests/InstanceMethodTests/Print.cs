using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PolynomialDivision.Service;
using PolynomialDivision.Service.Interfaces;

namespace PolynomialDivision.Test.ServiceTests.PolynomialTests.InstanceMethodTests
{
    [TestClass]
    public class Print
    {
        [TestMethod]
        public void WhenSpacesAreNotSpecifiedThenResultShouldNotContainSpacesAroundOperators()
        {
            // Arrange
            IPolynomial polynomial = new Polynomial(1, 2, 3);
            string expected = "3x^2+2x+1";

            // Act
            string result = polynomial.Print(insertSpaces: false);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void WhenSpacesAreSpecifiedThenResultShouldContainSpacesAroundOperators()
        {
            // Arrange
            IPolynomial polynomial = new Polynomial(1, 2, 3);
            string expected = "3x^2 + 2x + 1";

            // Act
            string result = polynomial.Print(insertSpaces: true);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
