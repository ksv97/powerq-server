using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;
using CoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Repositories.FeedbackRepository
{
	public class FeedbackRepository : IFeedbackRepository
	{
		private Context context;

		public FeedbackRepository(Context context)
		{
			this.context = context;
		}

		public int? CreateFeedback(FeedbackViewModel viewModel)
		{
			if (viewModel != null)
			{
				FeedbackAnswerForm newAnswerForm = new FeedbackAnswerForm
				{
					DeadlineDate = viewModel.FeedbackAnswerForm.DeadlineDate,
					Name = viewModel.FeedbackAnswerForm.Name,
					FeedbackAnswers = new List<FeedbackAnswer>()
				};

				foreach (FeedbackAnswerViewModel answerVM in viewModel.FeedbackAnswerForm.FeedbackAnswers)
				{
					FeedbackAnswer newAnswer = new FeedbackAnswer
					{
						Question = answerVM.Question,
						Answer = answerVM.Answer,
					};
					newAnswerForm.FeedbackAnswers.Add(newAnswer);
				}

				Feedback newFeedback = new Feedback
				{
					Author = this.context.Users.Single(i => i.Id == viewModel.Author.Id),
					DateOfWriting = viewModel.DateOfWriting,
					FeedbackAnswerForm = newAnswerForm,
					Mark = viewModel.Mark,
					ScheduledEvent = this.context.ScheduledEvents.Single(i => i.Event.Id == viewModel.Event.Id && i.User.Id == viewModel.Author.Id),
				};

				var entity = this.context.Feedbacks.Add(newFeedback);				
				context.SaveChanges();				
				return 1;
			}
			return null;
		}

		public List<FeedbackViewModel> GetAllFeedbacks()
		{
			List<Feedback> feedbacksFromDb = this.context.Feedbacks.Include(i => i.Author).Include(i => i.FeedbackAnswerForm).ThenInclude(a => a.FeedbackAnswers).Include(e => e.ScheduledEvent).ThenInclude(ev => ev.Event).ToList();
			List<FeedbackViewModel> feedbacksViewModels = new List<FeedbackViewModel>();

			foreach (Feedback feedbackFromDb in feedbacksFromDb)
			{
				feedbacksViewModels.Add(new FeedbackViewModel(feedbackFromDb));
			}
			return feedbacksViewModels;
		}
	}
}
