using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Cafeteria_Final_Project_C_.Models;


namespace Cafeteria_Final_Project_C_.Data
{
    internal class DBconection : DbContext
    {
        private static readonly string _connectionString = "Server=ROXY\\SQLEXPRESS;Database=Cafeteria;Trusted_Connection=True;TrustServerCertificate=True;";
        
        public DBconection()
        {
            Database.EnsureCreated();
        }

        public DBconection(DbContextOptions<DBconection> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
