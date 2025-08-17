using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria_Final_Project_C_.Models
{
    public class TransactionItem
    {
        [Key]
        public int Id { get; set; }
        public int ProductId  { get; set; }
        public int TransactionId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}
