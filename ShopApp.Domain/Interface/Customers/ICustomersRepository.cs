using ShopApp.Domain.Base;
using ShopApp.Domain.Models.Customers;


namespace ShopApp.Domain.Interface.Customers
{
    public interface ICustomersRepository
    {
        Task<OperationResult<CustomersCreateModel>> CreateCustmersAsync(CustomersCreateModel model);
        Task<OperationResult<List<CustomersGetModel>>> GetAllCustmersAsync();
        Task<OperationResult<CustomersGetModel>> GetCustmersByIdAsync(int id);
        Task<OperationResult<CustomersDeleteModel>> DeleteCustmersByIdAsync(int id, int delete_user);
        Task<OperationResult<CustomersUpdateModel>> UpdateCustmersAsync(CustomersUpdateModel model);
    }
}
