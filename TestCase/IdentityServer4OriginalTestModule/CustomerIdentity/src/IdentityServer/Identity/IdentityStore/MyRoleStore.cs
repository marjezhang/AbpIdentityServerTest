using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer.Identity.IdentityStore.Entity;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Identity.IdentityStore
{
    public class MyRoleStore : IRoleStore<MyRole>,
        IRoleClaimStore<MyRole>
    {
        public MyRoleStore()
        {
        }

        public void Dispose()
        {
        }

        public Task<IdentityResult> CreateAsync(MyRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(MyRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(MyRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(MyRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(MyRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(MyRole role, string roleName,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(MyRole role,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(MyRole role, string normalizedName,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<MyRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return new MyRole(){Id = "quarrierrole",Name = "quarrierrole"};
        }

        public async Task<MyRole> FindByNameAsync(string normalizedRoleName,
            CancellationToken cancellationToken)
        {
            return new MyRole(){Id = "quarrierrole",Name = "quarrierrole"};
        }

        public async Task<IList<Claim>> GetClaimsAsync(MyRole role,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return new List<Claim>();
        }

        public Task AddClaimAsync(MyRole role, Claim claim,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(MyRole role, Claim claim,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
