using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tower.Database;

namespace TowerTests.Classes;
internal static class CleanClass
{
	/// <summary>
	/// Limpa os dados do banco de dados
	/// </summary>
	public static void Clear()
	{
		var DataBase = "Tower-UnitTest";
		var UserID = "root";
		var Password = "root";
		var Server = "localhost";
		BDContext.VariablesConnection.SetDataBase(Server, Password,DataBase,UserID);
		using var context = BDContext.Initialize();
		context.Database.EnsureDeleted();
		context.Database.Migrate();
	}
}