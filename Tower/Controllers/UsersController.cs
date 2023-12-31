﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tower.Classes;
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
    /// <summary>
    /// Pagina de logout do usuário
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
    /// <summary>
    /// Pagina de login
    /// </summary>
    /// <param name="returnUrl"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        using var context = BDContext.Initialize();
        if (context.Usuarios.Any())
        {
            var url = TempData["ReturnURL"];
            url ??= returnUrl;
            TempData["ReturnURL"] = url;
            ViewBag.Error = "";
            return View();

        }
        else
        {
            return RedirectToAction("NewSystem");
        }
    }
    /// <summary>
    /// Função para autenticar os dados inseridos pelo usuário
    /// </summary>
    /// <param name="users"></param>
    /// <returns>Retorna a pagina principal, caso o usuário tenha tentanto utilizar outra pagina antes do login, redireciona para pagina requerida</returns>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(User user)
    {
        try
        {
            var url = TempData["ReturnURL"];
            TempData["ReturnURL"] = url;
            var (claims, authProperties) = SecurityClass.ValidateLogin(user);
            var claimIdendity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(claimIdendity), authProperties);
            return url != null ? Redirect((string)url) : RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
            return View();
        }
    }
    [AllowAnonymous]
    [HttpGet]
    public IActionResult NewSystem()
    {
        using var context = BDContext.Initialize();
        if (!context.Usuarios.Any())
        {
            return View();
        }
        else
        {
            return RedirectToAction("Login");
        }
    }
    [AllowAnonymous]
    [HttpPost]
    public IActionResult NewSystem(User user)
    {
        try
        {

            using var context = BDContext.Initialize();
            if (context.Usuarios.Any())
            {
                throw new Exception("O sistema já possui usuário cadastrado")
                {
                    Source = "Action",
                };
            }
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                var usercadastro = new UserClass().CadastraUser(user);
                TempData["success"] = "Usuário cadastrado com sucesso";
                return RedirectToAction("Index", "Home");
            }
        }
        catch (Exception ex) when (ex.Source == "Action")
        {
            TempData["error"] = ex.Message;
            return RedirectToAction("Index", "Home");
        }
    }
}