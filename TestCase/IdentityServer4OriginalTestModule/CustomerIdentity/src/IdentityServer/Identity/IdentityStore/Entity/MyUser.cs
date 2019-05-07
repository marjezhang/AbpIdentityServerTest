using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Test;

namespace IdentityServer.Identity.IdentityStore.Entity
{
    public class MyUser : TestUser
    {
        public string MyCustomerDes { get; set; }

    }
}
