using AutoMapper;
using BLL.Interfaces.ModlesInterface;
using DAL.unitofwork;
using DTO.CustomerDtos;
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

        public async Task<IEnumerable<CustomerResponseDto>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.CustomerRepo.GetAll();
            return _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
        }

        public async Task<CustomerDetailsDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _unitOfWork.CustomerRepo.GetById(id);
            return _mapper.Map<CustomerDetailsDto>(customer);
        }

        public async Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = _mapper.Map<DAL.Models.Customer>(createCustomerDto);
            var createdCustomer = await _unitOfWork.CustomerRepo.Add(customer);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CustomerResponseDto>(createdCustomer);
        }

        public async Task<CustomerResponseDto> UpdateCustomerAsync(int id, UpdateCustomerDto updateCustomerDto)
        {
            var existingCustomer = await _unitOfWork.CustomerRepo.GetById(id);
            if (existingCustomer == null)
                return null;

            _mapper.Map(updateCustomerDto, existingCustomer);
            _unitOfWork.CustomerRepo.Update(existingCustomer);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CustomerResponseDto>(existingCustomer);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var result = await _unitOfWork.CustomerRepo.Delete(id);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }
    }
}
