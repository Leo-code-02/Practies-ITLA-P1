using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria_Final_Project_C_.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string StudentNumber { get; set; } = "";
        public decimal Credit { get; set; } = 0m;
    }
}
