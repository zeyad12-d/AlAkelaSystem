using DTO.CustomerDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.ModlesInterface
{
    public interface ICustomerServices
    {
        Task<IEnumerable<CustomerResponseDto>> GetAllCustomersAsync();
        Task<CustomerDetailsDto> GetCustomerByIdAsync(int id);
        Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task<CustomerResponseDto> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
