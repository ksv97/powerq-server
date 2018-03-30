using System;
using System.Collections.Generic;
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
		public List<ScheduledEvent> ScheduledEvents { set; get; }
		public List<Event> AuthoredEvents { get; set; }
		public List<Feedback> FeedbacksOfUser { get; set; }

		public int RoleId { set; get; }		
		public Role Role { set; get; }

		public User()
		{
			this.ScheduledEvents = new List<ScheduledEvent>();
			this.AuthoredEvents = new List<Event>();
			this.FeedbacksOfUser = new List<Feedback>();
		}
    }
}
