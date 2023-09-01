using Microsoft.EntityFrameworkCore;
using Tower.Database;

namespace Tower.DBModels;

public class AccessClass
{
    public static Acesso RegistrarSaida(int PessoaID)
    {
        try
        {
            using var context = BDContext.Initialize();
            var Pessoa = context.Pessoas.Where(x => x.Id == PessoaID).Include(x => x.Acessos).First();
            var acesso = Pessoa.Acessos?.LastOrDefault();
            if (acesso == null || acesso.DataHoraSaida!=null)
            {
                throw new Exception("Pessoa não possui entrada registrada");
            }
            acesso.DataHoraSaida = DateTime.Now;
            context.SaveChanges();
            return acesso;
        }
        catch
        {
            throw;
        }
    }
    public static Acesso RegistrarEntrada(int PessoaID)
    {
        try
        {
            using var context = BDContext.Initialize();
            var Pessoa = context.Pessoas.Where(x => x.Id == PessoaID).Include(x => x.Acessos).First();
            var acesso = Pessoa.Acessos?.LastOrDefault();
            if (acesso != null && acesso.DataHoraSaida == null)
            {
                throw new Exception("Pessoa possui entrada, porem não possui saída registrada");
            }
            var NewAcesso = new Acesso
            {
                Pessoa = Pessoa,
                PessoaID = Pessoa.Id,
                DataHoraEntrada = DateTime.Now,
            };
            context.Acessos.Add(NewAcesso);
            context.SaveChanges();
            return NewAcesso;
        }
        catch
        {
            throw;
        }
    }
}
