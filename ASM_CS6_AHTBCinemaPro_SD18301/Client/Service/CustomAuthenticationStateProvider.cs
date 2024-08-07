namespace ASM_CS6_AHTBCinemaPro_SD18301.Client.Service
{
    // Client/Services/CustomAuthenticationStateProvider.cs

    using Microsoft.AspNetCore.Components.Authorization;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_anonymous));
        }

        public void MarkUserAsAuthenticated(string username, string role)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "apiauth_type"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var authState = Task.FromResult(new AuthenticationState(_anonymous));
            NotifyAuthenticationStateChanged(authState);
        }
    }

}
