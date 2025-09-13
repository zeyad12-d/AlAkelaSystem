using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CategoryDtos
{
    public class CreateCategoryDto
    {
        public string Name { get; set; } = null!;
        public string? Icon { get; set; }
    }
}
