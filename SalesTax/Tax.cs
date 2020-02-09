namespace SalesTax
{
    public class Tax
        : ITax
    {
        public string Description { get; set; }
        public float Rate { get; set; }
    }
}
