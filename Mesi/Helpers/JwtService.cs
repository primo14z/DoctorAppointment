namespace Mesi.Helpers;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

public class JwtService(IConfiguration configuration)
{
    private readonly string secretKey = configuration["Jwt:SecretKey"];

    public string GenerateJwt(string userId, string username, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role),
        };

        var key = new SymmetricSecurityKey(Convert.FromBase64String(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "YourIssuer",
            audience: "YourAudience",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal ValidateJwt(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Convert.FromBase64String(secretKey));

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidIssuer = "YourIssuer",
            ValidAudience = "YourAudience",
            ClockSkew = TimeSpan.Zero, // You may want to adjust the clock skew
        };

        SecurityToken validatedToken;
        return tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
    }
}