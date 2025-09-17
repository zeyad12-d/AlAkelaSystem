using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CouponDtos
{
    public class UpdateCouponDto : CreateCouponDto
    {
        public int Id { get; set; }
    }
}
