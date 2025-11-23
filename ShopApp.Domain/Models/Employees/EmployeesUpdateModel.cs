

using ShopApp.Domain.Models.Employees.EmployeesBaseModel;

namespace ShopApp.Domain.Models.Employees
{
    public record EmployeesUpdateModel : EmployeesModel
    {
        public int modiffy_user { get; set; }
    }
}
