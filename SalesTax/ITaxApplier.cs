using System;
namespace SalesTax
{
    public interface ITaxApplier
    {
        ITaxLiability[] ProductCategoryTaxLiabilities { get; }

        IReceipt ApplyTaxes(IBasket basket);
    }
}
