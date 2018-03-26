using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class Feedback
    {
		public int Id { set; get; }
		public int Mark { set; get; }
		public DateTime DateOfWriting { set; get; }

		public int ScheduledEventId { set; get; }
		public ScheduledEvent ScheduledEvent { set; get; }

		public int FeedbackAnswerFormId { get; set; }
		public FeedbackAnswerForm FeedbackAnswerForm { set; get; }

		public Feedback()
		{

		}
		

    }
}
