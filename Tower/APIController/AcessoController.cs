using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Tower.Classes;
using Tower.Database;
using Tower.DBModels;
using Tower.ModelsAPI;

namespace Tower.API_Controllers;
/// <summary>
/// Controller para acesso dos parceiros
/// </summary>
[Route("/api/[controller]")]
[ApiController]
[IgnoreAntiforgeryToken]
[Authorize(Policy = "JWT")]
public class AcessoController : Controller
{
    /// <summary>
    /// Action responsavel por regitrar uma entrada a determinada pessoa 
    /// </summary>
    /// <param name="id">ID da pessoa que esta entrando</param>
    /// <returns></returns>
	[HttpPost]
	[Route("entrar")]
	public IActionResult entrar(int id)
	{
		return View();
	}
    /// <summary>
    /// Action responsavel por registar uma saida a determinada pessoa 
    /// </summary>
    /// <param name="id">ID da pessoa que está saindo</param>
    /// <returns>ID da pessoa que está saindo</returns>
    [HttpPost]
    [Route("sair")]
    public IActionResult sair(int id)
    {
        return View();
    }
}
