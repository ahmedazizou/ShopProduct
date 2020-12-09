using ProductShop.API.Data.Repositories;
using ProductShop.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductShop.API.Data
{
    public class ProductRepo : IProductRepo
    {
        private readonly ProductShopContext _context;

        public ProductRepo(ProductShopContext context)
        {
            _context = context;

        }

        public Product GetProductById(int id)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == id);

            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = _context.Products.ToList();

            return products;
        }

        public void UpdateProduct(Product product)
        {
      
        }
        public void CreateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _context.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _context.Products.Remove(product);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
