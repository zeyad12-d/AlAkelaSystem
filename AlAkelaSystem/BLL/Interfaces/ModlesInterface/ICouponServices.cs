using DTO.CouponDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.ModlesInterface
{
    public interface ICouponServices
    {
        Task<IEnumerable<CouponResponseDto>> GetAllCouponsAsync();
        Task<CouponResponseDto> GetCouponByIdAsync(int id);
        Task<CouponResponseDto> CreateCouponAsync(CreateCouponDto createCouponDto);
        Task<CouponResponseDto> UpdateCouponAsync(int id, UpdateCouponDto updateCouponDto);
        Task<bool> DeleteCouponAsync(int id);
    }
}
