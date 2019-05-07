using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;
using Volo.Abp.Uow;
using ApiResource = Volo.Abp.IdentityServer.ApiResources.ApiResource;
using Client = Volo.Abp.IdentityServer.Clients.Client;
using IdentityResource = Volo.Abp.IdentityServer.IdentityResources.IdentityResource;

namespace IfcaIdentityServerHost
{
    public class IdentityServerDataSeeder : ITransientDependency
    {
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IIdentityResourceRepository _identityResourceRepository;
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly IdentityUserManager _identityUserManager;


        private readonly string _testUser = "Quarrier-TestUser";
        private readonly string _testPwd = "Abc123$";

        public IdentityServerDataSeeder(
            IApiResourceRepository apiResourceRepository,
            IClientRepository clientRepository,
            IIdentityResourceRepository identityResourceRepository,
            IIdentityUserRepository identityUserRepository,
            IdentityUserManager identityUserManager
            )
        {
            _apiResourceRepository = apiResourceRepository;
            _clientRepository = clientRepository;
            _identityResourceRepository = identityResourceRepository;
            _identityUserRepository = identityUserRepository;
            GuidGenerator = SimpleGuidGenerator.Instance;
            _identityUserManager = identityUserManager;
        }

        public IGuidGenerator GuidGenerator { get; set; }

        public void Seed()
        {
            AsyncHelper.RunSync(SeedIdentityServerAsync);
            AsyncHelper.RunSync(SeedIdentityAsync);
        }

        private async Task SeedIdentityAsync()
        {
            if (await _identityUserRepository.FindByNormalizedUserNameAsync(
                    _testUser) != null)
            {
                return;
            }

            await SeedIdentityTestUserAsync();



        }

        private async Task SeedIdentityTestUserAsync()
        {
            var quarrier = new IdentityUser(GuidGenerator.Create(), _testUser,
                "zhangzhe@ifca.com.cn");
            
            quarrier.AddClaims(GuidGenerator,
                new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "quarrier zhang"),
                    new Claim(JwtClaimTypes.GivenName, "quarrier"),
                    new Claim(JwtClaimTypes.FamilyName, "Zahng"),
                    new Claim(JwtClaimTypes.Email, "quarrier@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true",
                        ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://quarrier.com"),
                    new Claim(JwtClaimTypes.Address,
                        @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }",
                        IdentityServer4.IdentityServerConstants.ClaimValueTypes
                            .Json)
                });
            var quarrIdentityResult =
                await _identityUserRepository.InsertAsync(quarrier);

            await _identityUserManager.AddPasswordAsync(quarrIdentityResult, _testPwd);


            Console.WriteLine("TestUser created");
        }

        private async Task SeedIdentityServerAsync()
        {
            if (await _clientRepository.FindByCliendIdAsync("Mvc-Testclient") != null)
            {
                return;
            }

            await SaveApiResource();
            await SaveMvcTestClientAsync();
            await SaveJsTestClientAsync();
            await SaveIdentityResourcesAsync();
        }

        private async Task SaveApiResource()
        {
            var apiResource = new ApiResource(
                Guid.NewGuid(),
                "api1",
                "My API",
                "My api resource description"
            );

            apiResource.AddUserClaim("email");
            apiResource.AddUserClaim("role");

            var apiResource2 = new ApiResource(
                Guid.NewGuid(),
                "api2",
                "My API2",
                "My api2 resource description"
            );

            apiResource2.AddUserClaim("email");
            apiResource2.AddUserClaim("role");
            apiResource2.AddUserClaim(JwtClaimTypes.WebSite);


            await _apiResourceRepository.InsertAsync(apiResource);
            await _apiResourceRepository.InsertAsync(apiResource2);

        }

        private async Task SaveMvcTestClientAsync()
        {
            var client = new Client(
                Guid.NewGuid(),
                "Mvc-Testclient"
            )
            {
                ClientName = "Mvc-Testclient",
                ProtocolType = "oidc",
                Description = "test-client",
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true,

                AbsoluteRefreshTokenLifetime = 31536000 //365 days

            };

            client.AddScope("api1");
            client.AddScope("api2");
            client.AddScope("roles");
            client.AddScope(IdentityServerConstants.StandardScopes.OpenId);
            client.AddScope(IdentityServerConstants.StandardScopes.Profile);

            client.AddScope("unique_name");

//            client.AddGrantType("client_credentials");
//            client.AddGrantType("password");
            client.AddGrantType(GrantType.Hybrid);

//            client.AddSecret("K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=");
            client.AddSecret("abc".Sha256());

            client.AddRedirectUri("http://localhost:5002/signin-oidc");
            client.AddPostLogoutRedirectUri(
                "http://localhost:5002/signout-callback-oidc");
            client.AddCorsOrigin("http://localhost:5002"); 


            await _clientRepository.InsertAsync(client);
        }

        private async Task SaveJsTestClientAsync()
        {
            var client = new Client(
                Guid.NewGuid(),
                "Js-Testclient"
            )
            {
                ClientName = "Js-Testclient",
                ProtocolType = "oidc",
                Description = "test-client",
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true,
                AllowAccessTokensViaBrowser = true,
                RequirePkce = true,
            RequireClientSecret = false,
            AbsoluteRefreshTokenLifetime = 31536000 //365 days

            };

            client.AddScope("api1");
            client.AddScope("api2");
            client.AddScope("roles");
            client.AddScope(IdentityServerConstants.StandardScopes.OpenId);
            client.AddScope(IdentityServerConstants.StandardScopes.Profile);

            client.AddScope("unique_name");

            //            client.AddGrantType("client_credentials");
            //            client.AddGrantType("password");
            client.AddGrantType(GrantType.Implicit);

            //            client.AddSecret("K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=");
            client.AddSecret("abc".Sha256());

            

            client.AddRedirectUri("http://localhost:5003/callback.html"); 
            client.AddPostLogoutRedirectUri("http://localhost:5003/index.html");
            client.AddCorsOrigin("http://localhost:5003");   
            
            await _clientRepository.InsertAsync(client);
        }

        private async Task SaveIdentityResourcesAsync()
        {
            var identityResourceOpenId = new IdentityResource(Guid.NewGuid(), "openid", "OpenID", required: true);
            await _identityResourceRepository.InsertAsync(identityResourceOpenId);

            var identityResourceEmail = new IdentityResource(Guid.NewGuid(), "email", "Email", required: true);
            identityResourceEmail.AddUserClaim("email");
            identityResourceEmail.AddUserClaim("email_verified");
            await _identityResourceRepository.InsertAsync(identityResourceEmail);

            var identityResourceRole = new IdentityResource(Guid.NewGuid(), "roles", "Roles", required: true);
            identityResourceRole.AddUserClaim("role");
            await _identityResourceRepository.InsertAsync(identityResourceRole);

            var identityResourceProfile = new IdentityResource(Guid.NewGuid(), "profile", "Profile", required: true);
            identityResourceProfile.AddUserClaim("unique_name");
            await _identityResourceRepository.InsertAsync(identityResourceProfile);
        }
    }
}
