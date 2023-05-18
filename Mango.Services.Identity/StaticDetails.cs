using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Mango.Services.Identity
{
	public static class StaticDetails
	{
		public static string Admin { get; set; } = "Admin";
		public static string User { get; set; } = "User";

		public static IEnumerable<IdentityResource> Resources =>
			new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Email(),
				new IdentityResources.Profile(),
			};
		public static IEnumerable<ApiScope> Scopes =>
			new List<ApiScope>
			{
				new ApiScope("mango", "Mango Server"),
				new ApiScope("read", "Read your data."),
				new ApiScope("write", "Write your data."),
				new ApiScope("delete", "Delete your data."),
			};

        public static IEnumerable<Client> GetClients(string clientSecret)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret(clientSecret.Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "read", "write", "profile" }
                },
                new Client
                {
                    ClientId = "mango",
                    ClientSecrets = { new Secret(clientSecret.Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:7129/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7129/signout-callback-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "mango"
                    }
                }
            };
        }
    }
}
