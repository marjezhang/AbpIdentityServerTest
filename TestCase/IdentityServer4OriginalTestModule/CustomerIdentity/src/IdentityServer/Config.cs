// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer.Identity.IdentityStore.Entity;

namespace IdentityServer
{
    public static class Config
    {
        public static List<MyUser> GetMyUsers()
        {
            return new List<MyUser>
            {
                new MyUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",
                    MyCustomerDes = "i am do myself",

                    Claims = new []
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com"),
                        new Claim(JwtClaimTypes.NickName,"TestMyServerClaims"), 
                        new Claim("quarrierClaims1","myClaimInUser1"),
                        new Claim("quarrierClaims2","myClaimInUser2"),
                    }
                },
                new MyUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",
                    MyCustomerDes = "i am do myself",

                    Claims = new []
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com")
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com"),
                        new Claim(JwtClaimTypes.NickName,"TestMyServerClaims"), 
                        new Claim("quarrierClaims1","myClaimInUser1"), 
                        new Claim("quarrierClaims2","myClaimInUser2"), 
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API" ,new []
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                }),
                new ApiResource("api2", "My API2",new []{"quarrierClaims2"})
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = {"api1"}
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AccessTokenLifetime = 1800,//设置AccessToken过期时间
                    
                    //RefreshTokenExpiration = TokenExpiration.Absolute,//刷新令牌将在固定时间点到期
                    AbsoluteRefreshTokenLifetime = 2592000,//RefreshToken的最长生命周期,默认30天
                    RefreshTokenExpiration = TokenExpiration.Sliding,//刷新令牌时，将刷新RefreshToken的生命周期。RefreshToken的总生命周期不会超过AbsoluteRefreshTokenLifetime。
                    SlidingRefreshTokenLifetime = 3600,//以秒为单位滑动刷新令牌的生命周期。
                    //按照现有的设置，如果3600内没有使用RefreshToken，那么RefreshToken将失效。即便是在3600内一直有使用RefreshToken，RefreshToken的总生命周期不会超过30天。所有的时间都可以按实际需求调整。

                    AllowOfflineAccess = true,//如果要获取refresh_tokens ,必须把AllowOfflineAccess设置为true

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "api1","api2"},
                    AlwaysSendClientClaims = true,
                     Claims = new[] {new Claim("ClientClaimType1", "client1"),
                        new Claim("ClientClaimType2", "client2"),}


                },
                // OpenID Connect hybrid flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = {"http://localhost:5002/signin-oidc"},
                    PostLogoutRedirectUris =
                        {"http://localhost:5002/signout-callback-oidc"},

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1", "api2"
                    },
                    AlwaysSendClientClaims = true,

                    Claims = new[] {new Claim("ClientClaimType1", "client1"),
                        new Claim("ClientClaimType2", "client2"),},

                    AllowOfflineAccess = true
                },
                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = {"http://localhost:5003/callback.html"},
                    PostLogoutRedirectUris =
                        {"http://localhost:5003/index.html"},
                    AllowedCorsOrigins = {"http://localhost:5003"},

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                }
            };
        }
    }
}