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
			if(context.Usuarios.Any(x=>x.Email == user.Email))
			{
				throw new Exception("Email já cadastrado")
				{
					Source = "Action",
				};
			}
			user.Password = SecurityClass.GenerateHASH(user.Password);
			context.Usuarios.Add(user);
			context.SaveChanges();
			return user;
		}
		catch
		{
			throw;
		}
	}
}
