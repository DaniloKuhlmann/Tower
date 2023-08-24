using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tower.Database;
using Tower.Models;

namespace Tower.Controllers;
public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	public IActionResult Index()
	{
		using var context = BDContext.Initialize();
		var Pessoas = context.Pessoas.ToList();
		ViewBag.Pessoas = Pessoas;
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
