using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product
    {
        [Key] public int Id { get; set; }
        [Required, StringLength(90)] public string Name { get; set; } = string.Empty;
        [StringLength(512)] public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal Price { get; set; }
        public int Stock { get; set; }
        [Required] public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = default;

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


    }
}
