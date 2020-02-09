namespace SalesTax
{
    public interface IReceipt
    {
        string[] ProductSummary { get; }

        string SaleTaxesDescription { get; }

        string TotalDescription { get; }
    }
}
