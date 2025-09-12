using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Discount
    {
        [Key] 
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string DiscountName { get; set; } = string.Empty;
        [Range(0, 100)] 
        public double DiscountValue { get; set; }
    }
}
