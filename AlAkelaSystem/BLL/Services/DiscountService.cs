using AutoMapper;
using BLL.Interfaces.ModlesInterface;
using DAL.unitofwork;
using DTO.DiscountDtos;
using System.Collections.Generic;
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

        public async Task<IEnumerable<DiscountResponseDto>> GetAllDiscountsAsync()
        {
            var discounts = await _unitOfWork.DiscountRepo.GetAll();
            return _mapper.Map<IEnumerable<DiscountResponseDto>>(discounts);
        }

        public async Task<DiscountResponseDto> GetDiscountByIdAsync(int id)
        {
            var discount = await _unitOfWork.DiscountRepo.GetById(id);
            return _mapper.Map<DiscountResponseDto>(discount);
        }

        public async Task<DiscountResponseDto> CreateDiscountAsync(CreateDiscountDto createDiscountDto)
        {
            var discount = _mapper.Map<DAL.Models.Discount>(createDiscountDto);
            var createdDiscount = await _unitOfWork.DiscountRepo.Add(discount);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DiscountResponseDto>(createdDiscount);
        }

        public async Task<DiscountResponseDto> UpdateDiscountAsync(int id, UpdateDiscountDto updateDiscountDto)
        {
            var existingDiscount = await _unitOfWork.DiscountRepo.GetById(id);
            if (existingDiscount == null)
                return null;

            _mapper.Map(updateDiscountDto, existingDiscount);
            _unitOfWork.DiscountRepo.Update(existingDiscount);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DiscountResponseDto>(existingDiscount);
        }

        public async Task<bool> DeleteDiscountAsync(int id)
        {
            var result = await _unitOfWork.DiscountRepo.Delete(id);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }
    }
}
