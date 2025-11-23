using ShopApp.Application.Interface.Suppliers;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.Suppliers;
using ShopApp.Domain.Models.Suppliers;

namespace ShopApp.Application.Service.SupplierService
{
    public class SupplierService : ISuppliersService
    {
        private readonly ISuppliersRepository _suppliersRepository;

        public SupplierService(ISuppliersRepository suppliersRepository)
        {
            _suppliersRepository = suppliersRepository;
        }
        public async Task<OperationResult<SuppliersCreateModel>> CreateSupplierAsync(SuppliersCreateModel model)
        {
            return await _suppliersRepository.CreateSupplierAsync(model);
        }

        public async Task<OperationResult<SuppliersDeleteModel>> DeleteSupplierByIdAsync(int id, int delete_user)
        {
            return await _suppliersRepository.DeleteSupplierByIdAsync(id, delete_user);
        }

        public async Task<OperationResult<List<SuppliersGetModel>>> GetAllSupplierAsync()
        {
            return await _suppliersRepository.GetAllSupplierAsync();
        }

        public async Task<OperationResult<SuppliersGetModel>> GetSupplierByIdAsync(int id)
        {
            return await _suppliersRepository.GetSupplierByIdAsync(id);
        }

        public async Task<OperationResult<SuppliersUpdateModel>> UpdateSupplier(SuppliersUpdateModel model)
        {
            return await _suppliersRepository.UpdateSupplier(model);
        }
    }
}
