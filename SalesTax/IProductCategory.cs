using System;
namespace SalesTax
{
    public interface IProductCategory
    {
        string Description { get; }

        ITax[] TaxLiabilities { get; set; }
    }
}
