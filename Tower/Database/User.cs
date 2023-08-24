using Microsoft.EntityFrameworkCore;

namespace Tower.Database;
[Index(nameof(Email), IsUnique = true)]

public class User
{
	public int Id { get; set; }
	public required string Nome { get; set; }
	public required string Email { get; set; }
	public required string Password { get; set; }
}
