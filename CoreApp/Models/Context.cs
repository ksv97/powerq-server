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
		public DbSet<Event> Events { set; get; }
		public DbSet<ScheduledEvent> ScheduledEvents { set; get; }
		public DbSet<FeedbackForm> FeedbackForms { set; get; }
		public DbSet<FeedbackQuestion> FeedbackQuestions { set; get; }

		public Context(DbContextOptions<Context> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ScheduledEvent>().HasKey(key => new { key.EventId, key.UserId });

		}

	}
}
