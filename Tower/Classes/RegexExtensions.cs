using System.Text.RegularExpressions;
using Tower.Database;

namespace Tower.Classes;

public static class RegexExtensions
{
	public static string CPFFormat(string sequencia)
	{
		return Regex.Replace(Regex.Replace(sequencia, @"\D", "")[..11], @"(\w{3})(\w{3})(\w{3})(\w{2})", @"$1.$2.$3-$4");
	}
}
