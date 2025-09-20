using AutoMapper;
using BLL.Interfaces.ModlesInterface;
using DAL.unitofwork;
using DTO.CouponDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CouponService : ICouponServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CouponService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CouponResponseDto>> GetAllCouponsAsync()
        {
            var coupons = await _unitOfWork.CouponRepo.GetAll();
            return _mapper.Map<IEnumerable<CouponResponseDto>>(coupons);
        }

        public async Task<CouponResponseDto> GetCouponByIdAsync(int id)
        {
            var coupon = await _unitOfWork.CouponRepo.GetById(id);
            return _mapper.Map<CouponResponseDto>(coupon);
        }

        public async Task<CouponResponseDto> CreateCouponAsync(CreateCouponDto createCouponDto)
        {
            var coupon = _mapper.Map<DAL.Models.Coupon>(createCouponDto);
            var createdCoupon = await _unitOfWork.CouponRepo.Add(coupon);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CouponResponseDto>(createdCoupon);
        }

        public async Task<CouponResponseDto> UpdateCouponAsync(int id, UpdateCouponDto updateCouponDto)
        {
            var existingCoupon = await _unitOfWork.CouponRepo.GetById(id);
            if (existingCoupon == null)
                return null;

            _mapper.Map(updateCouponDto, existingCoupon);
            _unitOfWork.CouponRepo.Update(existingCoupon);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CouponResponseDto>(existingCoupon);
        }

        public async Task<bool> DeleteCouponAsync(int id)
        {
            var result = await _unitOfWork.CouponRepo.Delete(id);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }
    }
}
