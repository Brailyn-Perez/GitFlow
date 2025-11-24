

using ShopApp.Domain.Base;
using ShopApp.Domain.Models.Order.OrderBaseModel;

namespace ShopApp.Application.Interface.Order
{
    public interface IOrderService
    {
        Task<OperationResult<OrderModel>> CreateOrderAsync(OrderModel model);
        Task<OperationResult<List<OrderModel>>> GetAllOrderAsync();
        Task<OperationResult<OrderModel>> GetOrderByIdAsync(int id);
        Task<OperationResult<OrderModel>> DeleteOrderByIdAsync(int id);
        Task<OperationResult<OrderModel>> UpdateOrder(OrderModel model);
    }
}
 