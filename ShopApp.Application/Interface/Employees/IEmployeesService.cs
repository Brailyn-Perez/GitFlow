using ShopApp.Domain.Base;
using ShopApp.Domain.Models.Employees;

namespace ShopApp.Application.Interface.Employees
{
    public interface IEmployeesService
    {
        Task<OperationResult<EmployeesCreateModel>> CreateEmployeesAsync(EmployeesCreateModel model);
        Task<OperationResult<List<EmployeesGetModel>>> GetAllEmployeesAsync();
        Task<OperationResult<EmployeesGetModel>> GetEmployeesByIdAsync(int id);
        Task<OperationResult<EmployeesDeleteModel>> DeleteEmployeesByIdAsync(int id, int delete_user);
        Task<OperationResult<EmployeesUpdateModel>> UpdateEmployees(EmployeesUpdateModel model);
    }
}
