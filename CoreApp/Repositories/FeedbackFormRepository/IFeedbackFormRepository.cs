using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;


namespace CoreApp.Repositories.FeedbackFormRepository
{
    public interface IFeedbackFormRepository
    {
		List<FeedbackFormViewModel> GetAllFeedbackForms();
		bool DeleteFeedbackForm(int id);
		bool AddFeedbackForm(FeedbackFormViewModel item);
		bool UpdateFeedbackForm(FeedbackFormViewModel item);
	}
}
