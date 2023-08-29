using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Tower.Classes;
using Tower.Database;

namespace Tower.ModelsAPI;
/// <summary>
/// Dados da pessoa a ser cadastrada
/// </summary>
[DisplayName("Pessoa")]
public class PessoaAPI : Pessoa
{
	/// <summary>
	/// CPF do visitante, digite apenas números
	/// </summary>
	public string CPF { 
		get 
		{
			return base.CPF;
		} set
		{
			base.CPF = @Regex.Replace(value ?? "", @"(\w{3})(\w{3})(\w{3})(\w{2})", @"$1.$2.$3-$4");
		} 
	}
	public new string Tipo
	{
		get
		{
			return ExtensionsClass.GetEnumDisplayName(base.Tipo);
		}
		set
		{
			if(value== null)
			{
				throw new Exception("Valor do tipo não pode ser nulo");
			}
			base.Tipo = ExtensionsClass.GetEnumValueFromDisplayName<TipoFunc>(value);
		}
	}
	[Microsoft.AspNetCore.Mvc.BindProperty]
	private new int Id { get; }
	[JsonIgnore]
	private new List<Acesso>? Acessos { get;}
}
