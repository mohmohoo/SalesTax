namespace SalesTax
{
    public class TaxLiability
        : ITaxLiability
    {

        public IProductCategory Category { get; set; }

        public ITax[] Taxes { get; set; }

        public TaxLiability()
        {
            Taxes = new ITax[] { };
        }
    }
}
