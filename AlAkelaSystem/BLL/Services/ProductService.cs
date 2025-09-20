using AutoMapper;
using BLL.Interfaces.ModlesInterface;
using DAL.unitofwork;
using DTO.ProductDtos;
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

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepo.GetAll();
            return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepo.GetById(id);
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<DAL.Models.Product>(createProductDto);
            var createdProduct = await _unitOfWork.ProductRepo.Add(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductResponseDto>(createdProduct);
        }

        public async Task<ProductResponseDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var existingProduct = await _unitOfWork.ProductRepo.GetById(id);
            if (existingProduct == null)
                return null;

            _mapper.Map(updateProductDto, existingProduct);
            _unitOfWork.ProductRepo.Update(existingProduct);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductResponseDto>(existingProduct);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var result = await _unitOfWork.ProductRepo.Delete(id);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = _unitOfWork.ProductRepo.Query()
                .Where(p => p.CategoryId == categoryId);
            return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
        }
    }
}
