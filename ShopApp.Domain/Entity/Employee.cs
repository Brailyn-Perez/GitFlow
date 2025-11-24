

namespace ShopApp.Domain.Entity 
{
    public sealed class Employee : BaseEntity.BaseEntity
    {
        public int Empid { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public string Title { get; set; }

        public string Titleofcourtesy { get; set; }

        public DateTime Birthdate { get; set; }

        public DateTime Hiredate { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string Postalcode { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public int? Mgrid { get; set; }


    }
}
 
