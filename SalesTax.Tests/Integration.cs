using NUnit.Framework;

namespace SalesTax.Tests
{
    public class Integration
    {
        IProductCategory bookCategory;
        IProductCategory foodCategory;
        IProductCategory medicalCategory;
        IProductCategory importedCategory;
        IProductCategory otherCategory;

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

            bookCategory = new ProductCategory { Description = ProductCategoryDescription.Book };
            foodCategory = new ProductCategory { Description = ProductCategoryDescription.Food };
            medicalCategory = new ProductCategory { Description = ProductCategoryDescription.Medical };
            importedCategory = new ProductCategory { Description = ProductCategoryDescription.Imported };
            otherCategory = new ProductCategory { Description = ProductCategoryDescription.Other };

            var bookTaxLiabilities = new TaxLiability { Category = bookCategory };
            var foodTaxLiabilities = new TaxLiability { Category = foodCategory };
            var medicalTaxLiabilities = new TaxLiability { Category = medicalCategory };
            var importedTaxLiabilities = new TaxLiability { Category = importedCategory, Taxes = new[] { importedTax } };
            var otherTaxLiabilities = new TaxLiability { Category = otherCategory, Taxes = new[] { basicTax } };

            target = new Application(bookTaxLiabilities,
                foodTaxLiabilities,
                medicalTaxLiabilities,
                importedTaxLiabilities,
                otherTaxLiabilities);
        }

        [Test]
        public void Scenario1Test()
        {
            var book = new Product(bookCategory) { Description = "Book", Price = 12.49f };
            var musicCd = new Product(otherCategory) { Description = "Music CD", Price = 14.99f };
            var chocoletBar = new Product(foodCategory) { Description = "Chocolate Bar", Price = 0.85f };

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