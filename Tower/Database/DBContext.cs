using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tower.Database;

public class BDContext : DbContext
{
	public static string DefaultConnection
	{
		get
		{
			var server = "localhost";
			var userId = "root";
			var password = "root";
			var database = "Tower";
			return $"server={server};user id={userId};password={password};database={database};treattinyasboolean=true;ConvertZeroDateTime=True";
		}
	}
	public static string ConnectionSet
	{
		get
		{
			var user = Environment.GetEnvironmentVariable("DataBase-User");
			var password = Environment.GetEnvironmentVariable("DataBase-Password");
			var address = Environment.GetEnvironmentVariable("DataBase-Address");
			var Database = Environment.GetEnvironmentVariable("DataBase-Database");
			return $"server={address};user id={user};password={password};database={Database};ConvertZeroDateTime=True;treattinyasboolean=true;";
		}
	}
	public static BDContext Initialize()
	{
		var StringConnection = ConnectionSet;

        var optionsBuilder = new DbContextOptionsBuilder<BDContext>();
        optionsBuilder.UseMySql(StringConnection, ServerVersion.AutoDetect(StringConnection), option =>
        {
            option.EnableStringComparisonTranslations();
        });
        var context = new BDContext(optionsBuilder.Options);
		return context;
	}

	public BDContext(DbContextOptions<BDContext> options) : base(options)
	{
	}
	public virtual DbSet<User> Usuario { get; set; }
	public virtual DbSet<Pessoa> Pessoas { get; set; }
	public virtual DbSet<Acesso> Acessos { get; set; }
}