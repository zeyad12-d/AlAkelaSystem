using AutoMapper;
using BLL.Interfaces.ModlesInterface;
using DAL.unitofwork;
using DTO.CategoryDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepo.GetAll();
            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
        }

        public async Task<CategoryDetailsDto> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepo.GetById(id);
            return _mapper.Map<CategoryDetailsDto>(category);
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<DAL.Models.Category>(createCategoryDto);
            var createdCategory = await _unitOfWork.CategoryRepo.Add(category);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryResponseDto>(createdCategory);
        }

        public async Task<CategoryResponseDto> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var existingCategory = await _unitOfWork.CategoryRepo.GetById(id);
            if (existingCategory == null)
                return null;

            _mapper.Map(updateCategoryDto, existingCategory);
            _unitOfWork.CategoryRepo.Update(existingCategory);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryResponseDto>(existingCategory);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var result = await _unitOfWork.CategoryRepo.Delete(id);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }
    }
}
