using AutoMapper;
using BLL.Helper;
using BLL.Interfaces.ModlesInterface;
using DAL.Models;
using DAL.unitofwork;
using DTO.CustomerDtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CustomerService : ICustomerServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #region Get All Customer
        public async Task<ApiResponse<IEnumerable<CustomerResponseDto>>> GetAllCustomersAsync( int pageSize, int page)
        {
            if (page < 1) page = 1;
            if(pageSize < 1) pageSize = 12;

            var Query = await _unitOfWork.CustomerRepo.Query()
                .Skip((page - 1)* pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
            if(Query ==null|| !Query.Any())
            {
                return new ApiResponse<IEnumerable<CustomerResponseDto>>(404,"Customer Not Found");
            }
            var Customer = _mapper.Map<IEnumerable<CustomerResponseDto>>(Query);

            return new ApiResponse<IEnumerable<CustomerResponseDto>>(200, "success", Customer);
           
        }
        #endregion

        #region Get Customer By Id
        public async Task<ApiResponse<CustomerDetailsDto>> GetCustomerByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ApiResponse<CustomerDetailsDto>.ErrorResponse("Invalid Id", 400);

            var customer = await _unitOfWork.CustomerRepo.Query()
                .Include(s => s.Orders)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (customer == null)
                return ApiResponse<CustomerDetailsDto>.ErrorResponse("Customer Not Found", 404);

            var map = _mapper.Map<CustomerDetailsDto>(customer);

            return ApiResponse<CustomerDetailsDto>.SuccessResponse(map, "Success");
        }
        #endregion

        #region Search
        public async Task<ApiResponse<IEnumerable<CustomerResponseDto>>> Search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return new ApiResponse<IEnumerable<CustomerResponseDto>>(400, "Invalid Search Term");

            var customers = await _unitOfWork.CustomerRepo.Query()
                .Where(c => c.Name.ToLower().Contains(term.ToLower())
                         || c.Address.ToLower().Contains(term.ToLower()))
                .AsNoTracking()
                .ToListAsync();

            if (customers == null || !customers.Any())
                return new ApiResponse<IEnumerable<CustomerResponseDto>>(404, "Customer Not Found");

            var mapped = _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
            return new ApiResponse<IEnumerable<CustomerResponseDto>>(200, "Success", mapped);
        }


        #endregion


        #region Create Customer
        public async Task<ApiResponse<CustomerResponseDto>> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            try
            {
                if (createCustomerDto == null)
                    return ApiResponse<CustomerResponseDto>.ErrorResponse("Invalid payload");

                var customer = _mapper.Map<Customer>(createCustomerDto);
                var createdCustomer = await _unitOfWork.CustomerRepo.Add(customer);
                await _unitOfWork.SaveChangesAsync();

                var response = _mapper.Map<CustomerResponseDto>(createdCustomer);

               
                return new ApiResponse<CustomerResponseDto>(201, "Customer Created", response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CustomerResponseDto>(500, ex.Message, null);
            }
        }
        #endregion

        #region Update Customer
        public async Task<ApiResponse<CustomerResponseDto>> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto)
        {
            if (id <= 0)
                return new ApiResponse<CustomerResponseDto>(400, "Invalid Id");

            var existingCustomer = await _unitOfWork.CustomerRepo.GetById(id);
            if (existingCustomer == null)
                return new ApiResponse<CustomerResponseDto>(404, "Customer Not Found");

            _mapper.Map(updateCustomerDto, existingCustomer);
            _unitOfWork.CustomerRepo.Update(existingCustomer);
            await _unitOfWork.SaveChangesAsync();

            var mapped = _mapper.Map<CustomerResponseDto>(existingCustomer);
            return new ApiResponse<CustomerResponseDto>(200, "Customer Updated", mapped);
        }
        #endregion


        #region DeleteCustomer
        public async Task<ApiResponse<bool>> DeleteCustomerAsync(int id)
        {
            if (id <= 0)
                return new ApiResponse<bool>(400, "Invalid Id", false);

            var customer = await _unitOfWork.CustomerRepo.GetById(id);
            if (customer == null)
                return new ApiResponse<bool>(404, "Customer Not Found", false);

            var result = await _unitOfWork.CustomerRepo.Delete(id);
            if (!result)
                return new ApiResponse<bool>(400, "Delete Failed", false);

            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<bool>(200, "Customer Deleted Successfully", true);
        }
        #endregion

    }
}

