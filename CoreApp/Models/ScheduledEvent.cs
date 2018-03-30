using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class ScheduledEvent
    {
		public int Id { set; get; }

		public Feedback Feedback { get; set; }
		
		[ForeignKey("UserId")]
		public User User { set; get; }
		public int? UserId { set; get; }

		[ForeignKey("AuthorId")]
		public User Author { set; get; }
		public int? AuthorId { set; get; }

		public int EventId { set; get; }
		public Event Event { set; get; }
    }
}
