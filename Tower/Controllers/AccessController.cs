using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Tower.Classes;
using Tower.Database;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Tower.DBModels;
using Microsoft.EntityFrameworkCore;

namespace Tower.Controllers;
public class AccessController : Controller
{
	public IActionResult Index(int? id)
	{
		try
		{
			using var context = BDContext.Initialize();
			var acesso = context.Acessos.Where(x => (x.PessoaID == id || id == null)).Include(x => x.Pessoa).OrderByDescending(x=>x.Id).ToList();
			ViewBag.Acesso = acesso;
			return View();
		}
		catch
		{
			return RedirectToAction("Index", "Home");
		}
    }
	[HttpPost]
    [IgnoreAntiforgeryToken]
    public IActionResult RegistrarEntrada(int id)
	{
		try
		{
			AccessClass.RegistrarEntrada(id);
			return Ok();
		}
		catch(Exception ex)
		{
			return StatusCode(400, ex.Message);
		}
	}
    [HttpPost]
	[IgnoreAntiforgeryToken]
    public IActionResult RegistrarSaida(int id)
    {
        try
        {
            AccessClass.RegistrarSaida(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex.Message);
        }
    }

}
