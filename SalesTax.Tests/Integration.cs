using NUnit.Framework;

namespace SalesTax.Tests
{
    public class Integration
    {
        ITaxApplier target; 

        [SetUp]
        public void Setup()
        {
            var basicTax = new Tax
            {
                Description = "Basic",
                Rate = 0.1f
            };

            var importedTax = new Tax
            {
                Description ="Imported",
                Rate = 0.05f
            };

            var bookCategory = new ProductCategory { Description = ProductCategoryDescription.Book };
            var foodCategory = new ProductCategory { Description = ProductCategoryDescription.Food };
            var medicalCategory = new ProductCategory { Description = ProductCategoryDescription.Medical };
            var importedCategory = new ProductCategory { Description = ProductCategoryDescription.Imported };
            var otherCategory = new ProductCategory { Description = ProductCategoryDescription.Other };

            var productTaxLiabilities = new TaxLiability();

            productTaxLiabilities.Define(bookCategory);
            productTaxLiabilities.Define(foodCategory);
            productTaxLiabilities.Define(medicalCategory);
            productTaxLiabilities.Define(importedCategory, importedTax);
            productTaxLiabilities.Define(otherCategory, basicTax);

            target = new Application(productTaxLiabilities);
        }

        [Test]
        public void Scenario1Test()
        {
            var book = new Product(false) { Description = "Book", Price = 12.49f };
            var musicCd = new Product(false) { Description = "Music CD", Price = 14.99f };
            var chocoletBar = new Product(false) { Description = "Chocolate Bar", Price = 0.85f };

            var basket = new Basket(book, musicCd, chocoletBar);

            var reciept = target.ApplyTaxes(basket);
            Assert.AreEqual("Sale Taxes: £1.50", reciept.SaleTaxesDescription);
            Assert.AreEqual("Total: £23.83", reciept.TotalDescription);

            Assert.NotNull(reciept.ProductSummary);
            Assert.AreEqual(3, reciept.ProductSummary.Length);

            Assert.AreEqual("1 book: £12.49", reciept.ProductSummary[0]);
            Assert.AreEqual("1 music CD: £16.49", reciept.ProductSummary[1]);
            Assert.AreEqual("1 chocolate bar: £0.85", reciept.ProductSummary[2]);
        }
    }
}