using System;
using tenetApi.Model;

namespace tenetApi.ViewModel
{
    public class PromotionViewModel : ModelBase
    {
        public long PromotionID { get; set; }
        public long ProductID { get; set; }
        public long ShopID { get; set; }
        public long BasePrice { get; set; }
        public long DiscountPrice { get; set; }
        public int Stock { get; set; }
        public int QualityGrade { get; set; }
        public DateTime EndDate { get; set; }
        public string EndTime { get; set; }
        public bool IsActive { get; set; }
    }
}
