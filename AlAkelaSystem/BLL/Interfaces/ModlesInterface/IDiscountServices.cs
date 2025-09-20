using DTO.DiscountDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.ModlesInterface
{
    public interface IDiscountServices
    {
        Task<IEnumerable<DiscountResponseDto>> GetAllDiscountsAsync();
        Task<DiscountResponseDto> GetDiscountByIdAsync(int id);
        Task<DiscountResponseDto> CreateDiscountAsync(CreateDiscountDto createDiscountDto);
        Task<DiscountResponseDto> UpdateDiscountAsync(int id, UpdateDiscountDto updateDiscountDto);
        Task<bool> DeleteDiscountAsync(int id);
    }
}
