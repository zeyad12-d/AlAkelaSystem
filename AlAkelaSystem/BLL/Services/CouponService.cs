using AutoMapper;
using BLL.Helper;
using BLL.Interfaces.ModlesInterface;
using DAL.Models;
using DAL.unitofwork;
using DTO.CouponDtos;
using Microsoft.EntityFrameworkCore;
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

        #region Get All Coupones
        public async Task<ApiResponse<IEnumerable<CouponResponseDto>>> GetAllCouponsAsync()
        {
            var coupons = await _unitOfWork.CouponRepo
                .Query()
                .Where(s=>s.IsActive== true)
                .ToListAsync();
            if (coupons==null||!coupons.Any())
                return new ApiResponse<IEnumerable<CouponResponseDto>>(404, "Coupnes Not Found");

            var map =  _mapper.Map<IEnumerable<CouponResponseDto>>(coupons);

            return new ApiResponse<IEnumerable<CouponResponseDto>>(200, "Success", map);
        }
        #endregion

        #region Get Coupon
        public async Task<ApiResponse<CouponResponseDto>> GetCouponByIdAsync(int id)
        {
            if (id <= 0)
                return new ApiResponse<CouponResponseDto>(400, "Invalid Id");

            var coupon = await _unitOfWork.CouponRepo.GetById(id);
            if (coupon == null) return new ApiResponse<CouponResponseDto>(404, "Coupon Not Found");

           var map= _mapper.Map<CouponResponseDto>(coupon);
            return new ApiResponse<CouponResponseDto>(200,"Success", map);
        }
        #endregion

        #region Create Coupones
        public async Task<ApiResponse<CouponResponseDto>> CreateCouponAsync(CreateCouponDto createCouponDto)
        {
            if (createCouponDto == null)
                return new ApiResponse<CouponResponseDto>(400, "Invalid Payload");
            var coupon = _mapper.Map<Coupon>(createCouponDto);

            var createdCoupon = await _unitOfWork.CouponRepo.Add(coupon);
            await _unitOfWork.SaveChangesAsync();
             var map=_mapper.Map<CouponResponseDto>(createdCoupon);
            return new ApiResponse<CouponResponseDto>(201, "Coupon Created", map);
        }
        #endregion

        #region Update Coupon
        public async Task<ApiResponse<CouponResponseDto>> UpdateCouponAsync(int id, UpdateCouponDto updateCouponDto)
        {
            if (id <= 0)
                return new ApiResponse<CouponResponseDto>(400, "Invalid Id");

            var existingCoupon = await _unitOfWork.CouponRepo.GetById(id);
            if (existingCoupon == null)
                return new ApiResponse<CouponResponseDto>(404, "Coupon Not Found");

            var updatedCoupon = _mapper.Map(updateCouponDto, existingCoupon);

            _unitOfWork.CouponRepo.Update(updatedCoupon);
            await _unitOfWork.SaveChangesAsync();

            var mapped = _mapper.Map<CouponResponseDto>(updatedCoupon);
            return new ApiResponse<CouponResponseDto>(200, "Coupon Updated", mapped);
        }
        #endregion

        #region Deleted coupon
        public async Task<ApiResponse<bool>> DeleteCouponAsync(int id)
        {
            if (id <= 0)
                return new ApiResponse<bool>(400, "Invalid Id", false);

            var coupon = await _unitOfWork.CouponRepo.GetById(id);
            if (coupon == null)
                return new ApiResponse<bool>(404, "Coupon Not Found", false);

            var result = await _unitOfWork.CouponRepo.Delete(id);
            if (!result)
                return new ApiResponse<bool>(400, "Delete Failed", false);

            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<bool>(200, "Coupon Deleted Successfully", true);
        }
        #endregion
    }
}

