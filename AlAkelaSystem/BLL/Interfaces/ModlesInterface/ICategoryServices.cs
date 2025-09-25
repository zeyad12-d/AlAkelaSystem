using BLL.Helper;
using DAL.unitofwork;
using DTO.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.ModlesInterface
{
    public interface ICategoryServices
    {
        Task<ApiResponse<IEnumerable<CategoryResponseDto>>> GetAllCategoriesAsync();
        Task<ApiResponse<CategoryDetailsDto>> GetCategoryByIdAsync(int id);
        Task<ApiResponse<CategoryResponseDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<ApiResponse<CategoryResponseDto>> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);
        Task<ApiResponse<bool>> DeleteCategoryAsync(int id);
    }
}
