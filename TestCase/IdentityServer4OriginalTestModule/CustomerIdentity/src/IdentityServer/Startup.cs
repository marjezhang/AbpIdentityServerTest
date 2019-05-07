// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer.Identity.IdentityStore;
using IdentityServer.Identity.IdentityStore.Entity;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc
                .CompatibilityVersion.Version_2_1);

            AddIdentityMyself(services);

            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients());
//                .AddTestUsers(Config.GetUsers());

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            UpdateIdentityServerBuilder(builder);
            
           
            services.AddAuthentication(o =>
                {
//                    o.DefaultScheme = "Cookies";
//                    o.DefaultChallengeScheme = "oidc";
                })
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants
                        .ExternalCookieAuthenticationScheme;

                    options.ClientId = "<insert here>";
                    options.ClientSecret = "<insert here>";
                })
                .AddOpenIdConnect("oidc", "OpenID Connect", options =>
                {
                    options.SignInScheme = IdentityServerConstants
                        .ExternalCookieAuthenticationScheme;
                    options.SignOutScheme =
                        IdentityServerConstants.SignoutScheme;
                    options.SaveTokens = true;

                    options.Authority = "https://demo.identityserver.io/";
                    options.ClientId = "implicit";

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            NameClaimType = "name",
                            RoleClaimType = "role"
                        };
                });

        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();
        }

        private void UpdateIdentityServerBuilder(IIdentityServerBuilder builder)
        {
            //ResourceOwnerPwd方式 需要加入验证接口???
            builder.AddAspNetIdentity<MyUser>();
            builder.AddProfileService<MyProfileService>();
            builder.AddResourceOwnerValidator<MyResourceOwnerPasswordValidator>();

        }


        private void AddIdentityMyself(IServiceCollection services)
        {

           
            //AbpRoleManager
            services.TryAddScoped<MyRoleManager>();
            services.TryAddScoped(typeof(RoleManager<MyRole>), provider => provider.GetService(typeof(MyRoleManager)));

            //AbpUserManager
            services.TryAddScoped<MyUserManager>();
            services.TryAddScoped(typeof(UserManager<MyUser>), provider => provider.GetService(typeof(MyUserManager)));

            //AbpUserStore
            services.TryAddScoped<MyUserStore>();
            services.TryAddScoped(typeof(IUserStore<MyUser>), provider => provider.GetService(typeof(MyUserStore)));

            //AbpRoleStore
            services.TryAddScoped<MyRoleStore>();
            services.TryAddScoped(typeof(IRoleStore<MyRole>), provider => provider.GetService(typeof(MyRoleStore)));


            //这里用addcore  和addidentity区别到时候测试???
            //测试addidentity   abp有个unique email要求这里不要
//            services.AddIdentityCore<MyUser>().AddRoles<MyRole>()
//                .AddClaimsPrincipalFactory<MyUserClaimsPrincipalFactory>();
//                .AddDefaultTokenProviders();
            services.AddIdentity<MyUser, MyRole>()
                .AddClaimsPrincipalFactory<MyUserClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();



        }
    }
}