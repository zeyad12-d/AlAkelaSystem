using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class SelectExtrasProduct
    {
        [Key] public int Id { get; set; }
        public int OrderItemId { get; set; }
        public virtual OrderItem OrderItem { get; set; } = default!;
        public int ExtraId { get; set; }
        public virtual Extras Extras { get; set; } = default!;
        [StringLength(250)] public string? Description { get; set; } = string.Empty;
    }
}