﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tenetApi.Model
{
    public class ProductCategory : ModelBase
    {
        [Key]
        public long ProductCategoryID { get; set; }
        [MaxLength(20)]
        public string ProductCategoryTitle { get; set; }
        [MaxLength(300)]
        public string ProductCategoryDescription { get; set; }

        public List<Product> productFk { get; set; }
    }
}
