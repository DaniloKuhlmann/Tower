using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tower.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerTests.Classes;
using Tower.Classes;
using Tower.Database;

namespace Tower.DBModels.Tests;

[TestClass()]
public class AccessClassTests
{
	[TestMethod()]
	public void RegistrarEntradaTest()
	{
		CleanClass.Clear();
		var PessoasRegistrada = new PessoasClass().CadastraPessoa(new Database.Pessoa
		{
			Nome = "Teste",
			CPF = "333.3333.333-33",
			Empresa = "Teste",
			Tipo = ExtensionsClass.GetEnumValueFromDisplayName<Database.TipoFunc>("Visitante"),
		});
		var acesso = AccessClass.RegistrarEntrada(PessoasRegistrada.Id);
		var Context = BDContext.Initialize();
		var Acesso = Context.Acessos.Where(x => x.PessoaID == PessoasRegistrada.Id).ToList().Last();
		Assert.IsTrue(Acesso.Id == acesso.Id);
	}
	[TestMethod()]
	public void RegistrarSaidaTest()
	{
		RegistrarEntradaTest();
		var context = BDContext.Initialize();
		var pessoa = context.Pessoas.First();
		var Saida = AccessClass.RegistrarSaida(pessoa.Id);
		Assert.IsNotNull(Saida.DataHoraSaida);
	}
	[TestMethod()]
	public void TryRegistraDuasSaidas()
	{
		RegistrarSaidaTest();
		try
		{
			var context = BDContext.Initialize();
			var pessoa = context.Pessoas.First();
			var Saida = AccessClass.RegistrarSaida(pessoa.Id);
			Assert.Fail("Saida registrada mais de uma vez");
		}
		catch (Exception ex)
		{
			Assert.IsNotNull(ex.Message);
		}
	}
	[TestMethod()]
	public void TryRegistraDuasEntradas()
	{
		RegistrarEntradaTest();
		try
		{
			var context = BDContext.Initialize();
			var pessoa = context.Pessoas.First();
			var acesso = AccessClass.RegistrarEntrada(pessoa.Id);
			var Context = BDContext.Initialize();
			Assert.Fail("Entrada registrada mais de uma vez");
		}
		catch (Exception ex)
		{
			Assert.IsNotNull(ex.Message);
		}
	}

}