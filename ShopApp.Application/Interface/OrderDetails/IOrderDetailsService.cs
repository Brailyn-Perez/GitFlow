using ShopApp.Domain.Base;
using ShopApp.Domain.Models.OrderDetails.OrderDetailsBaseModel;

namespace ShopApp.Application.Interface.OrderDetails
{
    public interface IOrderDetailsService
    {
        Task<OperationResult<OrderDetailsModel>> CreateOrderDetailsAsync(OrderDetailsModel model);
        Task<OperationResult<List<OrderDetailsModel>>> GetAllOrderDetailsAsync();
        Task<OperationResult<OrderDetailsModel>> GetOrderDetailsByIdAsync(int id);
        Task<OperationResult<OrderDetailsModel>> DeleteOrderDetailsByIdAsync(int id); // ver como implementar
        Task<OperationResult<OrderDetailsModel>> UpdateOrderDetails(OrderDetailsModel model);
    }
}
 