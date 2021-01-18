namespace PolynomialDivision.Service.Interfaces
{
    public interface IPolynomialDivisionResult
    {
        IPolynomial Quotient { get; set; }
        IPolynomial Remainder { get; set; }
    }
}
