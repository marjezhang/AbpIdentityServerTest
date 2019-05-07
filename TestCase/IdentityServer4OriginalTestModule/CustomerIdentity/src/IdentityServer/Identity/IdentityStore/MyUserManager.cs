using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Identity.IdentityStore.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityServer.Identity.IdentityStore
{
    public class MyUserManager : UserManager<MyUser>
    {
        public MyUserManager(IUserStore<MyUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<MyUser> passwordHasher,
            IEnumerable<IUserValidator<MyUser>> userValidators,
            IEnumerable<IPasswordValidator<MyUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services, ILogger<UserManager<MyUser>> logger) :
            base(store, optionsAccessor, passwordHasher, userValidators,
                passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}
