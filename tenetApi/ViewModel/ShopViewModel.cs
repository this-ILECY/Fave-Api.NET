﻿using tenetApi.Model;

namespace tenetApi.ViewModel
{
    public class ShopViewModel : ModelBase
    {
        public long ShopID { get; set; }
        public long UserID { get; set; }
        public long ShopCategoryID { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string TelePhone { get; set; }
        public string CellPhone { get; set; }
        public string ShopAvatar { get; set; }
        public string ShopBanner { get; set; }
        public decimal ShopLatitude { get; set; }
        public decimal ShopLongitude { get; set; }
        public bool IsActive { get; set; }

    }





    public class InvoiceChartViewModel
    {
        public int[] barChartLabels { get; set; }
        public int[] data { get; set; }
        public string[] backgroundColor { get; set; }
    }

}
