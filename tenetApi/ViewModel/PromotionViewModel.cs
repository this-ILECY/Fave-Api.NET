using System;
using tenetApi.Model;

namespace tenetApi.ViewModel
{
    public class PromotionViewModel : ModelBase
    {
        public long ProductID { get; set; }
        public long ShopID { get; set; }
        public long PromotionID { get; set; }
        public string ProductTitle { get; set; }
        public long DiscountPercent { get; set; }
        public long DiscountPrice { get; set; }
        public long BasePrice { get; set; }
        public DateTime RemainingTime { get; set; }
        public int QualityGrade { get; set; }
        public int Stock { get; set; }
        public string ShopName { get; set; }
        public string productDescription { get; set; }
        public string ShopAddress { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }
    }
}
