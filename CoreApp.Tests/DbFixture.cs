using System;
using System.Collections.Generic;
using System.Text;
using CoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Tests
{
	public class DbFixture : IDisposable
	{
		public Context Context { get; private set; }
		public List<User> Users { get; private set; }

		public DbFixture()
		{
			var optionsBuilder = new DbContextOptionsBuilder<Context>();		
			optionsBuilder.UseInMemoryDatabase("main");
			this.Context = new Context(optionsBuilder.Options);
			//DbSeed.Seed(Context);
		}
		
		public void Dispose()
		{
			this.Context.Dispose();
		}
	}
}
