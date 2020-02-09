using System;
using System.Linq;

namespace SalesTax
{
    public class SalesTaxApplication
        : ITaxApplier
    {
        public ITaxLiability[] ProductCategoryTaxLiabilities { get; }

        public SalesTaxApplication(params ITaxLiability[] productCategoryTaxLiabilities)
        {
            ProductCategoryTaxLiabilities = productCategoryTaxLiabilities;
        }

        public IReceipt ApplyTaxes(IBasket basket)
        {
            var receipt = new Receipt();
            var totalWithTaxes = 0d;

            foreach (var product in basket.Products)
            {
                var taxes = ProductCategoryTaxLiabilities
                    .Where(taxLiability =>
                        product
                            .Categories
                            .Any(category => taxLiability.Category.Description == category.Description))
                    .SelectMany(liability => liability.Taxes) ?? new ITax[] { };

                var totalTaxRate = 1 + taxes.Aggregate(0f, (soFar, currentTax) => soFar + currentTax.Rate);

                totalWithTaxes += totalTaxRate * product.Price;
                receipt.ProductSummary = receipt
                    .ProductSummary
                    .Concat(new[] { $"1 {product.Description}: £{(product.Price * totalTaxRate).ToString("0.00")}" })
                    .ToArray();
            }

            var taxesOnly = (float)totalWithTaxes - basket.Products.Sum(product => product.Price);
            receipt.SaleTaxesDescription = $"Sale Taxes: £{Round(taxesOnly).ToString("0.00")}";
            receipt.TotalDescription = $"Total: £{Math.Round(totalWithTaxes, 2).ToString("0.00")}";
            return receipt;
        }

        private static float Round(float value)
        {
            return (float)Math.Ceiling(value * 20) / 20;

        }
    }
}
