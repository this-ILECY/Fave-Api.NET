using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tenetApi.Model
{
    public class Promotion : ModelBase
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
        public DateTime StartDate { get; set; }
        public bool IsActive{ get; set; }

        public Product productFk { get; set; }
        public Shop shopFk { get; set; }
    }
}
