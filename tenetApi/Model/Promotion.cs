using System;
using System.ComponentModel.DataAnnotations;

namespace tenetApi.Model
{
    public class Promotion
    {
        [Key]
        public long PromotionID { get; set; }
        public long ProductID { get; set; }
        public long ShopID { get; set; }
        public long BasePrice { get; set; }
        public long DiscountPrice { get; set; }
        public int Stock { get; set; }
        public int QualityGrade { get; set; }
        public DateTime EndDate { get; set; }
        [MaxLength(30)]
        public string EndTime { get; set; }
        public bool IsActive{ get; set; }
        public bool IsDeleted{ get; set; }

        public Product productFk { get; set; }
    }
}
