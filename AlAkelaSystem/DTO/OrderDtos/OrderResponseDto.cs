using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.OrderDtos
{
    public class OrderResponseDto
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public OrderStatus Stauts { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public List<OrderItemsRepsonseDto> OrderItems { get; set; } = new List<OrderItemsRepsonseDto>();
    }
}
