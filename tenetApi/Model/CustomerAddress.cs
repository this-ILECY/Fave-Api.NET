using System.ComponentModel.DataAnnotations;

namespace tenetModel.Model
{
    public class CustomerAddress
    {
        [Key]
        public long CustomerAddressID { get; set; }
        public long CustomerID { get; set; }
        [MaxLength(20)]
        public string AddressTitle { get; set; }
        public decimal CustomerLatitude { get; set; }
        public decimal CustomerLongitude { get; set; }
        public bool IsDeleted{ get; set; }

        public Customer customerFk {  get; set; }

    }
}
