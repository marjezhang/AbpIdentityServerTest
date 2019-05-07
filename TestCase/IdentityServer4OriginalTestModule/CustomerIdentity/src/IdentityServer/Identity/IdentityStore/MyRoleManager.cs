using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Identity.IdentityStore.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Identity.IdentityStore
{
    public class MyRoleManager : RoleManager<MyRole>
    {
        public MyRoleManager(IRoleStore<MyRole> store,
            IEnumerable<IRoleValidator<MyRole>> roleValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            ILogger<RoleManager<MyRole>> logger) : base(store, roleValidators,
            keyNormalizer, errors, logger)
        {
        }
    }
}
