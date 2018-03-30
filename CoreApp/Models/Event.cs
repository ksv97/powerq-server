using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

		public int? AuthorId { set; get; }
		[ForeignKey("AuthorId")]
		public User Author{ get; set; }

		public Event()
		{
			this.ScheduledEvents = new List<ScheduledEvent>();
		}

    }
}
