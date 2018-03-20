using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;

namespace CoreApp.ViewModels
{
    public class FeedbackQuestionViewModel
    {
		public int Id { set; get; }
		public string Name { set; get; }

		public FeedbackQuestionViewModel(FeedbackQuestion feedbackQuestion)
		{
			if (feedbackQuestion != null)
			{
				this.Id = feedbackQuestion.Id;
				this.Name = feedbackQuestion.Name;
			}
		}
	}
}
