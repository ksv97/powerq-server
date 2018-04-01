using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;

namespace CoreApp.Repositories.MarkRepository
{
    public interface IMarkRepository
    {
		int? UpdateMark(FeedbackViewModel feedback);
    }
}
