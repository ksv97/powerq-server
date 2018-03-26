using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class FeedbackAnswer
    {

		public int Id { set; get; }
		public string Question { set; get; }
		public string Answer { set; get; }

		public int FeedbackAnswerFormId { get; set; }
		public FeedbackAnswerForm FeedbackAnswerForm { set; get; }

		public FeedbackAnswer()
		{

		}
    }
}
