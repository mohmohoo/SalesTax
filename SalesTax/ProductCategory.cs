namespace SalesTax
{
    public class ProductCategory
        : IProductCategory
    {
        public string Description { get; set; }

        public ITax[] TaxLiabilities { get; set; }
    }
}
