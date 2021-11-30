using Microsoft.AspNetCore.Identity;
using System;
using tenetModel.Model;

namespace tenet.Api.Model
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
