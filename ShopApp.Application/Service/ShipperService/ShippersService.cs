using ShopApp.Application.Interface.Shippers;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.Shippers;
using ShopApp.Domain.Models.Shippers;

namespace ShopApp.Application.Service.ShipperService
{
    public class ShippersService : IShippersService
    {
        private readonly IShippersRepository _shippersRepository;

        public ShippersService(IShippersRepository shippersRepository)
        {
            _shippersRepository = shippersRepository;
        }
        public async Task<OperationResult<ShippersCreateModel>> CreateShippersAsync(ShippersCreateModel model)
        {
            return await _shippersRepository.CreateShippersAsync(model);
        }

        public async Task<OperationResult<ShippersDeleteModel>> DeleteShippersByIdAsync(int id, int delete_user)
        {
            return await _shippersRepository.DeleteShippersByIdAsync(id, delete_user);
        }

        public async Task<OperationResult<List<ShippersGetModel>>> GetAllShippersAsync()
        {
            return await _shippersRepository.GetAllShippersAsync();
        }

        public async Task<OperationResult<ShippersGetModel>> GetShippersByIdAsync(int id)
        {
            return await _shippersRepository.GetShippersByIdAsync(id);
        }

        public async Task<OperationResult<ShippersUpdateModel>> UpdateShippers(ShippersUpdateModel model)
        {
            return await _shippersRepository.UpdateShippers(model);
        }
    }
}
