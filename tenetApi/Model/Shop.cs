using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tenetApi.Model
{
    public class Shop : ModelBase
    {
        [Key]
        public long ShopID { get; set; }
        public long UserID { get; set; }
        [MaxLength(30)]
        public string ShopName { get; set; }
        [MaxLength(300)]
        public string ShopAddress { get; set; }
        public int TelePhone { get; set; }
        public int CellPhone { get; set; }
        public decimal ShopLatitude { get; set; }
        public decimal ShopLongitude { get; set; }
        public bool IsActive { get; set; }

        public User userFk { get; set; }
        public List<Product> productFk { get; set; }
        public ShopCategory shopCategoryFk { get; set; }
    }
}
