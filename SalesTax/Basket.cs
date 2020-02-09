namespace SalesTax
{
    public class Basket
        : IBasket
    {
        public IProduct[] Products { get; set; }

        public Basket(params IProduct[] products)
        {
            Products = products ?? new IProduct[] { };
        }
    }
}
