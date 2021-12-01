using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tenetApi.Model
{
    public class ShopCategory : ModelBase
    {
        [Key]
        public long ShopCategoryID { get; set; }
        [MaxLength(20)]
        public string ShopCategoryTitle { get; set; }
        [MaxLength(300)]
        public string ShopCategoryDescription { get; set; }

        public List<Shop> shopFk { get; set; }
    }
}
