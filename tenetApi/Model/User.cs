using Microsoft.AspNetCore.Identity;
using System;

namespace tenetApi.Model
{
    public class User: IdentityUser<long>
    {
        public DateTime UserCreatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public Customer customerFk { get; set; }
        public Shop shopFk { get; set; }  

    }
}
