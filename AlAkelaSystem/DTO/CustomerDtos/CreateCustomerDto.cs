using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.CustomerDtos
{
    public class CreateCustomerDto
    {
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
