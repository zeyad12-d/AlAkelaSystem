using BLL.Helper;
using DTO.CustomerDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.ModlesInterface
{
    public interface ICustomerServices
    {
        Task<ApiResponse<IEnumerable<CustomerResponseDto>>> Search (string Term);
        Task<ApiResponse<IEnumerable<CustomerResponseDto>>> GetAllCustomersAsync(int PageSize, int page );
       Task<ApiResponse<CustomerDetailsDto>> GetCustomerByIdAsync(string id);
        Task<ApiResponse<CustomerResponseDto>>CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task<ApiResponse<CustomerResponseDto>> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto);
        Task<ApiResponse<bool>> DeleteCustomerAsync(int id);
    }
}
