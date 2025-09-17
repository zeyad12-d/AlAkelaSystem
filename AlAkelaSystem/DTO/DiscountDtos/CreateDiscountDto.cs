using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DiscountDtos
{
    public class CreateDiscountDto
    {
        public string discountName { get; set; } = string.Empty;
        public double discountValue { get; set; }


    }
}
