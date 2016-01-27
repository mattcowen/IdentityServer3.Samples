using System;
using IdentityServer3.Core.Configuration;
using Owin;
using SelfHost.Config;

namespace SelfHost
{
    internal class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var factory = new IdentityServerServiceFactory();
            factory
                .UseInMemoryClients(Clients.Get())
                .UseInMemoryScopes(Scopes.Get())
                .UseInMemoryUsers(Users.Get());

            var options = new IdentityServerOptions
            {
                SiteName = "IdentityServer3 (self host)",
                SigningCertificate = Certificate.Get(),
                Factory = factory,
            };

            options.AuthenticationOptions.CookieOptions.SlidingExpiration = true;
            options.AuthenticationOptions.CookieOptions.ExpireTimeSpan = TimeSpan.FromMinutes(60);

            appBuilder.UseIdentityServer(options);
        }
    }
}