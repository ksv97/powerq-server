using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class Event
    {
		public int Id { set; get; }
		public DateTime Date { set; get; }
		public string Title { set; get; }
		public string Description { set; get; }
		public bool IsDeadline { set; get; }
		public List<ScheduledEvent> ScheduledEvents { set; get; }

		public Event()
		{
			this.ScheduledEvents = new List<ScheduledEvent>();
		}

    }
}
