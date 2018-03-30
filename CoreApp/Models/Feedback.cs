using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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

		public Feedback()
		{

		}
		

    }
}
