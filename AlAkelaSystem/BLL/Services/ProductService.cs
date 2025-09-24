using AutoMapper;
using BLL.Helper;
using BLL.Interfaces.ModlesInterface;
using DAL.Models;
using DAL.unitofwork;
using DTO.ProductDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IproductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Get All Products
        public async Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetAllProductsAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 12;

            var products = await _unitOfWork.ProductRepo.Query()
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!products.Any())
                return ApiResponse<IEnumerable<ProductResponseDto>>.ErrorResponse("No Products Found", 404);

            var mapped = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            return ApiResponse<IEnumerable<ProductResponseDto>>.SuccessResponse(mapped, "Success");
        }
        #endregion

        #region Get Product By Id
        public async Task<ApiResponse<ProductResponseDto>> GetProductByIdAsync(int id)
        {
            if (id <= 0)
                return ApiResponse<ProductResponseDto>.ErrorResponse("Invalid Id", 400);

            var product = await _unitOfWork.ProductRepo.GetById(id);
            if (product == null)
                return ApiResponse<ProductResponseDto>.ErrorResponse("Product Not Found", 404);

            var mapped = _mapper.Map<ProductResponseDto>(product);
            return ApiResponse<ProductResponseDto>.SuccessResponse(mapped, "Success");
        }
        #endregion

        #region Create Product
        public async Task<ApiResponse<ProductResponseDto>> CreateProductAsync(CreateProductDto createProductDto)
        {
            try
            {
                if (createProductDto == null)
                    return ApiResponse<ProductResponseDto>.ErrorResponse("Invalid Payload", 400);

                var product = _mapper.Map<Product>(createProductDto);
                var createdProduct = await _unitOfWork.ProductRepo.Add(product);
                await _unitOfWork.SaveChangesAsync();

                var mapped = _mapper.Map<ProductResponseDto>(createdProduct);
                return ApiResponse<ProductResponseDto>.SuccessResponse(mapped, "Product Created Successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductResponseDto>(500, ex.Message, null);
            }
        }
        #endregion

        #region Update Product
        public async Task<ApiResponse<ProductResponseDto>> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            try
            {
                if (id <= 0)
                    return ApiResponse<ProductResponseDto>.ErrorResponse("Invalid Id", 400);

                var existingProduct = await _unitOfWork.ProductRepo.GetById(id);
                if (existingProduct == null)
                    return ApiResponse<ProductResponseDto>.ErrorResponse("Product Not Found", 404);

                _mapper.Map(updateProductDto, existingProduct);
                _unitOfWork.ProductRepo.Update(existingProduct);
                await _unitOfWork.SaveChangesAsync();

                var mapped = _mapper.Map<ProductResponseDto>(existingProduct);
                return ApiResponse<ProductResponseDto>.SuccessResponse(mapped, "Product Updated Successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductResponseDto>(500, ex.Message, null);
            }
        }
        #endregion

        #region Delete Product
        public async Task<ApiResponse<bool>> DeleteProductAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return new ApiResponse<bool>(400, "Invalid Id", false);

                var product = await _unitOfWork.ProductRepo.GetById(id);
                if (product == null)
                    return new ApiResponse<bool>(404, "Product Not Found", false);

                var result = await _unitOfWork.ProductRepo.Delete(id);
                if (!result)
                    return new ApiResponse<bool>(400, "Delete Failed", false);

                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<bool>(200, "Product Deleted Successfully", true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(500, ex.Message, false);
            }
        }
        #endregion

        #region Get Products By CategoryId
        public async Task<ApiResponse<IEnumerable<ProductResponseDto>>> GetProductsByCategoryIdAsync(int categoryId)
        {
            if (categoryId <= 0)
                return ApiResponse<IEnumerable<ProductResponseDto>>.ErrorResponse("Invalid Category Id", 400);

            var products = await _unitOfWork.ProductRepo.Query()
                .Where(p => p.CategoryId == categoryId)
                .AsNoTracking()
                .ToListAsync();

            if (!products.Any())
                return ApiResponse<IEnumerable<ProductResponseDto>>.ErrorResponse("No Products Found For This Category", 404);

            var mapped = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            return ApiResponse<IEnumerable<ProductResponseDto>>.SuccessResponse(mapped, "Success");
        }
        #endregion
    }
}
