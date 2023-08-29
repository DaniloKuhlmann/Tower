using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Tower.Database;

namespace Tower.ModelsAPI;
/// <summary>
/// Cadastro utilizado para acessar o sistema
/// </summary>
[DisplayName("Usuário")]
public class UserAPI : User
{
    /// <summary>
    /// Preencha o email do usuário utilizado no sistema
    /// </summary>
    public string Email
    {
        set
        {
            base.Email = value;
        }

    }
    /// <summary>
    /// Preencha a senha utilizada no cadastro
    /// </summary>
    public string Password
    {
        set
        {
            base.Password = value;
        }
    }
    private int Id { get; set; }
    private string Nome { get; set; }
}
