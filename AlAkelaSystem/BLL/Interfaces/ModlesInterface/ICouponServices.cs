using BLL.Helper;
using DTO.CouponDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.ModlesInterface
{
    public interface ICouponServices
    {
        Task<ApiResponse<IEnumerable<CouponResponseDto>>> GetAllCouponsAsync();
        Task<ApiResponse<CouponResponseDto>> GetCouponByIdAsync(int id);
        Task<ApiResponse<CouponResponseDto>> CreateCouponAsync(CreateCouponDto createCouponDto);
        Task<ApiResponse<CouponResponseDto>> UpdateCouponAsync(int id, UpdateCouponDto updateCouponDto);
        Task<ApiResponse<bool>> DeleteCouponAsync(int id);
    }
}
