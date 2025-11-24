using ShopApp.Application.Interface.Order;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface;
using ShopApp.Domain.Models.Order.OrderBaseModel;

namespace ShopApp.Application.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {

            _orderRepository = orderRepository;
        }
        public async Task<OperationResult<OrderModel>> CreateOrderAsync(OrderModel model)
        {
            return await _orderRepository.CreateOrderAsync(model);
        }

        public async Task<OperationResult<OrderModel>> DeleteOrderByIdAsync(int id)
        {
            return await _orderRepository.DeleteOrderByIdAsync(id);
        }

        public async Task<OperationResult<List<OrderModel>>> GetAllOrderAsync()
        {
            return await _orderRepository.GetAllOrderAsync();
        }

        public async Task<OperationResult<OrderModel>> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetOrderByIdAsync(id);
        }

        public async Task<OperationResult<OrderModel>> UpdateOrder(OrderModel model)
        {
            return await _orderRepository.UpdateOrder(model);
        }
    }
}
 