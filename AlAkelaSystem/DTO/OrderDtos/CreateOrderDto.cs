using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.OrderDtos
{
    public class CreateOrderDto
    {
        public string CustomerId {  get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public decimal totalAmount { get; set; }

        public List<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>();
    }
}
