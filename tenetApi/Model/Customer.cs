using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tenet.Api.Model;

namespace tenetModel.Model
{
    public class Customer
    {
        [Key]
        public long CustomerID { get; set; }
        public long UserID { get; set; }
        [MaxLength(30)]
        public string CustomerFirstName { get; set; }
        [MaxLength(30)]
        public string CustomerLastName { get; set; }
        public int Telephone { get; set; }
        public int CellPhone { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        public bool IsDeleted{ get; set; }

        public List<CustomerAddress> custAdresFk {  get; set; }
        public User userFk { get; set; }
    }
}
