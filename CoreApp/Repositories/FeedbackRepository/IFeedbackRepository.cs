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
		List<FeedbackViewModel> GetAllFeedbacks();
    }
}
