using System.Collections.Generic;
using System.Linq;

namespace SalesTax
{
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
}
