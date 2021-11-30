using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tenetModel.Model;

namespace tenetApi.Model
{
    public class Product
    {
        [Key]
        public long ProductID { get; set; }
        public long ShopID { get; set; }
        public long ProductCategoryID { get; set; }
        public long ProductCode{ get; set; }
        [MaxLength(30)]
        public string ProductTitle { get; set; }
        public string description{ get; set; }
        public bool IsDeleted{ get; set; }

        public Shop shopFk { get; set; }
        public List<Promotion> promotionFk { get; set; }
        public ProductCategory productCategoryFk { get; set; }
    }
}
