using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;
using CoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Repositories.FeedbackFormRepository
{	

	public class FeedbackFormRepository : IFeedbackFormRepository
	{
		private Context context;

		public FeedbackFormRepository(Context context)
		{
			this.context = context;
		}

		public bool AddFeedbackForm(FeedbackFormViewModel item)
		{			
			if (item != null)
			{
				FeedbackForm newFeedbackForm = new FeedbackForm
				{
					DeadlineDate = item.DeadlineDate,
					Name = item.Name,
				};
				foreach (FeedbackQuestionViewModel question in item.FeedbackQuestions)
				{
					FeedbackQuestion feedbackQuestion = new FeedbackQuestion
					{
						Name = question.Name,
					};
					newFeedbackForm.FeedbackQuestions.Add(feedbackQuestion);
				}
				this.context.FeedbackForms.Add(newFeedbackForm);
				this.context.SaveChanges();
				return true;
			}
			return false;			
		}

		public bool DeleteFeedbackForm(FeedbackFormViewModel item)
		{
			if (item != null)
			{
				FeedbackForm formFromDb = this.context.FeedbackForms.Include(x => x.FeedbackQuestions).SingleOrDefault(x => x.Id == item.Id);
				if (formFromDb != null)
				{
					this.context.FeedbackForms.Remove(formFromDb);
					this.context.SaveChanges();
				}
				else return false;
			}
			return false;
			
		}

		public List<FeedbackFormViewModel> GetAllFeedbackForms()
		{
			var query = this.context.FeedbackForms.Include(x => x.FeedbackQuestions);
			List<FeedbackFormViewModel> result = new List<FeedbackFormViewModel>();
			foreach (FeedbackForm formFromDb in query)
			{
				result.Add(new FeedbackFormViewModel(formFromDb));
			}
			return result;
		}

		public bool UpdateFeedbackForm(FeedbackFormViewModel item)
		{
			if (item != null)
			{
				FeedbackForm formFromDb = this.context.FeedbackForms.Include(x => x.FeedbackQuestions).SingleOrDefault(x => x.Id == item.Id);
				if (formFromDb != null)
				{
					formFromDb.DeadlineDate = item.DeadlineDate;
					formFromDb.Name = item.Name;
					formFromDb.FeedbackQuestions = new List<FeedbackQuestion>();
					
					// TODO
					// Дописать часть с апдейтом вопросов по следующему алгоритму
					// Сначала удалить все вопросы из старого списка, айдишников которых нет в новом списке item.FeedbackQuestions
					// Затем перебрать все вопросы в новом списке. Если найден в существующем вопрос с таким айдишником, изменить его характеристики
					// Иначе добавить новый вопрос к этому списку и записать в базу данных
				}
				else return false;
			}
			return false;
			
		}
	}
}
