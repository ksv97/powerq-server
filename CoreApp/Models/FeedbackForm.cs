using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class FeedbackForm
    {
		public int Id { set; get; }
		public string Name { set; get; }
		public DateTime DeadlineDate { set; get; }

		public List<FeedbackQuestion> FeedbackQuestions { set; get; }

		public FeedbackForm()
		{
			this.FeedbackQuestions = new List<FeedbackQuestion>();
		}
	}
}
