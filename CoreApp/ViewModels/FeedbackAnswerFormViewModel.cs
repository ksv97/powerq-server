using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;

namespace CoreApp.ViewModels
{
    public class FeedbackAnswerFormViewModel
    {
		public int Id { set; get; }
		public string Name { set; get; }
		public DateTime DeadlineDate { set; get; }

		public List<FeedbackAnswerViewModel> FeedbackAnswers;

		public FeedbackAnswerFormViewModel(FeedbackAnswerForm form)
		{			
			if (form != null)
			{
				Id = form.Id;
				Name = form.Name;
				DeadlineDate = form.DeadlineDate;

				FeedbackAnswers = new List<FeedbackAnswerViewModel>();
				foreach (FeedbackAnswer answer in form.FeedbackAnswers)
				{
					FeedbackAnswerViewModel answerVM = new FeedbackAnswerViewModel(answer);
					FeedbackAnswers.Add(answerVM);
				}
			}
			
		}
    }
}
