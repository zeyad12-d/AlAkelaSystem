using AutoMapper;
using BLL.Interfaces.ModlesInterface;
using DAL.unitofwork;
using DTO.OrderDtos;
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

        public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.OrderRepo.GetAll();
            return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task<OrderResponseDto> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.OrderRepo.GetById(id);
            return _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            // Create the order
            var order = new DAL.Models.Orders
            {
                CustomerName = createOrderDto.CustomerName,
                TotalAmount = createOrderDto.totalAmount,
                OrderDate = System.DateTime.Now
            };

            var createdOrder = await _unitOfWork.OrderRepo.Add(order);
            await _unitOfWork.SaveChangesAsync();

            // Create order items
            foreach (var itemDto in createOrderDto.OrderItems)
            {
                var orderItem = new DAL.Models.OrderItem
                {
                    OrderId = createdOrder.Id,
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    Price = itemDto.Price
                };
                await _unitOfWork.OrderItemRepo.Add(orderItem);
            }

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrderResponseDto>(createdOrder);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            // First delete all order items
            var orderItems = _unitOfWork.OrderItemRepo.Query()
                .Where(oi => oi.OrderId == id);
            
            foreach (var item in orderItems)
            {
                await _unitOfWork.OrderItemRepo.Delete(item.Id);
            }

            // Then delete the order
            var result = await _unitOfWork.OrderRepo.Delete(id);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var orders = _unitOfWork.OrderRepo.Query()
                .Where(o => o.CustomerId == customerId);
            return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }
    }
}
