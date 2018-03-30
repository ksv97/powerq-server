using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class User
    {
		public int Id { set; get; }
		public string Login { set; get; }
		public string Password { set; get; }
		public string FirstName { set; get; }
		public string SurName { set; get; }
		public bool IsAdmin { set; get; }
		[InverseProperty("User")]
		public List<ScheduledEvent> ScheduledEvents { set; get; }
		[InverseProperty("Author")]
		public List<ScheduledEvent> AuthoredEvents { set; get; }

		public int RoleId { set; get; }
		
		public Role Role { set; get; }

		public User()
		{
			this.ScheduledEvents = new List<ScheduledEvent>();
		}
    }
}
