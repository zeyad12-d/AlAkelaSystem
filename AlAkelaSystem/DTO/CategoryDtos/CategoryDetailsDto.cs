using DTO.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CategoryDtos
{
    public class CategoryDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Icon { get; set; } = null!;

        public List<ProductResponseDto> Products { get; set; } = new List<ProductResponseDto>();
    }
}
