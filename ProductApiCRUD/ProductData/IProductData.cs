using ProductApiCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApiCRUD.ProductData
{
   public  interface IProductData
    {
        IEnumerable<Product> GetProducts(Paginator filter);

        Product GetProduct(Guid id);

        Product AddProduct(Product product);

        void DeleteProduct(Product product);

        Product EditProduct(Product product);
    }
}
