using System.Collections.Generic;

namespace PolynomialDivision.Service.Interfaces
{
    public interface IPolynomial
    {
        double this[int index] { get; set; }

        IList<double> Coefficients { get; set; }
        int Degree { get; }
        bool HasOnlyZeroCoefficients { get; }

        IPolynomialDivisionResult DivideByPolynomial(IPolynomial divisorPolynomial);
        string Print(bool insertSpaces = false);
    }
}
