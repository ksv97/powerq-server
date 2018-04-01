using CoreApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Repositories.FeedbackRepository
{
    public interface IFeedbackRepository
    {
		int? CreateFeedback(FeedbackViewModel viewModel);
		int? UpdateFeedback(FeedbackViewModel viewModel);
		int? DeleteFeedback(DeleteFeedbackViewModel deleteModel);		
		List<FeedbackViewModel> GetAllFeedbacks();
		List<FeedbackViewModel> GetUserFeedbacks(int userId);
		List<FeedbackViewModel> GetFacultyFeedbacks(int facultyId);
	}
}
