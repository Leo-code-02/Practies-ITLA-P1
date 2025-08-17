using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria_Final_Project_C_.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } 
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
