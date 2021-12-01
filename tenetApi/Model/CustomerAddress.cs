using System.ComponentModel.DataAnnotations;

namespace tenetApi.Model
{
    public class CustomerAddress:ModelBase
    {
        [Key]
        public long CustomerAddressID { get; set; }
        public long CustomerID { get; set; }
        [MaxLength(20)]
        public string AddressTitle { get; set; }
        public decimal CustomerLatitude { get; set; }
        public decimal CustomerLongitude { get; set; }


        public Customer customerFk {  get; set; }

    }
}
