using DEBUG.BL.ViewModels.OptionsVMs;
using DEBUG.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DEBUG.BL.ExternalServices;

public class JWTTokenHandler : IJWTTokenHandler
{
    readonly JWTOption _option;
    public JWTTokenHandler(IOptions<JWTOption> options)
    {
        _option = options.Value;
    }
    public string CreateToken(User user, int hours = 24)
    {
        List<Claim> claims =
            [
                new Claim("Username", user.UserName),
                new Claim("Email", user.Email),
                new Claim("Role", user.Role.ToString()),
                new Claim("FullName", user.FullName)
            ];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.SecretKey));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken securityToken = new(
            issuer: _option.Issuer,
            audience: _option.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddSeconds(hours),
            signingCredentials: credentials
            );
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(securityToken);
    }
}
