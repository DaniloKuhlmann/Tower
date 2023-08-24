using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tower.Database;

namespace Tower.Classes;

public class SecurityClass
{
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
	public (List<Claim> claims, AuthenticationProperties authProperties) ValidateLogin(User UserValidate)
	{
		using var context = BDContext.Initialize();
		var password = UserValidate.Password;
		var user = context.Usuario.FirstOrDefault(p => p.Email == UserValidate.Email);
		if (user == null)
		{
			throw new Exception("Usuario incorreto");
		}
		var passwordHash = GenerateHASH(password);
		return (passwordHash != user.Password) ? throw new Exception("Senha incorreta") : DefineClaim(user);
	}
}
