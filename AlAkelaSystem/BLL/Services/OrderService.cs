using AutoMapper;
using BLL.Helper;
using BLL.Interfaces.ModlesInterface;
using DAL.Models;
using DAL.unitofwork;
using DTO.OrderDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Get All Orders
        public async Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.OrderRepo.Query()
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsNoTracking()
                .ToListAsync();

            if (!orders.Any())
                return ApiResponse<IEnumerable<OrderResponseDto>>.ErrorResponse("No Orders Found", 404);

            var mapped = _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
            return ApiResponse<IEnumerable<OrderResponseDto>>.SuccessResponse(mapped, "Success");
        }
        #endregion

        #region Get Order By Id
        public async Task<ApiResponse<OrderResponseDto>> GetOrderByIdAsync(int id)
        {
            if (id <= 0)
                return ApiResponse<OrderResponseDto>.ErrorResponse("Invalid Id", 400);

            var order = await _unitOfWork.OrderRepo.Query()
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return ApiResponse<OrderResponseDto>.ErrorResponse("Order Not Found", 404);

            var mapped = _mapper.Map<OrderResponseDto>(order);
            return ApiResponse<OrderResponseDto>.SuccessResponse(mapped, "Success");
        }
        #endregion

        #region Create Order
        public async Task<ApiResponse<OrderResponseDto>> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            try
            {
                if (createOrderDto == null)
                    return ApiResponse<OrderResponseDto>.ErrorResponse("Invalid Payload", 400);

                var order = _mapper.Map<Orders>(createOrderDto);

                var createdOrder = await _unitOfWork.OrderRepo.Add(order);
                await _unitOfWork.SaveChangesAsync();

                var mapped = _mapper.Map<OrderResponseDto>(createdOrder);
                return ApiResponse<OrderResponseDto>.SuccessResponse(mapped, "Order Created");
            }
            catch (Exception ex)
            {
                return new ApiResponse<OrderResponseDto>(500, ex.Message, null);
            }
        }
        #endregion

        #region Update Order Status
        public async Task<ApiResponse<OrderResponseDto>> UpdateOrderStatusAsync(int id, string newStatus)
        {
            var order = await _unitOfWork.OrderRepo.GetById(id);
            if (order == null)
                return ApiResponse<OrderResponseDto>.ErrorResponse("Order Not Found", 404);

            if (!Enum.TryParse<OrderStatus>(newStatus, true, out var statusEnum))
                return ApiResponse<OrderResponseDto>.ErrorResponse("Invalid Status", 400);

            order.Status = statusEnum;
            _unitOfWork.OrderRepo.Update(order);
            await _unitOfWork.SaveChangesAsync();

            var mapped = _mapper.Map<OrderResponseDto>(order);
            return ApiResponse<OrderResponseDto>.SuccessResponse(mapped, "Order Status Updated");
        }
        #endregion

        #region Delete Order
        public async Task<ApiResponse<bool>> DeleteOrderAsync(int id)
        {
            if (id <= 0)
                return ApiResponse<bool>.ErrorResponse("Invalid Id", 400);

            var order = await _unitOfWork.OrderRepo.Query()
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return new ApiResponse<bool>(404,"Order Not Found", false);

          
            foreach (var item in order.OrderItems.ToList())
            {
                await _unitOfWork.OrderItemRepo.Delete(item.OrderItemId);
            }

          
            var result = await _unitOfWork.OrderRepo.Delete(id);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
                return ApiResponse<bool>.SuccessResponse(true, "Order Deleted Successfully");
            }

            return new ApiResponse<bool>(500,"Failed to Delete Order", false);
        }
        #endregion

        #region Get Orders By CustomerId
        public async Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetOrdersByCustomerIdAsync(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                return ApiResponse<IEnumerable<OrderResponseDto>>.ErrorResponse("Invalid Customer Id", 400);

            var orders = await _unitOfWork.OrderRepo.Query()
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.CustomerId == customerId)
                .AsNoTracking()
                .ToListAsync();

            if (!orders.Any())
                return ApiResponse<IEnumerable<OrderResponseDto>>.ErrorResponse("No Orders Found for this Customer", 404);

            var mapped = _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
            return ApiResponse<IEnumerable<OrderResponseDto>>.SuccessResponse(mapped, "Success");
        }


        #endregion
      
    }
}

