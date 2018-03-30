using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreApp.Models
{
    public class Feedback
    {
		[Key]
		public int ScheduledEventId { set; get; }
		public int Mark { set; get; }
		public DateTime DateOfWriting { set; get; }
		
		public ScheduledEvent ScheduledEvent { set; get; }		
		public FeedbackAnswerForm FeedbackAnswerForm { set; get; }

		public int? AuthorId { get; set; }
		[ForeignKey("AuthorId")]
		public User Author { get; set; }

		public Feedback()
		{

		}
		

    }
}
