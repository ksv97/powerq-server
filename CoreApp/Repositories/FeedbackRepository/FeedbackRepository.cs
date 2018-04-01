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

		public int? UpdateFeedback(FeedbackViewModel viewModel)
		{
			ScheduledEvent oldScheduledEventForFeedback = GetScheduledEventByKeys(viewModel.Author.Id, viewModel.Event.Id);
			if (oldScheduledEventForFeedback != null)
			{
				Feedback oldFeedback = oldScheduledEventForFeedback.Feedback;				
				foreach (FeedbackAnswerViewModel newAnswer in viewModel.FeedbackAnswerForm.FeedbackAnswers)
				{
					foreach (FeedbackAnswer oldAnswer in oldFeedback.FeedbackAnswerForm.FeedbackAnswers)
					{
						if (oldAnswer.Question == newAnswer.Question)
						{
							oldAnswer.Answer = newAnswer.Answer;
						}
					}
				}
				this.context.SaveChanges();
				return 1;
			}
			return null;
		}

		public int? DeleteFeedback(DeleteFeedbackViewModel deleteModel)
		{
			ScheduledEvent oldScheduledEventForFeedback = GetScheduledEventByKeys(deleteModel.UserId, deleteModel.EventId);
			if (oldScheduledEventForFeedback != null)
			{
				this.context.Feedbacks.Remove(oldScheduledEventForFeedback.Feedback);
				context.SaveChanges();
				return 1;
			}
			return null;
		}

		public List<FeedbackViewModel> GetAllFeedbacks()
		{
			List<Feedback> feedbacksFromDb = this.GetFeedbacksFromDb();
			List<FeedbackViewModel> feedbacksViewModels = new List<FeedbackViewModel>();

			foreach (Feedback feedbackFromDb in feedbacksFromDb)
			{
				feedbacksViewModels.Add(new FeedbackViewModel(feedbackFromDb));
			}
			return feedbacksViewModels;
		}

		private ScheduledEvent GetScheduledEventByKeys(int userId, int eventId)
		{
			return this.context.ScheduledEvents.Include(f => f.Feedback).ThenInclude(a => a.FeedbackAnswerForm).ThenInclude(f => f.FeedbackAnswers).Include(u => u.User).Include(e => e.Event).Single(i => i.Event.Id == eventId && i.User.Id == userId);
		}

		private List<Feedback> GetFeedbacksFromDb()
		{
			return this.context.Feedbacks.Include(i => i.Author).Include(i => i.FeedbackAnswerForm).ThenInclude(a => a.FeedbackAnswers)
				.Include(e => e.ScheduledEvent).ThenInclude(ev => ev.Event).OrderBy(i => i.FeedbackAnswerForm.Name).ToList();
		}

		public List<FeedbackViewModel> GetUserFeedbacks(int userId)
		{
			List<FeedbackViewModel> userFeedbacks = new List<FeedbackViewModel>();
			foreach (Feedback feedback in GetFeedbacksFromDb().Where(f => f.Author.Id == userId))
			{
				userFeedbacks.Add(new FeedbackViewModel(feedback));
			}
			return userFeedbacks;
		}

		public List<FeedbackViewModel> GetFacultyFeedbacks(int facultyId)
		{
			List<Curator> curatorsFromFaculty = this.context.Curators.Include(a => a.Faculty)
				.Include(a => a.User).ThenInclude(a => a.Role)
				.Where(cur => cur.Faculty.Id == facultyId).ToList();
			List<FeedbackViewModel> feedbacksForFaculty = new List<FeedbackViewModel>();
			foreach (Curator curator in curatorsFromFaculty)
			{
				feedbacksForFaculty.AddRange(this.GetUserFeedbacks(curator.User.Id));
			}
			return feedbacksForFaculty;
		}
	}
}
