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
            var book = new Product(bookCategory) { Description = "book", Price = 12.49f };
            var musicCd = new Product(otherCategory) { Description = "music CD", Price = 14.99f };
            var chocoletBar = new Product(foodCategory) { Description = "chocolate bar", Price = 0.85f };

            var basket = new Basket(book, musicCd, chocoletBar);

            var receipt = target.ApplyTaxes(basket);
            Assert.AreEqual("Sale Taxes: £1.50", receipt.SaleTaxesDescription);
            Assert.AreEqual("Total: £29.83", receipt.TotalDescription);

            Assert.NotNull(receipt.ProductSummary);
            Assert.AreEqual(3, receipt.ProductSummary.Length);

            Assert.AreEqual("1 book: £12.49", receipt.ProductSummary[0]);
            Assert.AreEqual("1 music CD: £16.49", receipt.ProductSummary[1]);
            Assert.AreEqual("1 chocolate bar: £0.85", receipt.ProductSummary[2]);
        }

        [Test]
        public void Scenario2Test()
        {
            var chocolates = new Product(foodCategory, importedCategory) { Description = "imported box of chocolates", Price = 10.00f };
            var perfume = new Product(otherCategory, importedCategory) { Description = "imported bottle of perfume", Price = 47.50f };

            var basket = new Basket(chocolates, perfume);

            var receipt = target.ApplyTaxes(basket);
            Assert.AreEqual("Sale Taxes: £7.65", receipt.SaleTaxesDescription);
            Assert.AreEqual("Total: £65.12", receipt.TotalDescription);

            Assert.NotNull(receipt.ProductSummary);
            Assert.AreEqual(2, receipt.ProductSummary.Length);

            Assert.AreEqual("1 imported box of chocolates: £10.50", receipt.ProductSummary[0]);
            Assert.AreEqual("1 imported bottle of perfume: £54.63", receipt.ProductSummary[1]);
        }

        [Test]
        public void Scenario3Test()
        {
            var perfumeImported = new Product(otherCategory, importedCategory) { Description = "imported bottle of perfume", Price = 27.99f };
            var perfume = new Product(otherCategory) { Description = "bottle of perfume", Price = 18.99f };
            var pills = new Product(medicalCategory) { Description = "packet of headache pills", Price = 9.75f };
            var chocolates = new Product(importedCategory) { Description = "box of imported chocolates", Price = 11.25f };

            var basket = new Basket(perfumeImported, perfume, pills, chocolates);

            var receipt = target.ApplyTaxes(basket);
            Assert.AreEqual("Sale Taxes: £6.70", receipt.SaleTaxesDescription);
            Assert.AreEqual("Total: £74.64", receipt.TotalDescription);

            Assert.NotNull(receipt.ProductSummary);
            Assert.AreEqual(4, receipt.ProductSummary.Length);

            Assert.AreEqual("1 imported bottle of perfume: £32.19", receipt.ProductSummary[0]);
            Assert.AreEqual("1 bottle of perfume: £20.89", receipt.ProductSummary[1]);
            Assert.AreEqual("1 packet of headache pills: £9.75", receipt.ProductSummary[2]);
            Assert.AreEqual("1 box of imported chocolates: £11.81", receipt.ProductSummary[3]);
        }
    }
}