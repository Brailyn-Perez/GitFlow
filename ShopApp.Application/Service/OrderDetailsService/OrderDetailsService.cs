using ShopApp.Application.Interface.OrderDetails;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.OrderDetails;
using ShopApp.Domain.Models.OrderDetails.OrderDetailsBaseModel;

namespace ShopApp.Application.Service.OrderDetailsService
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IOrderDetailsRepository _orderDetailsRepository;

        public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }
        public async Task<OperationResult<OrderDetailsModel>> CreateOrderDetailsAsync(OrderDetailsModel model)
        {
            return await _orderDetailsRepository.CreateOrderDetailsAsync(model);
        }

        public async Task<OperationResult<OrderDetailsModel>> DeleteOrderDetailsByIdAsync(int id)
        {
            return await _orderDetailsRepository.DeleteOrderDetailsByIdAsync(id);
        }

        public async Task<OperationResult<List<OrderDetailsModel>>> GetAllOrderDetailsAsync()
        {
            return await _orderDetailsRepository.GetAllOrderDetailsAsync();
        }

        public async Task<OperationResult<OrderDetailsModel>> GetOrderDetailsByIdAsync(int id)
        {
            return await _orderDetailsRepository.GetOrderDetailsByIdAsync(id);
        }

        public async Task<OperationResult<OrderDetailsModel>> UpdateOrderDetails(OrderDetailsModel model)
        {
            return await _orderDetailsRepository.UpdateOrderDetails(model);
        }
    }
}
