using Tower.Classes;
using Tower.Database;

namespace Tower.DBModels;

public class PessoasClass
{
	public Pessoa CadastraPessoa(Pessoa Pessoa)
	{
		try
		{			
			using var context = BDContext.Initialize();
			if(context.Pessoas.Any(x=>x.CPF == Pessoa.CPF))
			{
				throw new Exception("CPF já cadastrado")
				{
					Source = "Action",
				};
			}
			context.Pessoas.Add(Pessoa);
			context.SaveChanges();
			return Pessoa;
		}
		catch
		{
			throw;
		}
	}
    public Pessoa EditPessoa(Pessoa pessoa)
    {
        try
        {
            using var context = BDContext.Initialize();
            if (context.Pessoas.Any(x => x.CPF == pessoa.CPF && x.Id!= pessoa.Id))
            {
                throw new Exception("CPF já cadastrado")
                {
                    Source = "Action",
                };
            }
            context.Pessoas.Update(pessoa);
            context.SaveChanges();
            return pessoa;
        }
        catch
        {
            throw;
        }
    }
}
