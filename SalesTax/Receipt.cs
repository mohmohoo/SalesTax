namespace SalesTax
{
    public class Receipt
        : IReceipt
    {
        public string[] ProductSummary { get; set; }

        public string SaleTaxesDescription { get; set; }

        public string TotalDescription { get; set; }

        public Receipt()
        {
            ProductSummary = new string[] { };
        }
    }
}
