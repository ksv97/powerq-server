using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;
using CoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Repositories.MarkRepository
{
	public class MarkRepository : IMarkRepository
	{
		private Context context;

		public MarkRepository(Context context)
		{
			this.context = context;
		}

		public int? UpdateMark(FeedbackViewModel feedback)
		{
			ScheduledEvent oldScheduledEventForFeedback = GetScheduledEventByKeys(feedback.Author.Id, feedback.Event.Id);
			if (oldScheduledEventForFeedback != null)
			{
				Feedback oldFeedback = oldScheduledEventForFeedback.Feedback;
				oldFeedback.Mark = feedback.Mark;
				Curator authorOfFeedback = this.context.Curators.Include(c => c.Faculty).Include(u => u.User).SingleOrDefault(entity => entity.User.Id == feedback.Author.Id);
				if (authorOfFeedback != null)
				{
					authorOfFeedback.Mark = CalculateCuratorMark(authorOfFeedback.Id);
					authorOfFeedback.Faculty.Mark = CalculateFacultyMark(authorOfFeedback.Faculty.Id);
					context.SaveChanges();
					return 1;
				}				
			}
			return null;
		}

		private ScheduledEvent GetScheduledEventByKeys(int userId, int eventId)
		{
			return this.context.ScheduledEvents.Include(f => f.Feedback).ThenInclude(a => a.FeedbackAnswerForm).ThenInclude(f => f.FeedbackAnswers).Include(u => u.User).Include(e => e.Event).Single(i => i.Event.Id == eventId && i.User.Id == userId);
		}

		// оценка факультета = средняя оценка кураторов факультета с округлением в большую сторону
		private int CalculateFacultyMark (int facultyId)
		{
			Faculty faculty = this.context.Faculties.Include(u => u.Users).SingleOrDefault(a => a.Id == facultyId);
			List<Curator> curatorsFromFaculty = this.context.Curators.Include(f => f.Faculty).Where(cur => cur.Faculty.Id == facultyId).ToList();
			if (faculty != null && curatorsFromFaculty != null)
			{
				int result = 0;
				foreach (Curator curator in curatorsFromFaculty)
				{
					result += curator.Mark;
				}
				return (int)Math.Floor((double)result / curatorsFromFaculty.Count);
			}
			return 0;
		}

		private int CalculateCuratorMark (int curatorId)
		{
			Curator curator = this.context.Curators.Include(i => i.User).ThenInclude(f => f.FeedbacksOfUser).SingleOrDefault(entity => entity.Id == curatorId);
			if (curator != null)
			{
				int mark = 0;
				foreach (Feedback feedback in curator.User.FeedbacksOfUser)
				{
					mark += feedback.Mark;
				}
				return mark;
			}
			return 0;
		}
	}
}
