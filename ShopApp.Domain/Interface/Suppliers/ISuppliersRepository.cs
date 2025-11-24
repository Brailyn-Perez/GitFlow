using ShopApp.Domain.Base;
using ShopApp.Domain.Models.Suppliers;

namespace ShopApp.Domain.Interface.Suppliers
{
    public interface ISuppliersRepository
    {
        Task<OperationResult<SuppliersCreateModel>> CreateSupplierAsync(SuppliersCreateModel model);
        Task<OperationResult<List<SuppliersGetModel>>> GetAllSupplierAsync();
        Task<OperationResult<SuppliersGetModel>> GetSupplierByIdAsync(int id);
        Task<OperationResult<SuppliersDeleteModel>> DeleteSupplierByIdAsync(int id, int delete_user);
        Task<OperationResult<SuppliersUpdateModel>> UpdateSupplier(SuppliersUpdateModel model);
    }
}
 