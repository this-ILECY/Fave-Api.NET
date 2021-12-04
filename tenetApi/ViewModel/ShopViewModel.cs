using tenetApi.Model;

namespace tenetApi.ViewModel
{
    public class ShopViewModel :ModelBase
    {
        public long ShopID { get; set; }
        public long UserID { get; set; }
        public long ShopCategoryID { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public int TelePhone { get; set; }
        public int CellPhone { get; set; }
        public decimal ShopLatitude { get; set; }
        public decimal ShopLongitude { get; set; }
        public bool IsActive { get; set; }

    }
}
