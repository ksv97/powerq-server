using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class ScheduledEvent
    {
		public int Id { set; get; }

		public int UserId { set; get; }
		public User User { set; get; }

		public int EventId { set; get; }
		public Event Event { set; get; }
    }
}
