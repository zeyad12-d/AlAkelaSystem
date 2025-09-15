using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.OrderDtos
{
    public  class OrderItemsRepsonseDto
    {
      public int orderItemId { get; set; }
        public int productId { get; set; }
        public string productName { get; set; } = string.Empty;
        public int quantity { get; set; }

        public decimal price { get; set; }

        public decimal totalPrice { get; set; }

    }
}
