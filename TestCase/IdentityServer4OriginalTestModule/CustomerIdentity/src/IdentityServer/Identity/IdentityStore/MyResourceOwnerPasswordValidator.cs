using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer.Identity.IdentityStore.Entity;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;

namespace IdentityServer.Identity.IdentityStore
{
    public class
        MyResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly MyUserStore _users;
        private readonly ISystemClock _clock;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestUserResourceOwnerPasswordValidator"/> class.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="clock">The clock.</param>
        public MyResourceOwnerPasswordValidator(MyUserStore users, ISystemClock clock)
        {
            _users = users;
            _clock = clock;
        }

        /// <summary>
        /// Validates the resource owner password credential
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (_users.ValidateCredentials(context.UserName, context.Password))
            {
                Debug.Print(context.Request.Raw["quarrierKey"]);
                var user = _users.FindByUsername(context.UserName);
                context.Result = new GrantValidationResult(
                    user.SubjectId ?? throw new ArgumentException("Subject ID not set", nameof(user.SubjectId)),
                    OidcConstants.AuthenticationMethods.Password, _clock.UtcNow.UtcDateTime,
                    user.Claims);
            }

            return Task.CompletedTask;
        }
    }
}
