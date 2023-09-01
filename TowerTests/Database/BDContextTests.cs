using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tower.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerTests.Classes;

namespace Tower.Database.Tests;

[TestClass()]
public class BDContextTests
{
	[TestMethod()]
	public void InitializeTest()
	{
		CleanClass.Clear();
		var conneciton = BDContext.Initialize();
		Assert.IsNotNull(conneciton);
	}
}