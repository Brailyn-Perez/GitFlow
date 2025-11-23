using ShopApp.Application.Interface.Customers;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.Customers;
using ShopApp.Domain.Models.Customers;

namespace ShopApp.Application.Service.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomerService(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }
        public async Task<OperationResult<CustomersCreateModel>> CreateCustmersAsync(CustomersCreateModel model)
        {
            return await _customersRepository.CreateCustmersAsync(model);
        }

        public async  Task<OperationResult<CustomersDeleteModel>> DeleteCustmersByIdAsync(int id, int delete_user)
        {
            return await _customersRepository.DeleteCustmersByIdAsync(id, delete_user);
        }

        public async Task<OperationResult<List<CustomersGetModel>>> GetAllCustmersAsync()
        {
            return await _customersRepository.GetAllCustmersAsync();
        }

        public async Task<OperationResult<CustomersGetModel>> GetCustmersByIdAsync(int id)
        {
            return await _customersRepository.GetCustmersByIdAsync(id);
        }

        public async Task<OperationResult<CustomersUpdateModel>> UpdateCustmersAsync(CustomersUpdateModel model)
        {
            return await _customersRepository.UpdateCustmersAsync(model);
        }
    }
}
