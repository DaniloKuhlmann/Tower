using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tower.Database;

[Index(nameof(CPF), IsUnique = true)]
public class Pessoa
{
	[Key]
	public int Id { get; set; }
	/// <summary>
	/// Preencha o nome ou parte do nome do visitante.
	/// </summary>
	[Required]
	public string Nome { get; set; }
    [RegularExpression(@"\d{3}\.?\d{3}\.?\d{3}-?\d{2}|\d{2}\.?\d{3}\.?\d{3}/?\d{4}-?\d{2}", ErrorMessage = "Formato inválido")]
	[Required]
    public string CPF { get; set; }
	/// <summary>
	/// Escreva o tipo de visitante.
	/// </summary>
	[Required]
	[DisplayName("Tipo")]
	public TipoFunc Tipo { get; set; }
	/// <summary>
	/// Se possuir digite o nome da empresa a qual o visitante pertence
	/// </summary>
	public string? Empresa { get; set; }
	public virtual List<Acesso>? Acessos { get; set; }
}
/// <summary>
/// Tipo de funcionário a ser cadatrado
/// </summary>
public enum TipoFunc
{
	Funcionario,
	Visitante,
	[Display(Name ="Prestador de Serviço")]
	PrestadorServico
}
