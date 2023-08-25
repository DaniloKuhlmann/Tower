using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Tower.Classes;
using Tower.Database;
using Tower.DBModels;
using Tower.ModelsAPI;


namespace Tower.API_Controllers;
/// <summary>
/// Controller para lista de acessos
/// </summary>
[Route("api/[controller]")]
[ApiController]
[IgnoreAntiforgeryToken]
[Authorize(Policy = "JWT")]
public class PessoasController : Controller
{
    /// <summary>
    /// Lista de todas as pessoas cadastradas
    /// </summary>
    /// <param name="CPF"></param>	/// <returns></returns>
    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        try
        {
            using var context = BDContext.Initialize();
            var pessoas = context.Pessoas.ToList();
            return Ok(pessoas);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    /// <summary>
    /// Busca a pessoa por CPF
    /// </summary>
    /// <param name="CPF"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{CPF}")]
    public IActionResult Index([FromRoute] string CPF)
    {
        try
        {
            using var context = BDContext.Initialize();
            var cpf = Regex.Replace(CPF, @"\D", "");
            var pessoas = context.Pessoas.Where(x => (x.CPF.Replace("-", "").Replace(".", "") == cpf)).Include(x=>x.Acessos.OrderByDescending(y=>y.Id)).First();
            return Ok(pessoas);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    /// <summary>
    /// Área de Cadastro de pessoas
    /// </summary>
    /// <param name="Pessoa"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    public IActionResult Index(PessoaAPI Pessoa)
    {
        try
        {
            var PessoaCadastro = new PessoasClass().CadastraPessoa(Pessoa);
            return Ok(PessoaCadastro);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    /// <summary>
    /// Função responsavel por editar pessoa
    /// </summary>
    /// <param name="Pessoa"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public IActionResult Index(PessoaAPI Pessoa, [FromRoute] int id)
    {
        try
        {
            Pessoa.Id = id;
            var PessoaCadastro = new PessoasClass().EditPessoa(Pessoa);
            return Ok(PessoaCadastro);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
