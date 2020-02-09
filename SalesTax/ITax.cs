namespace SalesTax
{
    public interface ITax
    {
        string Description { get; }
        float Rate { get; }
    }
}
