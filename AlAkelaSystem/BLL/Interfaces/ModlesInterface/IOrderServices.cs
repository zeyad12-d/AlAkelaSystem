using DTO.OrderDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.ModlesInterface
{
    public interface IOrderServices
    {
        Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
        Task<OrderResponseDto> GetOrderByIdAsync(int id);
        Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<bool> DeleteOrderAsync(int id);
        Task<IEnumerable<OrderResponseDto>> GetOrdersByCustomerIdAsync(int customerId);
    }
}
