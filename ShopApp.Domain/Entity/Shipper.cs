

namespace ShopApp.Domain.Entity 
{
    public partial class Shipper : BaseEntity.BaseEntity
    {
        public int Shipperid { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string Postalcode { get; set; }

        public int? Country { get; set; }


    }
}

 