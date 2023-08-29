using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tower.Classes;
using Tower.Database;
using Tower.ModelsAPI;

namespace Tower.API_Controllers;
/// <summary>
/// Controller resposavel pelo login na API
/// </summary>
[Route("api/[controller]")]
[ApiController]
[IgnoreAntiforgeryToken]
[AllowAnonymous]
public class UsersController : Controller
{
	/// <summary>
	/// Área de Login
	/// </summary>
	/// <returns></returns>
	/// <remarks>
	/// Exemplo de requisição:
	///
	///     POST
	///     {
	///        "email": "teste@teste",
	///        "password": "12345",
	///     }
	///
	/// </remarks>
	[HttpPost]
    [Route("TokenGenerate")]
    public IActionResult TokenGenerate(UserAPI User)
    {
        try
        {
            var token = SecurityClass.GenerateToken(User);
            return Ok($"Bearer {token}");
        }
        catch
        {
            return StatusCode(401);
        }
    }
}
