using AutoMapper;
using BLL.Helper;
using BLL.Interfaces.ModlesInterface;
using DAL.Models;
using DAL.unitofwork;
using DTO.ExtrasDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ExtrasService : IExtrasServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExtrasService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Get All Extras
        public async Task<ApiResponse<IEnumerable<ExtrasResponseDto>>> GetAllExtrasAsync()
        {
            var extras = await _unitOfWork.ExtrasRepo.Query()
                .AsNoTracking()
                .ToListAsync();

            if (!extras.Any())
                return ApiResponse<IEnumerable<ExtrasResponseDto>>.ErrorResponse("No Extras Found", 404);

            var mapped = _mapper.Map<IEnumerable<ExtrasResponseDto>>(extras);
            return ApiResponse<IEnumerable<ExtrasResponseDto>>.SuccessResponse(mapped, "Success");
        }
        #endregion

        #region Get Extras By Id
        public async Task<ApiResponse<ExtrasResponseDto>> GetExtrasByIdAsync(int id)
        {
            if (id <= 0)
                return ApiResponse<ExtrasResponseDto>.ErrorResponse("Invalid Id", 400);

            var extras = await _unitOfWork.ExtrasRepo.GetById(id);
            if (extras == null)
                return ApiResponse<ExtrasResponseDto>.ErrorResponse("Extras Not Found", 404);

            var mapped = _mapper.Map<ExtrasResponseDto>(extras);
            return ApiResponse<ExtrasResponseDto>.SuccessResponse(mapped, "Success");
        }
        #endregion

        #region Create Extras
        public async Task<ApiResponse<ExtrasResponseDto>> CreateExtrasAsync(CreateExtrasDto createExtrasDto)
        {
            try
            {
                if (createExtrasDto == null)
                    return ApiResponse<ExtrasResponseDto>.ErrorResponse("Invalid Payload", 400);

                var extras = _mapper.Map<Extras>(createExtrasDto);

                var createdExtras = await _unitOfWork.ExtrasRepo.Add(extras);
                await _unitOfWork.SaveChangesAsync();

                var mapped = _mapper.Map<ExtrasResponseDto>(createdExtras);
                return ApiResponse<ExtrasResponseDto>.SuccessResponse(mapped, "Extras Created Successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse<ExtrasResponseDto>(500, ex.Message, null);
            }
        }
        #endregion

        #region Update Extras
        public async Task<ApiResponse<ExtrasResponseDto>> UpdateExtrasAsync(int id, UpdateExtrasDto updateExtrasDto)
        {
            try
            {
                if (id <= 0)
                    return ApiResponse<ExtrasResponseDto>.ErrorResponse("Invalid Id", 400);

                var existingExtras = await _unitOfWork.ExtrasRepo.GetById(id);
                if (existingExtras == null)
                    return ApiResponse<ExtrasResponseDto>.ErrorResponse("Extras Not Found", 404);

                _mapper.Map(updateExtrasDto, existingExtras);
                _unitOfWork.ExtrasRepo.Update(existingExtras);
                await _unitOfWork.SaveChangesAsync();

                var mapped = _mapper.Map<ExtrasResponseDto>(existingExtras);
                return ApiResponse<ExtrasResponseDto>.SuccessResponse(mapped, "Extras Updated Successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse<ExtrasResponseDto>(500, ex.Message, null);
            }
        }
        #endregion

        #region Delete Extras
        public async Task<ApiResponse<bool>> DeleteExtrasAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return new ApiResponse<bool>(400,"Invalid Id", false);

                var extras = await _unitOfWork.ExtrasRepo.GetById(id);
                if (extras == null)
                    return  new ApiResponse<bool>(404,"Extras Not Found", false);

                var result = await _unitOfWork.ExtrasRepo.Delete(id);
                if (result)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return ApiResponse<bool>.SuccessResponse(true, "Extras Deleted Successfully");
                }

                return new ApiResponse<bool>(500, "Failed to Delete Extras", false);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(500, ex.Message, false);
            }
        }
        #endregion
    }
}
