using Microsoft.AspNetCore.Mvc;
using Tower.Database;
using Tower.DBModels;

namespace Tower.Controllers;
public class UsersController : Controller
{
    public IActionResult Index()
    {
        try
        {
            using var context = BDContext.Initialize();
            var users = context.Usuarios.ToList();
            ViewBag.Users = users;
			return View();
		}
		catch
        {
            return RedirectToAction("Index","Home");
        }
    }
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Add(User user)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                var usercadastro = new UserClass().CadastraUser(user);
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex) when(ex.Source == "Action")
        {
            TempData["error"] = ex.Message;
            return View(user);
        }
        catch
        {
            return RedirectToAction("Index", "Home");
        }
    }
    public IActionResult Edit(int Id)
    {        
        return View();
    }
    [HttpPut]
    public IActionResult Edit(User user)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                var usercadastro = new UserClass().CadastraUser(user);
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex) when (ex.Source == "Action")
        {
            TempData["error"] = ex.Message;
            return View(user);
        }
        catch
        {
            return RedirectToAction("Index", "Home");
        }
    }

    public IActionResult Delete()
    {
        return View();
    }
}