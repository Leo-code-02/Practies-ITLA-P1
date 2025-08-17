using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Cafeteria_Final_Project_C_.Models;


namespace Cafeteria_Final_Project_C_.Services
{
    internal class TransactionService
    {
        public TransactionService() { }

        public List<Transaction> GetTransactions()
        {
            using (var db = new Data.DBconection())
            {
                return db.Transactions.ToList();
            }
        }
        public Transaction GetTransactionById(int id)
        {
            using (var db = new Data.DBconection())
            {
                return db.Transactions.FirstOrDefault(t => t.Id == id);
            }
        }
        public void AddTransaction(Transaction transaction)
        {
            using (var db = new Data.DBconection())
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
            }
        }

    }
}
