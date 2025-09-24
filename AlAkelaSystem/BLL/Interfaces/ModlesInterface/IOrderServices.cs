using BLL.Helper;
using DTO.OrderDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.ModlesInterface
{
    public interface IOrderServices
    {
        Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetAllOrdersAsync();
        Task<ApiResponse<OrderResponseDto>> GetOrderByIdAsync(int id);
        Task<ApiResponse<OrderResponseDto>> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<ApiResponse<bool>> DeleteOrderAsync(int id);
        Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetOrdersByCustomerIdAsync(string customerId);
    }
}
