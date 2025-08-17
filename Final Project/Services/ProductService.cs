using Cafeteria_Final_Project_C_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cafeteria_Final_Project_C_.Services
{
    internal class ProductService
    {
        public ProductService() { }
        public List<Product> GetProducts()
        {
            using (var db = new Data.DBconection())
            {
                return db.Products.ToList();
            }
        }
        public Product GetProductById(int id)
        {
            using (var db = new Data.DBconection())
            {
                return db.Products.FirstOrDefault(p => p.Id == id);
            }
        }
        public void AddProduct(Product product)
        {
            using (var db = new Data.DBconection())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
        }
        public void UpdateProduct(Product product)
        {
            using (var db = new Data.DBconection())
            {
                var existingProduct = db.Products.FirstOrDefault(p => p.Id == product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Price = product.Price;
                    existingProduct.Stock = product.Stock;
                    existingProduct.IsActive = product.IsActive;
                    db.SaveChanges();
                }
            }
        }
        public void DeleteProduct(int id)
        {
            using (var db = new Data.DBconection())
            {
                var product = db.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
            }

        }
    }   
}
