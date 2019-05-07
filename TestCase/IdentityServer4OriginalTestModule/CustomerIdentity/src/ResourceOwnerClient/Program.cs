// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResourceOwnerClient
{
    public class Program
    {
        private static async Task Main()
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                
                return;
            }

            // request token
            var requestpwd = new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",

                UserName = "alice",
                Password = "password",

                Scope = "api1 " + "api2 openid profile offline_access"



                //                Scope = "api1 api2"
            };
            requestpwd.Parameters.Add("quarrierKey","LL");
            var tokenResponse = await client.RequestPasswordTokenAsync(requestpwd);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");
            Console.WriteLine("accesstoken: "+tokenResponse.AccessToken);
            Console.WriteLine("RefreshToken: "+tokenResponse.RefreshToken);
            Console.WriteLine("IdentityToken: "+tokenResponse.IdentityToken);

            //calluserinfo
            var userinfoclient = new HttpClient();
            userinfoclient.SetBearerToken(tokenResponse.AccessToken);
             var userinforesponse = await userinfoclient.GetAsync(disco.UserInfoEndpoint);
            
            Console.WriteLine("UsernInfo: "+userinforesponse.Content.ReadAsStringAsync().Result);


            var refreshToken = await client.RequestRefreshTokenAsync(
                new RefreshTokenRequest()
                {
                    Address = disco.TokenEndpoint,
//                    ClientId = "ro.client",
                    RefreshToken = tokenResponse.RefreshToken,
                    Scope = "api1 " + "api2 openid profile offline_access"
                });
            


            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(JArray.Parse(content));
            }



        }
    }
}