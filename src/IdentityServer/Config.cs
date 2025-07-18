using System.Collections.Generic;
using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("auctionApp", "Auction app full access"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client{
                ClientId = "postman",
                ClientName = "postman",
                AllowedScopes= {"auctionApp", "openid", "profile"},
                RedirectUris= {"https://www.getpostman.com/oauth2/callback"},
                ClientSecrets = new[]{new Secret("NotASecret".Sha256())},
                AllowedGrantTypes= {GrantType.ResourceOwnerPassword} 
            },
            new Client{
                ClientId = "nextApp",
                ClientName = "nextApp",
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedGrantTypes= GrantTypes.CodeAndClientCredentials ,
                RequirePkce = false,
                RedirectUris = {
                    "http://localhost:3000/api/auth/callback/id-server"
                },
                PostLogoutRedirectUris = { "http://localhost:3000/" },
                AllowOfflineAccess = true,
                AllowedScopes= {"auctionApp", "openid", "profile"},
                AccessTokenLifetime = 3600*24*30,
                AlwaysIncludeUserClaimsInIdToken = true
            }
        };
}
