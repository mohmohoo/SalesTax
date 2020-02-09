
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesTax
{
    public interface IProduct
    {
        string Description { get; }

        float Price { get; }

        IReadOnlyCollection<IProductCategory> Categories { get; }
    }

    

    
}
