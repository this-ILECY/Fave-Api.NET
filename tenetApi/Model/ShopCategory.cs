using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tenetModel.Model;

namespace tenetApi.Model
{
    public class ShopCategory
    {
        [Key]
        public long ShopCategoryID { get; set; }
        [MaxLength(20)]
        public string ShopCategoryTitle { get; set; }
        [MaxLength(300)]
        public string ShopCategoryDescription { get; set; }
        public bool IsDeleted { get; set; }

        public List<Shop> shopFk { get; set; }
    }
}
