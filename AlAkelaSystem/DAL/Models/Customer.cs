using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Customer
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required, StringLength(100)] public string Name { get; set; } = string.Empty;
        [StringLength(20)] public string? Phone { get; set; }
        [StringLength(250)] public string? Address { get; set; }
    }
}