using BLL.Helper;
using DTO.ProductDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces.ModlesInterface
{
    public interface IproductServices
    {
        Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetAllProductsAsync( int page ,int pageSize);
        Task<ApiResponse<ProductResponseDto>> GetProductByIdAsync(int id);
        Task<ApiResponse<ProductResponseDto>> CreateProductAsync(CreateProductDto createProductDto);
        Task<ApiResponse<ProductResponseDto>> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
        Task<ApiResponse<bool>> DeleteProductAsync(int id);
        Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetProductsByCategoryIdAsync(int categoryId);
       
    }
}
