
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
    }

    public interface ITaxExemptCategory
        : IProductCategory
    {
    }

    public interface IImportedCategory
        : IProductCategory
    { }

    public class ProductCategory
        : IProductCategory
    {
        public string Description { get; set; }
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

        public Product(bool isImported, params IProductCategory[] productCategories)
        {
            var _productCategories = productCategories ?? new ProductCategory[] { };
            var _categories = isImported
                ? new IProductCategory[] { new ProductCategory { Description = ProductCategoryDescription.Imported } }
                    .Concat(_productCategories)
                : new IProductCategory[] { }.Concat(_productCategories);

            Categories = _categories.ToArray();
        }
    }

    public interface ITaxLiability
    {
        void Define(IProductCategory category, params ITax[] taxes);

        ITax[] For(IProductCategory category);
    }

    public class TaxLiability
        : ITaxLiability
    {
        public void Define(IProductCategory category, params ITax[] taxes)
        {
            throw new System.NotImplementedException();
        }

        public ITax[] For(IProductCategory category)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface ITaxApplier
    {
        ITaxLiability ProductCategoryTaxLiabilities { get; }

        IReceipt ApplyTaxes(IBasket product);
    }

    public class Application
        : ITaxApplier
    {
        public ITaxLiability ProductCategoryTaxLiabilities { get; }

        public Application(ITaxLiability productCategoryTaxLiabilities)
        {
            ProductCategoryTaxLiabilities = productCategoryTaxLiabilities;
        }

        public IReceipt ApplyTaxes(IBasket product)
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
}
