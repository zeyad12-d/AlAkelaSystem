using AutoMapper;
using BLL.Helper;
using BLL.Interfaces.ModlesInterface;
using DAL.Models;
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

        #region GetAll Category
        public async Task<ApiResponse<IEnumerable<CategoryResponseDto>>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepo.GetAll();

            if (categories == null || !categories.Any())
                return new ApiResponse<IEnumerable<CategoryResponseDto>>(404, "Category Not Found");

            var mapped = _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
            return new ApiResponse<IEnumerable<CategoryResponseDto>>(200, "Success", mapped);
        }
        #endregion

        #region GetCategoryById
        public async Task<ApiResponse<CategoryDetailsDto>> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepo.GetById(id);
            if (category == null)
                return new ApiResponse<CategoryDetailsDto>(404, "Category Not Found");

            var mapped = _mapper.Map<CategoryDetailsDto>(category);
            return new ApiResponse<CategoryDetailsDto>(200, "Success", mapped);
        }
        #endregion

        #region Create Category
        public async Task<ApiResponse<CategoryResponseDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null)
                return new ApiResponse<CategoryResponseDto>(400, "Invalid Payload");

            var category = _mapper.Map<Category>(createCategoryDto);
            var createdCategory = await _unitOfWork.CategoryRepo.Add(category);

            await _unitOfWork.SaveChangesAsync();

            var mapped = _mapper.Map<CategoryResponseDto>(createdCategory);
            return new ApiResponse<CategoryResponseDto>(201, "Category Created", mapped);
        }
        #endregion

        #region UpdateCategory
        public async Task<ApiResponse<CategoryResponseDto>> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var existingCategory = await _unitOfWork.CategoryRepo.GetById(id);
            if (existingCategory == null)
                return new ApiResponse<CategoryResponseDto>(404, "Category Not Found");

            var category = _mapper.Map(updateCategoryDto, existingCategory);
            _unitOfWork.CategoryRepo.Update(category);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<CategoryResponseDto>(category);
            return new ApiResponse<CategoryResponseDto>(200, "Category Updated", response);
        }
        #endregion

        #region DeleteCategory
        public async Task<ApiResponse<bool>> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepo.GetById(id);
            if (category == null)
                return new ApiResponse<bool>(404, "Category Not Found");

            var result = await _unitOfWork.CategoryRepo.Delete(id);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<bool>(200, "Category Deleted", true);
            }

            return new ApiResponse<bool>(400, "Delete Failed", false);
        }
        #endregion
    }
}

