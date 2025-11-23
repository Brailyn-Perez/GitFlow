using ShopApp.Application.Interface.Employees;
using ShopApp.Domain.Base;
using ShopApp.Domain.Interface.Employees;
using ShopApp.Domain.Models.Employees;

namespace ShopApp.Application.Service.EmployeesService
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeesRepository;

        public EmployeesService(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }
        public async Task<OperationResult<EmployeesCreateModel>> CreateEmployeesAsync(EmployeesCreateModel model)
        {
            return await _employeesRepository.CreateEmployeesAsync(model);
        }

        public async Task<OperationResult<EmployeesDeleteModel>> DeleteEmployeesByIdAsync(int id, int delete_user)
        {
            return await _employeesRepository.DeleteEmployeesByIdAsync(id, delete_user);
        }

        public async Task<OperationResult<List<EmployeesGetModel>>> GetAllEmployeesAsync()
        {
            return await _employeesRepository.GetAllEmployeesAsync();
        }

        public async Task<OperationResult<EmployeesGetModel>> GetEmployeesByIdAsync(int id)
        {
            return await _employeesRepository.GetEmployeesByIdAsync(id);
        }

        public async Task<OperationResult<EmployeesUpdateModel>> UpdateEmployees(EmployeesUpdateModel model)
        {
            return await _employeesRepository.UpdateEmployees(model);
        }
    }
}
 