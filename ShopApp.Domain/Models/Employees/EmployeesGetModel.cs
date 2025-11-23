

using ShopApp.Domain.Models.Employees.EmployeesBaseModel;

namespace ShopApp.Domain.Models.Employees
{
    public record EmployeesGetModel : EmployeesModel
    {
        public DateTime creation_date { get; set; }
        public int creation_user { get; set; }
    }
}
