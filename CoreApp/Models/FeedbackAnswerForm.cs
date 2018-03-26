using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
	public class FeedbackAnswerForm
	{
		public int Id { set; get; }
		public string Name { set; get; }
		public DateTime DeadlineDate { set; get; }


		public int FeedbackId { get; set; }
		public Feedback Feedback { set; get; }

		public List<FeedbackAnswer> FeedbackAnswers { set; get; }

		public FeedbackAnswerForm()
		{
			this.FeedbackAnswers = new List<FeedbackAnswer>();
		}
    }
}
