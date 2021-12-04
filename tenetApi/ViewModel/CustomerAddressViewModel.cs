using tenetApi.Model;

namespace tenetApi.ViewModel
{
    public class CustomerAddressViewModel :ModelBase

    {
        public long CustomerAddressID { get; set; }
        public long CustomerID { get; set; }
        public string AddressTitle { get; set; }
        public decimal CustomerLatitude { get; set; }
        public decimal CustomerLongitude { get; set; }
    }
}
