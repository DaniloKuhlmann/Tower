using Microsoft.AspNetCore.Mvc;
using Tower.Database;
using Tower.DBModels;

namespace Tower.Controllers;
public class PessoasController : Controller
{
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Add(Pessoa pessoa)
    {
        try
        {

            if (!ModelState.IsValid)
            {
                return View(pessoa);
            }
            else
            {
                new PessoasClass().CadastraPessoa(pessoa);
                return RedirectToAction("Index", "Home");
            }
        }
        catch (Exception ex) when (ex.Source == "Action")
        {
            TempData["error"] = ex.Message;
            return View(pessoa);
        }
        catch
        {
            return RedirectToAction("Index", "Home");
        }
    }
    public IActionResult Edit(int id)
    {
        using var context = BDContext.Initialize();
        var Pessoa = context.Pessoas.First(x => x.Id==id);
        return View(Pessoa);
    }
    [HttpPost]
    public IActionResult Edit(Pessoa pessoa)
    {
        try
        {

            if (!ModelState.IsValid)
            {
                return View(pessoa);
            }
            else
            {
                new PessoasClass().EditPessoa(pessoa);
                return RedirectToAction("Index", "Home");
            }
        }
        catch (Exception ex) when (ex.Source == "Action")
        {
            TempData["error"] = ex.Message;
            return View(pessoa);
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
