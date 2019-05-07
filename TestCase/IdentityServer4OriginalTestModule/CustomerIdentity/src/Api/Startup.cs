// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddAuthorization(a =>
                {
                    a.AddPolicy("zz",
                        policy =>
//                            policy.AddRequirements(new NameAuthorizationRequirement())
                            policy.RequireClaim(
                                "http://schemas.microsoft.com/identity/claims/identityprovider"));
                })
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "api1";
                });

            //test

            services
                .AddSingleton<IAuthorizationHandler, My2AuthorizationHandler>();
                
            services.AddSingleton<IAuthorizationHandler, My3AuthorizationHandler>();
            services.AddSingleton<InterfaceMy, Myclass1>();
            services.AddSingleton<InterfaceMy, MyClass2>();
            services.AddSingleton<ClassQuarrier>();
                
            

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5003",
                            "http://localhost:5002")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("default");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}