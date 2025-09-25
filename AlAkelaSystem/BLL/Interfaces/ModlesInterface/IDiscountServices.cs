using BLL.Helper;
using DTO.DiscountDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.ModlesInterface
{
    public interface IDiscountServices
    {
        Task<ApiResponse<IEnumerable<DiscountResponseDto>>> GetAllDiscountsAsync();
       Task<ApiResponse<DiscountResponseDto>> GetDiscountByIdAsync(int id);
        Task<ApiResponse<DiscountResponseDto>> CreateDiscountAsync(CreateDiscountDto createDiscountDto);
        Task<ApiResponse<DiscountResponseDto>> UpdateDiscountAsync(int id, UpdateDiscountDto updateDiscountDto);
        Task<ApiResponse<bool>> DeleteDiscountAsync(int id);
    }
}
