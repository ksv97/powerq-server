using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;

namespace CoreApp.ViewModels
{
    public class FeedbackAnswerViewModel
    {

		public int Id { set; get; }
		public string Question { set; get; }
		public string Answer { set; get; }

		public FeedbackAnswerViewModel(FeedbackAnswer model)
		{
			if (model != null)
			{
				Id = model.Id;
				Question = model.Question;
				Answer = model.Answer;
			}
			
		}
    }
}
