using System;
using System.Collections.Generic;
using System.Linq;
using Cafeteria_Final_Project_C_.Models;
using Cafeteria_Final_Project_C_.Data;

namespace Cafeteria_Final_Project_C_.Services
{
    internal class PurchaseService
    {
        public PurchaseService() { }

        public void Purchase(int studentId, List<Item> items)
        {
            using (var db = new DBconection())
            {
                
                var student = db.Students.FirstOrDefault(s => s.Id == studentId);
                if (student == null)
                    throw new Exception("Student not found");

                
                var productIds = items.Select(i => i.Id).ToList();
                var products = db.Products.Where(p => productIds.Contains(p.Id)).ToList();

                if (products.Count != items.Count)
                    throw new Exception("Some products not found");

  
                decimal totalCost = 0;

                foreach (var request in items)
                {
                    var product = products.First(p => p.Id == request.Id);

                    if (!product.IsActive)
                        throw new Exception($"Product '{product.Name}' is inactive");

                    if (product.Stock < request.Quantity)
                        throw new Exception($"Not enough stock for '{product.Name}'. Available: {product.Stock}, requested: {request.Quantity}");

                    totalCost += product.Price * request.Quantity;
                }

               
                if (student.Credit < totalCost)
                    throw new Exception("Insufficient credit");

                
                var transaction = new Transaction
                {
                    StudentId = studentId,
                    Date = DateTime.Now,
                    Total = totalCost
                };
                db.Transactions.Add(transaction);
                db.SaveChanges(); 


                foreach (var request in items)
                {
                    var product = products.First(p => p.Id == request.Id);

                    product.Stock -= request.Quantity;

                    var transactionItem = new TransactionItem
                    {
                        ProductId = product.Id,
                        TransactionId = transaction.Id,
                        Quantity = request.Quantity,
                        UnitPrice = product.Price,
                        Subtotal = product.Price * request.Quantity
                    };

                    db.TransactionItems.Add(transactionItem);
                }

                
                student.Credit -= totalCost;
                db.SaveChanges();
            }
        }
    }
}
