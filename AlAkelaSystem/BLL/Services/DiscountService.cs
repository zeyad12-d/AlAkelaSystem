using AutoMapper;
using BLL.Helper;
using BLL.Interfaces.ModlesInterface;
using DAL.Models;
using DAL.unitofwork;
using DTO.DiscountDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DiscountService : IDiscountServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DiscountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Get All Discounts
        public async Task<ApiResponse<IEnumerable<DiscountResponseDto>>> GetAllDiscountsAsync()
        {
            var discounts = await _unitOfWork.DiscountRepo.Query()
                .AsNoTracking()
                .ToListAsync();

            if (!discounts.Any())
                return ApiResponse<IEnumerable<DiscountResponseDto>>.ErrorResponse("No Discounts Found", 404);

            var mapped = _mapper.Map<IEnumerable<DiscountResponseDto>>(discounts);
            return ApiResponse<IEnumerable<DiscountResponseDto>>.SuccessResponse(mapped, "Success");
        }
        #endregion

        #region Get Discount By Id
        public async Task<ApiResponse<DiscountResponseDto>> GetDiscountByIdAsync(int id)
        {
            if (id <= 0)
                return ApiResponse<DiscountResponseDto>.ErrorResponse("Invalid Id", 400);

            var discount = await _unitOfWork.DiscountRepo.GetById(id);
            if (discount == null)
                return ApiResponse<DiscountResponseDto>.ErrorResponse("Discount Not Found", 404);

            var mapped = _mapper.Map<DiscountResponseDto>(discount);
            return ApiResponse<DiscountResponseDto>.SuccessResponse(mapped, "Success");
        }
        #endregion

        #region Create Discount
        public async Task<ApiResponse<DiscountResponseDto>> CreateDiscountAsync(CreateDiscountDto createDiscountDto)
        {
            try
            {
                if (createDiscountDto == null)
                    return ApiResponse<DiscountResponseDto>.ErrorResponse("Invalid Payload", 400);

                var discount = _mapper.Map<Discount>(createDiscountDto);

                var createdDiscount = await _unitOfWork.DiscountRepo.Add(discount);
                await _unitOfWork.SaveChangesAsync();

                var mapped = _mapper.Map<DiscountResponseDto>(createdDiscount);
                return ApiResponse<DiscountResponseDto>.SuccessResponse(mapped, "Discount Created");
            }
            catch (Exception ex)
            {
                return new ApiResponse<DiscountResponseDto>(500, ex.Message, null);
            }
        }
        #endregion

        #region Update Discount
        public async Task<ApiResponse<DiscountResponseDto>> UpdateDiscountAsync(int id, UpdateDiscountDto updateDiscountDto)
        {
            try
            {
                if (id <= 0)
                    return ApiResponse<DiscountResponseDto>.ErrorResponse("Invalid Id", 400);

                var existingDiscount = await _unitOfWork.DiscountRepo.GetById(id);
                if (existingDiscount == null)
                    return ApiResponse<DiscountResponseDto>.ErrorResponse("Discount Not Found", 404);

                _mapper.Map(updateDiscountDto, existingDiscount);
                _unitOfWork.DiscountRepo.Update(existingDiscount);

                await _unitOfWork.SaveChangesAsync();

                var mapped = _mapper.Map<DiscountResponseDto>(existingDiscount);
                return ApiResponse<DiscountResponseDto>.SuccessResponse(mapped, "Discount Updated");
            }
            catch (Exception ex)
            {
                return new ApiResponse<DiscountResponseDto>(500, ex.Message, null);
            }
        }
        #endregion

        #region Delete Discount
        public async Task<ApiResponse<bool>> DeleteDiscountAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return new ApiResponse<bool>(400,"Invalid Id", false);

                var discount = await _unitOfWork.DiscountRepo.GetById(id);
                if (discount == null)
                    return new ApiResponse<bool>(404,"Discount Not Found",  false);

                var result = await _unitOfWork.DiscountRepo.Delete(id);
                if (result)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return ApiResponse<bool>.SuccessResponse(true, "Discount Deleted Successfully");
                }

                return new ApiResponse<bool>(500, "Failed to Delete Discount", false);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(500, ex.Message, false);
            }
        }
        #endregion
    }
}
