using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria_Final_Project_C_.Models
{
    internal class Item
    {
        public Item() { }
        public int Id { get; set; }
        public int Quantity { get; set; } = 0;
        public Item(int id,int quantity)
        {
            Id = id;
            Quantity = quantity;
            
        }
    }
}
