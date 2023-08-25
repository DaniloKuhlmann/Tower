using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Tower.Database;

public class Acesso
{
	public int Id { get; set; }
	[ForeignKey(nameof(Pessoa))]
	public int PessoaID { get; set; }
	public DateTime DataHoraEntrada { get; set; }
	public DateTime? DataHoraSaida { get; set; }
	[XmlIgnore]
	public required Pessoa Pessoa { get; set; }
}
