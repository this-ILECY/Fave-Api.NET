﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace tenetApi.Model
{
    public class Customer :ModelBase
    {
        [Key]
        public long CustomerID { get; set; }
        public long UserID { get; set; }
        [MaxLength(30)]
        public string CustomerFirstName { get; set; }
        [MaxLength(30)]
        public string CustomerLastName { get; set; }
        public string Telephone { get; set; }
        public string CellPhone { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        public bool IsActive{ get; set; }

        public List<CustomerAddress> custAdresFk {  get; set; }
        public User userFk { get; set; }
    }
}
