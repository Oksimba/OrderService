using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Common.Helpers
{
    public class TokenVerifier
    {
        public IOptions<AuthOptions> AuthOptions { get; }
        public TokenVerifier(IOptions<AuthOptions> authOptions)
        {
            AuthOptions = authOptions;
        }
        public ClaimsPrincipal VerifyToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(AuthOptions.Value.Secret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            return tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
        }
    }
}
