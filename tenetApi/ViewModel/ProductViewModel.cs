using tenetApi.Model;

namespace tenetApi.ViewModel
{
    public class ProductViewModel : ModelBase
    {
        public long ProductID { get; set; }
        public long ShopID { get; set; }
        public long ProductCategoryID { get; set; }
        public long ProductCode { get; set; }
        public string ProductTitle { get; set; }
        public string description { get; set; } 
    }
}
