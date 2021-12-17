using ProductApiCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApiCRUD.ProductData
{
    public class SqlProductData : IProductData
    {
        private ProductContext _productcontext;
        public SqlProductData(ProductContext productcontext)
        {
            _productcontext = productcontext;
        }
        public Product AddProduct(Product product)
        {
            product.Id = Guid.NewGuid();
            _productcontext.Products.Add(product);
            _productcontext.SaveChanges();
            return product;
        }

        public void DeleteProduct(Product product)
        {
  
            _productcontext.Products.Remove(product);
            _productcontext.SaveChanges();
        }

        public Product EditProduct(Product product)
        {
            var existingproduct = _productcontext.Products.Find(product.Id);
            if(existingproduct != null)
            {
                existingproduct.Name = product.Name;
                existingproduct.Description = product.Description;
                existingproduct.price = product.price;
                _productcontext.Products.Update(existingproduct);
                _productcontext.SaveChanges();
            }
            return product;
        }

        public Product GetProduct(Guid id)
        {
            var product = _productcontext.Products.Find(id);
            return product;
        }

        public IEnumerable<Product> GetProducts(Paginator filter)
        {
            var paginator = new Paginator(filter.per_page, filter.current_page);
            return _productcontext.Products.Skip((paginator.current_page - 1) * paginator.per_page).Take(paginator.per_page).ToArray();
        }
    }
}
