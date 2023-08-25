using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Tower.Classes;
using Tower.Database;

namespace Tower.ModelsAPI;

public class PessoaAPI : Pessoa
{
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
	[JsonIgnore]
	private new int Id { get; }
	[JsonIgnore]
	private new List<Acesso>? Acessos { get;}
}
