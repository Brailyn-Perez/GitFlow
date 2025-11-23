

using ShopApp.Domain.Models.Employees.EmployeesBaseModel;

namespace ShopApp.Domain.Models.Employees
{
    public record EmployeesCreateModel : EmployeesModel
    {
        public int creation_user { get; set; }
    }
}
 