using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazingPizza.Client
{
    public class ServerAuthenticationStateProvider : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Currently, this returns fake data
            // In a moment, we'll get real data from the server
            var claim = new Claim(ClaimTypes.Name, "Fake user");
            var identity = new ClaimsIdentity(new[] { claim }, "serverauth");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }
    }
}
