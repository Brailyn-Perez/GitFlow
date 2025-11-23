using ShopApp.Domain.Base;
using ShopApp.Domain.Models.Order.OrderBaseModel;

namespace ShopApp.Domain.Interface
{
    public interface IOrderRepository 
    {
        Task<OperationResult<OrderModel>> CreateOrderAsync(OrderModel model);
        Task<OperationResult<List<OrderModel>>> GetAllOrderAsync();
        Task<OperationResult<OrderModel>> GetOrderByIdAsync(int id);
        Task<OperationResult<OrderModel>> DeleteOrderByIdAsync(int id);
        Task<OperationResult<OrderModel>> UpdateOrder(OrderModel model);
    }
}
