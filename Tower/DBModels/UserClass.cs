using Tower.Classes;
using Tower.Database;

namespace Tower.DBModels;

public class UserClass
{
	public User CadastraUser(User user)
	{
		try
		{
			using var context = BDContext.Initialize();
			user.Password = SecurityClass.GenerateHASH(user.Password);
			context.Usuario.Add(user);
			context.SaveChanges();
			return user;
		}
		catch
		{
			throw;
		}
	}
}
