using System;
using System.Linq;

namespace SalesTax
{
    class Program
    {
        private static IProductCategory bookCategory = new ProductCategory { Description = ProductCategoryDescription.Book };
        private static IProductCategory foodCategory = new ProductCategory { Description = ProductCategoryDescription.Food };
        private static IProductCategory medicalCategory = new ProductCategory { Description = ProductCategoryDescription.Medical };
        private static IProductCategory importedCategory = new ProductCategory { Description = ProductCategoryDescription.Imported };
        private static IProductCategory otherCategory = new ProductCategory { Description = ProductCategoryDescription.Other };

        private static ITax basicTax = new Tax
        {
            Description = "Basic",
            Rate = 0.1f
        };

        private static ITax importedTax = new Tax
        {
            Description = "Imported",
            Rate = 0.05f
        };

        private static ITaxLiability bookTaxLiabilities = new TaxLiability { Category = bookCategory };
        private static ITaxLiability foodTaxLiabilities = new TaxLiability { Category = foodCategory };
        private static ITaxLiability medicalTaxLiabilities = new TaxLiability { Category = medicalCategory };
        private static ITaxLiability importedTaxLiabilities = new TaxLiability { Category = importedCategory, Taxes = new[] { importedTax } };
        private static ITaxLiability otherTaxLiabilities = new TaxLiability { Category = otherCategory, Taxes = new[] { basicTax } };

        private static ITaxApplier taxApplier = new SalesTaxApplication(bookTaxLiabilities,
                foodTaxLiabilities,
                medicalTaxLiabilities,
                importedTaxLiabilities,
                otherTaxLiabilities);

        static void Main(string[] args)
        {
            var exit = 1;
            
            while (exit != 0)
            {
                Console.WriteLine("Please type 1, 2 or 3 to print out corresponding scenario basket or 0 to exit");

                if (int.TryParse(Console.ReadLine(), out var option))
                {
                    switch(option)
                    {
                        case 1:
                            PrintScenario1();
                            break;
                        case 2:
                            PrintScenario2();
                            break;
                        case 3:
                            PrintScenario3();
                            break;
                        
                    }
                }

                exit = option;
                
            }
        }

        private static void PrintScenario1()
        {
            var book = new Product(bookCategory) { Description = "book", Price = 12.49f };
            var musicCd = new Product(otherCategory) { Description = "music CD", Price = 14.99f };
            var chocoletBar = new Product(foodCategory) { Description = "chocolate bar", Price = 0.85f };

            var basket = new Basket(book, musicCd, chocoletBar);

            Print(basket);
        }

        private static void PrintScenario2()
        {
            var book = new Product(bookCategory) { Description = "book", Price = 12.49f };
            var musicCd = new Product(otherCategory) { Description = "music CD", Price = 14.99f };
            var chocoletBar = new Product(foodCategory) { Description = "chocolate bar", Price = 0.85f };

            var basket = new Basket(book, musicCd, chocoletBar);

            Print(basket);

        }

        private static void PrintScenario3()
        {
            var chocolates = new Product(foodCategory, importedCategory) { Description = "imported box of chocolates", Price = 10.00f };
            var perfume = new Product(otherCategory, importedCategory) { Description = "imported bottle of perfume", Price = 47.50f };

            var basket = new Basket(chocolates, perfume);

            Print(basket);
        }

        private static void Print(IBasket basket)
        {
            var receipt = taxApplier.ApplyTaxes(basket);

            receipt
                .ProductSummary
                .ToList()
                .ForEach(summary => Console.WriteLine(summary));

            Console.WriteLine(receipt.SaleTaxesDescription);

            Console.WriteLine(receipt.TotalDescription);
        }
    }
}
