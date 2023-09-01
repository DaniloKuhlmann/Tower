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
using System.Text.RegularExpressions;

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
			CPF = "3333333333ds33",
			Empresa = "Teste",
			Tipo = ExtensionsClass.GetEnumValueFromDisplayName<Database.TipoFunc>("Visitante"),
		});
		var sequencia = "333dsa333ds333333333aaa333sssd33";
		var sq2 = Regex.Replace(sequencia, @"\D", "")[..11];
		var teste = Regex.Replace(sq2, @"(\w{3})(\w{3})(\w{3})(\w{2})", @"$1.$2.$3-$4");

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