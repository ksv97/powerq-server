using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;

namespace CoreApp.ViewModels
{
    public class FeedbackFormViewModel
    {

		public int Id { set; get; }
		public string Name { set; get; }
		public DateTime DeadlineDate { set; get; }

		public List<FeedbackQuestionViewModel> Questions { set; get; }

		public FeedbackFormViewModel(FeedbackForm feedbackForm)
		{
			if (feedbackForm != null)
			{
				this.Id = feedbackForm.Id;
				this.Name = feedbackForm.Name;
				this.DeadlineDate = feedbackForm.DeadlineDate;
				this.Questions = new List<FeedbackQuestionViewModel>();

				foreach (FeedbackQuestion fq in feedbackForm.FeedbackQuestions)
				{
					this.Questions.Add(new FeedbackQuestionViewModel(fq));
				}
			}
		}
	}
}
