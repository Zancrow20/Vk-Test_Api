using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using VkWebApi.Services.AuthService;
using Exception = System.Exception;

namespace VkWebApi.BasicAuthHandler;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IAuthService _authService;

    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IAuthService authService) : base(options, logger, encoder, clock)
    {
        _authService = authService;
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
        return base.HandleChallengeAsync(properties);
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization header wasn't found");
            
            var authHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"].ToString());
            var credentialBytes = Convert.FromBase64String(authHeaderValue.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
            var username = credentials[0];
            var password = credentials[1];

            var user = await _authService.Authenticate(username, password);
            if(user == null)
                return AuthenticateResult.Fail("Invalid credentials");
            var claims = new[] {new Claim(ClaimTypes.Name, user.Login), new Claim(ClaimTypes.Role,"Admin")};
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
                
            return AuthenticateResult.Success(ticket);

        }
        catch(Exception ex)
        {
            return AuthenticateResult.Fail("Authentication error has occured");
        }
    }
}