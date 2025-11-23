using ShopApp.Domain.Base;
using ShopApp.Domain.Models.Shippers;

namespace ShopApp.Application.Interface.Shippers
{
    public interface IShippersService
    {
        Task<OperationResult<ShippersCreateModel>> CreateShippersAsync(ShippersCreateModel model);
        Task<OperationResult<List<ShippersGetModel>>> GetAllShippersAsync();
        Task<OperationResult<ShippersGetModel>> GetShippersByIdAsync(int id);
        Task<OperationResult<ShippersDeleteModel>> DeleteShippersByIdAsync(int id, int delete_user);
        Task<OperationResult<ShippersUpdateModel>> UpdateShippers(ShippersUpdateModel model);
    }
}
