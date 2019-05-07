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
    public class MyUserStore :
      IUserLoginStore<MyUser>,
        IUserRoleStore<MyUser>,
        IUserClaimStore<MyUser>,
        IUserPasswordStore<MyUser>,
        IUserSecurityStampStore<MyUser>,
        IUserEmailStore<MyUser>,
        IUserLockoutStore<MyUser>,
        IUserPhoneNumberStore<MyUser>,
        IUserTwoFactorStore<MyUser>,
        IUserAuthenticationTokenStore<MyUser>,
        IUserAuthenticatorKeyStore<MyUser>,
        IUserTwoFactorRecoveryCodeStore<MyUser>

    {
        private readonly List<MyUser> _myUsers;

        public MyUserStore()
        {
            _myUsers = Config.GetMyUsers();
        }

        public MyUser FindByUsername(string username)
        {
            return _myUsers.FirstOrDefault(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public bool ValidateCredentials(string username, string password)
        {
            var user = _myUsers.First(m=>m.Username==username);
            if (user != null)
            {
                return user.Password.Equals(password);
            }

            return false;
        }

        public void Dispose()
        {
            
        }

        public async Task<string> GetUserIdAsync(MyUser user, CancellationToken cancellationToken)
        {
            return user.SubjectId;
        }

        public async Task<string> GetUserNameAsync(MyUser user, CancellationToken cancellationToken)
        {
            return user.Username;
        }

        public Task SetUserNameAsync(MyUser user, string userName,
            CancellationToken cancellationToken)
        {
            user.Username = userName;
            return Task.CompletedTask;
        }

        public async Task<string> GetNormalizedUserNameAsync(MyUser user,
            CancellationToken cancellationToken)
        {
            return user.Username;
        }

        public Task SetNormalizedUserNameAsync(MyUser user, string normalizedName,
            CancellationToken cancellationToken)
        {
            user.Username = normalizedName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> CreateAsync(MyUser user, CancellationToken cancellationToken)
        {
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(MyUser user, CancellationToken cancellationToken)
        {
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(MyUser user, CancellationToken cancellationToken)
        {
            return IdentityResult.Success;
        }

        public async Task<MyUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {

            var user = _myUsers.FirstOrDefault(u => u.SubjectId == userId);
            return user;
        }

        public async Task<MyUser> FindByNameAsync(string normalizedUserName,
            CancellationToken cancellationToken)
        {
            var user =_myUsers.FirstOrDefault(u => u.Username == normalizedUserName);
            return user;
        }

        public Task AddLoginAsync(MyUser user, UserLoginInfo login,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task RemoveLoginAsync(MyUser user, string loginProvider, string providerKey,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(MyUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<MyUser> FindByLoginAsync(string loginProvider, string providerKey,
            CancellationToken cancellationToken)
        {
            return _myUsers.FirstOrDefault();
        }

        public Task AddToRoleAsync(MyUser user, string roleName,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task RemoveFromRoleAsync(MyUser user, string roleName,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<IList<string>> GetRolesAsync(MyUser user, CancellationToken cancellationToken)
        {
            return new[] {"quarrierrole"};
        }

        public async Task<bool> IsInRoleAsync(MyUser user, string roleName,
            CancellationToken cancellationToken)
        {
            return true;
        }

        public async Task<IList<MyUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return _myUsers;
        }

        public async Task<IList<Claim>> GetClaimsAsync(MyUser user, CancellationToken cancellationToken)
        {
            return user.Claims.ToList();
        }

        public Task AddClaimsAsync(MyUser user, IEnumerable<Claim> claims,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task ReplaceClaimAsync(MyUser user, Claim claim, Claim newClaim,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task RemoveClaimsAsync(MyUser user, IEnumerable<Claim> claims,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<IList<MyUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            return _myUsers;
        }

        public Task SetPasswordHashAsync(MyUser user, string passwordHash,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<string> GetPasswordHashAsync(MyUser user, CancellationToken cancellationToken)
        {
            return user.Password;
        }

        public async Task<bool> HasPasswordAsync(MyUser user, CancellationToken cancellationToken)
        {
            return true;
        }

        public Task SetSecurityStampAsync(MyUser user, string stamp,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<string> GetSecurityStampAsync(MyUser user, CancellationToken cancellationToken)
        {
            return "";
        }

        public Task SetEmailAsync(MyUser user, string email,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<string> GetEmailAsync(MyUser user, CancellationToken cancellationToken)
        {
            return "quarrier.zhang@ifca.com.cn";
        }

        public async Task<bool> GetEmailConfirmedAsync(MyUser user, CancellationToken cancellationToken)
        {
            return true;
        }

        public Task SetEmailConfirmedAsync(MyUser user, bool confirmed,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<MyUser> FindByEmailAsync(string normalizedEmail,
            CancellationToken cancellationToken)
        {
            return _myUsers.FirstOrDefault();
        }

        public async Task<string> GetNormalizedEmailAsync(MyUser user, CancellationToken cancellationToken)
        {
            return "quarrier.zhang@ifca.com.cn";
        }

        public Task SetNormalizedEmailAsync(MyUser user, string normalizedEmail,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(MyUser user, CancellationToken cancellationToken)
        {

            return null;
        }

        public Task SetLockoutEndDateAsync(MyUser user, DateTimeOffset? lockoutEnd,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<int> IncrementAccessFailedCountAsync(MyUser user,
            CancellationToken cancellationToken)
        {
            return 0;
        }

        public Task ResetAccessFailedCountAsync(MyUser user,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<int> GetAccessFailedCountAsync(MyUser user, CancellationToken cancellationToken)
        {
            return 0;
        }

        public async Task<bool> GetLockoutEnabledAsync(MyUser user, CancellationToken cancellationToken)
        {
            return true;
        }

        public Task SetLockoutEnabledAsync(MyUser user, bool enabled,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetPhoneNumberAsync(MyUser user, string phoneNumber,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<string> GetPhoneNumberAsync(MyUser user, CancellationToken cancellationToken)
        {
            return "";
        }

        public async Task<bool> GetPhoneNumberConfirmedAsync(MyUser user,
            CancellationToken cancellationToken)
        {
            return true;
        }

        public Task SetPhoneNumberConfirmedAsync(MyUser user, bool confirmed,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetTwoFactorEnabledAsync(MyUser user, bool enabled,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<bool> GetTwoFactorEnabledAsync(MyUser user, CancellationToken cancellationToken)
        {
            return true;
        }

        public Task SetTokenAsync(MyUser user, string loginProvider, string name, string value,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task RemoveTokenAsync(MyUser user, string loginProvider, string name,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<string> GetTokenAsync(MyUser user, string loginProvider, string name,
            CancellationToken cancellationToken)
        {
            return "";
        }

        public Task SetAuthenticatorKeyAsync(MyUser user, string key,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<string> GetAuthenticatorKeyAsync(MyUser user, CancellationToken cancellationToken)
        {
            return "";
        }

        public Task ReplaceCodesAsync(MyUser user, IEnumerable<string> recoveryCodes,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<bool> RedeemCodeAsync(MyUser user, string code,
            CancellationToken cancellationToken)
        {
            return false;
        }

        public async Task<int> CountCodesAsync(MyUser user, CancellationToken cancellationToken)
        {
            return 0;
        }
    }
}
