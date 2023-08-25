using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tower.Database;
using Tower.ModelsAPI;

namespace Tower.Classes;

public static class SecurityClass
{    /// <summary>
     /// Gera uma chave aleatória em hexadecimal no formato AES
     /// </summary>
     /// <returns></returns>
    private static Aes AESKey;
    public static Aes? GenerateKey()
    {
        if (AESKey == null)
        {
            using var rsaProvider = new RSACryptoServiceProvider(2048);
            var privatekey = Convert.ToBase64String(rsaProvider.ExportPkcs8PrivateKey());
            var key = Aes.Create();
            AESKey = key;
        }
        return AESKey;
    }
    public static string GenerateHASH(string Password) => Convert.ToBase64String(SHA512.HashData(Encoding.UTF8.GetBytes(Password)));
    private static (List<Claim> claims, AuthenticationProperties authProperties) DefineClaim(User user)
    {
        var claims = SetClaim(user);
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
            IssuedUtc = DateTimeOffset.UtcNow,
        };
        return (claims, authProperties);
    }

    private static List<Claim> SetClaim(User user)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };
        return claims;
    }
    public static (List<Claim> claims, AuthenticationProperties authProperties) ValidateLogin(User UserValidate)
    {
        using var context = BDContext.Initialize();

        var password = UserValidate.Password;
        var user = context.Usuarios.FirstOrDefault(p => p.Email == UserValidate.Email);
        if (user == null)
        {
            throw new Exception("Usuario incorreto");
        }
        var passwordHash = GenerateHASH(password);
        return (passwordHash != user.Password) ? throw new Exception("Senha incorreta") : DefineClaim(user);
    }
    public static string GenerateToken(UserAPI User)
    {
        try
        {
            GenerateKey();
            var clains = ValidateLogin(User);
            var tokenHandler = new JwtSecurityTokenHandler();
            var Expirestimer = DateTime.UtcNow.AddHours(8);
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                Expirestimer = DateTime.UtcNow.AddHours(1);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(clains.claims),
                Expires = Expirestimer,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(AESKey.Key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch
        {
            throw;
        }
    }
}
