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

		public bool DeleteFeedbackForm(int id)
		{
			FeedbackForm formFromDb = this.context.FeedbackForms.Include(x => x.FeedbackQuestions).SingleOrDefault(x => x.Id == id);
			if (formFromDb != null)
			{
				foreach (FeedbackQuestion question in formFromDb.FeedbackQuestions)
				{
					this.context.FeedbackQuestions.Remove(question);
				}
				context.SaveChanges();

				this.context.FeedbackForms.Remove(formFromDb);
				this.context.SaveChanges();
				return true;
			}
			else return false;
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

					foreach (FeedbackQuestion questionFromDb in formFromDb.FeedbackQuestions)
					{
						// Найти в новом списке элемент с совпадающим айдишником
						FeedbackQuestionViewModel newQuestion = item.FeedbackQuestions.SingleOrDefault(q => q.Id == questionFromDb.Id);

						// если он найден, поместить в уже существующий в базе элемент новые данные
						if (newQuestion != null) 
						{
							questionFromDb.Name = newQuestion.Name;							
						}
						else // иначе удалить существующий элемент
						{
							formFromDb.FeedbackQuestions.Remove(questionFromDb);
						}
					}

					context.SaveChanges();

					// Выбрать все вопросы, у которых айди равен -1 (то есть новые) и добавить их в список
					foreach (FeedbackQuestionViewModel newQuestionVM in item.FeedbackQuestions.Where(i => i.Id == -1))
					{
						FeedbackQuestion questionToAdd = new FeedbackQuestion()
						{
							Name = newQuestionVM.Name,
							FeedbackForm = formFromDb,
						};
						formFromDb.FeedbackQuestions.Add(questionToAdd);
					}
					context.SaveChanges();
				}
				else return false;
			}
			return false;
			
		}
	}
}
