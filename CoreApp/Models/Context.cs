using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Models
{
    public class Context: DbContext
    {
		public DbSet<Faculty> Faculties { set; get; }	
		public DbSet<User> Users { set; get; }
		public DbSet<Role> Roles { set; get; }
		public DbSet<Curator> Curators { set; get; }
		public DbSet<ElderCurator> ElderCurators { set; get; }
		public DbSet<ScheduleEvent> ScheduleEvents { set; get; }
		public DbSet<ScheduleEventUser> ScheduleEventUsers { set; get; }

		public Context(DbContextOptions<Context> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ScheduleEventUser>().HasKey(key => new { key.ScheduleEventId, key.UserId });

		}

	}
}
