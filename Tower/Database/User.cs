using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Tower.Database;
[Index(nameof(Email), IsUnique = true)]

public class User
{
    [Key]
	public int Id { get; set; }
	[Required]
	public string Nome { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
