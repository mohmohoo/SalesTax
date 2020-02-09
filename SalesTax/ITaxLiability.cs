namespace SalesTax
{
    public interface ITaxLiability
    {
        IProductCategory Category { get; }

        ITax[] Taxes { get; }
    }
}
