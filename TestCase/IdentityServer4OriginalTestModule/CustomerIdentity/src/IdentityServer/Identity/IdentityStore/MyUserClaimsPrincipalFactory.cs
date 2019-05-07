using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.Identity.IdentityStore.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace IdentityServer.Identity.IdentityStore
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<MyUser, MyRole>
    {
        public MyUserClaimsPrincipalFactory(
            UserManager<MyUser> userManager,
            RoleManager<MyRole> roleManager,
            IOptions<IdentityOptions> options)
            : base(
                  userManager,
                  roleManager,
                  options)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(MyUser user)
        {
            var principal = await base.CreateAsync(user);

           
                principal.Identities
                    .First()
                    .AddClaim(new Claim("mycustomerdes", user.MyCustomerDes));
            

            return principal;
        }
    }
}
