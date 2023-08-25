using Tower.Database;

namespace Tower.ModelsAPI;
/// <summary>
/// 
/// </summary>
public class UserAPI : User
{

    public string Email
    {
        set
        {
            base.Email = value;
        }

    }
    public string Password
    {
        set
        {
            base.Password = value;
        }
    }
    private int Id { get; set; }
    private string Nome { get; set; }
}
