using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Tower.Database;

[Index(nameof(CPF), IsUnique = true)]
public class Pessoa
{
	[Key]
	public int Id { get; set; }
	public required string Nome { get; set; }
	public required string CPF { get; set; }
	public required TipoFunc Tipo { get; set; }
	public required string Empresa { get; set; }
	public virtual List<Acesso>? Acessos { get; set; }
}
public enum TipoFunc
{
	Funcionario,
	Visitante,
	[Display(Name ="Prestador de Serviço")]
	PrestadorServiço

}
