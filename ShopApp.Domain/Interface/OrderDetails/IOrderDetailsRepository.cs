using ShopApp.Domain.Base;
using ShopApp.Domain.Models.OrderDetails.OrderDetailsBaseModel;


namespace ShopApp.Domain.Interface.OrderDetails
{
    public interface IOrderDetailsRepository
    {
        Task<OperationResult<OrderDetailsModel>> CreateOrderDetailsAsync(OrderDetailsModel model);
        Task<OperationResult<List<OrderDetailsModel>>> GetAllOrderDetailsAsync();
        Task<OperationResult<OrderDetailsModel>> GetOrderDetailsByIdAsync(int id); 
        Task<OperationResult<OrderDetailsModel>> DeleteOrderDetailsByIdAsync(int id);
        Task<OperationResult<OrderDetailsModel>> UpdateOrderDetails(OrderDetailsModel model);
    }
}
