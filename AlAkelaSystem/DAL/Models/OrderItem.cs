using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class OrderItem
    {
        [Key] public int OrderItemId { get; set; }
        public string OrderId { get; set; } = string.Empty;
        public virtual Orders Order { get; set; } = default!;
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = default!;
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal UnitPrice { get; set; }

        public virtual ICollection<SelectExtrasProduct> SelectExtras { get; set; } = new List<SelectExtrasProduct>();
    }
}