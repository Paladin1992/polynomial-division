using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PolynomialDivision.Service;
using PolynomialDivision.Service.Interfaces;

namespace PolynomialDivision.Test.ServiceTests.PolynomialTests.InstanceMethodTests
{
    [TestClass]
    public class HasOnlyZeroCoefficients
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(1, 2)]
        [DataRow(1, 2, 3)]
        public void WhenCoefficientsAreNotZeroThenResultShouldBeFalse(params double[] coeffs)
        {
            // Arrange
            IPolynomial polynomial = new Polynomial(coeffs);
            bool result;

            // Act
            result = polynomial.HasOnlyZeroCoefficients;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        [DataRow(0, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0, 10)]
        public void WhenCoefficientsContainZeroButAlsoHaveNonzeroValuesThenResultShouldBeFalse(params double[] coeffs)
        {
            // Arrange
            IPolynomial polynomial = new Polynomial(coeffs);
            bool result;

            // Act
            result = polynomial.HasOnlyZeroCoefficients;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(0, 0)]
        [DataRow(0, 0, 0)]
        public void WhenCoefficientsContainOnlyZerosThenResultShouldBeTrue(params double[] coeffs)
        {
            // Arrange
            IPolynomial polynomial = new Polynomial(coeffs);
            bool result;

            // Act
            result = polynomial.HasOnlyZeroCoefficients;

            // Assert
            result.Should().BeTrue();
        }
    }
}
