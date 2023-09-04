using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tower.Database;

public class BDContext : DbContext
{
	public static class VariablesConnection
	{
		public static bool IsSet = false;
		private static string? _Server { get; set; } = "localhost";
		private static string? _UserID { get; set; } = "root";
		private static string? _Password { get; set; } = "root";
		private static string? _DataBase { get; set; } = "Tower";
		public static string RetornoConnection()
		{			
			return $"server={_Server};user id={_UserID};password={_Password};database={_DataBase};treattinyasboolean=true;ConvertZeroDateTime=True";
		}
		public static void  SetDataBase(string Server, string Password, string Database, string UserID)
		{
			IsSet = true;
			_Server = Server;
			_UserID = UserID;
			_Password = Password;
			_DataBase = Database;
		}		
	}
	private static void ConnectionSet()
	{
		var DataBase = Environment.GetEnvironmentVariable("DataBase-Database");
		var Password = Environment.GetEnvironmentVariable("DataBase-Password");
		var UserID = Environment.GetEnvironmentVariable("DataBase-User");
		var Server = Environment.GetEnvironmentVariable("DataBase-Address");
		if (DataBase != null)
		{
			VariablesConnection.SetDataBase(Server, Password, DataBase, UserID);
		}
	}
	/// <summary>
	/// Classe de conexão com o banco de dados
	/// </summary>
	/// <returns></returns>
	public static BDContext Initialize()
	{
		if (!VariablesConnection.IsSet)
		{
			ConnectionSet();
		}
		var optionsBuilder = new DbContextOptionsBuilder<BDContext>();
		optionsBuilder.UseMySql(VariablesConnection.RetornoConnection(), ServerVersion.AutoDetect(VariablesConnection.RetornoConnection()), option =>
		{
			option.EnableStringComparisonTranslations();
		});
		var context = new BDContext(optionsBuilder.Options);
		return context;
	}


	public BDContext(DbContextOptions<BDContext> options) : base(options)
	{
	}
	public virtual DbSet<User> Usuarios { get; set; }
	public virtual DbSet<Pessoa> Pessoas { get; set; }
	public virtual DbSet<Acesso> Acessos { get; set; }
}