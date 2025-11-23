

using ShopApp.Domain.Models.Employees.EmployeesBaseModel;

namespace ShopApp.Domain.Models.Employees
{
    public record EmployeesDeleteModel : EmployeesModel
    {
        public int delete_user { get; set; }
    }
}
