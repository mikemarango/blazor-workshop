using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazingPizza.Client
{
    public class ServerAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;

        public ServerAuthenticationStateProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userInfo = await httpClient.GetJsonAsync<UserInfo>("user");

            var identity = userInfo.IsAuthenticated
                ? new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userInfo.Name) }, "serverauth")
                : new ClaimsIdentity();

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        //public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    // Currently, this returns fake data
        //    // In a moment, we'll get real data from the server
        //    var claim = new Claim(ClaimTypes.Name, "Fake user");
        //    var identity = new ClaimsIdentity(new[] { claim }, "serverauth");
        //    var authState = new AuthenticationState(new ClaimsPrincipal(identity));
        //    return await Task.FromResult(authState);
        //}
    }
}
