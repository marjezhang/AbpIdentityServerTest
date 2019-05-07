using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Identity.IdentityStore.Entity;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Identity.IdentityStore
{
    /// <summary>
    /// 可以获取用户其他信息， 多租户有用，  因为有个profile的identity保护资源
    /// </summary>
    public class MyProfileService : ProfileService<MyUser>
    {
        public MyProfileService(UserManager<MyUser> userManager,
            IUserClaimsPrincipalFactory<MyUser> claimsFactory) : base(
            userManager, claimsFactory)
        {
        }

        public MyProfileService(UserManager<MyUser> userManager,
            IUserClaimsPrincipalFactory<MyUser> claimsFactory,
            ILogger<ProfileService<MyUser>> logger) : base(userManager,
            claimsFactory, logger)
        {
        }

        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            return base.GetProfileDataAsync(context);
        }

        public override Task IsActiveAsync(IsActiveContext context)
        {
            return base.IsActiveAsync(context);
        }
    }
}
