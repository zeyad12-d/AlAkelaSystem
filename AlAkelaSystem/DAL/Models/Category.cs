﻿using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Category
    {
       [Key] public int Id { get; set; }
        [Required, StringLength(50)] public string Name { get; set; } = string.Empty;
        [StringLength(100)] public string Icon { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Extras> Extras { get; set; } = new List<Extras>();
    }
}