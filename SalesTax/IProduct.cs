
using System.Collections.Generic;
using System.Linq;

namespace SalesTax
{
    public static class ProductCategoryDescription
    {
        public const string Book = "Book";
        public const string Medical = "Medical";
        public const string Food = "Food";
        public const string Imported = "Imported";
        public const string Other = "Other";
    }

    public interface IProductCategory
    {
        string Description { get; }

        ITax[] TaxLiabilities { get; set; }
    }

    public class ProductCategory
        : IProductCategory
    {
        public string Description { get; set; }

        public ITax[] TaxLiabilities { get; set; }
    }

    public interface IProduct
    {
        string Description { get; }

        float Price { get; }

        IReadOnlyCollection<IProductCategory> Categories { get; }
    }

    public class Product
        : IProduct
    {
        public string Description { get; set; }

        public float Price { get; set; }

        public IReadOnlyCollection<IProductCategory> Categories { get; }

        public Product()
        {
            Categories = new IProductCategory[] { };
        }

        public Product(params IProductCategory[] productCategories)
            : this()
        {
            Categories = productCategories
                    .Concat(Categories)
                    .ToArray();
        }
    }

    public interface ITaxLiability
    {
        IProductCategory Category { get; }

        ITax[] Taxes { get; }
    }

    public class TaxLiability
        : ITaxLiability
    {
        
        public IProductCategory Category { get; set; }

        public ITax[] Taxes { get; set; }

        public TaxLiability()
        {
            Taxes = new ITax[] { };
        }
    }

    public interface ITaxApplier
    {
        ITaxLiability[] ProductCategoryTaxLiabilities { get; }

        IReceipt ApplyTaxes(IBasket basket);
    }

    public class Application
        : ITaxApplier
    {
        public ITaxLiability[] ProductCategoryTaxLiabilities { get; }

        public Application(params ITaxLiability[] productCategoryTaxLiabilities)
        {
            ProductCategoryTaxLiabilities = productCategoryTaxLiabilities;
        }

        public IReceipt ApplyTaxes(IBasket basket)
        {
           
            throw new System.NotImplementedException();
        }
    }

    public interface ITax
    {
        string Description { get; }
        float Rate { get; }
    }

    public class Tax
        : ITax
    {
        public string Description { get; set; }
        public float Rate { get; set; }
    }

    public interface IBasket
    {
        IProduct[] Products { get; }
    }

    public class Basket
        : IBasket
    {
        public IProduct[] Products { get; set; }

        public Basket(params IProduct[] products)
        {
            Products = products ?? new IProduct[] { };
        }
    }

    public interface IReceipt
    {
        string[] ProductSummary { get; }

        string SaleTaxesDescription { get; }

        string TotalDescription { get; }
    }

    public class Receipt
        : IReceipt
    {
        public string[] ProductSummary { get; set; }

        public string SaleTaxesDescription { get; set; }

        public string TotalDescription { get; set; }

        public Receipt()
        {
            ProductSummary = new string[] { };
        }
    }
}
