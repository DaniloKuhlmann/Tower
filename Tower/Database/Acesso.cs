using System.ComponentModel.DataAnnotations.Schema;

namespace Tower.Database;

public class Acesso
{
	public int Id { get; set; }
	[ForeignKey(nameof(Pessoa))]
	public int PessoaID { get; set; }
	public DateTime DataHoraEntrada { get; set; }
	public DateTime? DataHoraSaida { get; set; }
	public required Pessoa Pessoa { get; set; }
}
