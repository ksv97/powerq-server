using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;

namespace CoreApp.ViewModels
{
    public class FeedbackViewModel
    {
		public int Id { set; get; }
		public DateTime DateOfWriting { get; set; }
		public int Mark { get; set; }
		public FeedbackAnswerFormViewModel FeedbackAnswerForm { get; set; }
		public EventViewModel Event { get; set; }

		public FeedbackViewModel(Feedback feedback)
		{
			Id = feedback.Id;
			DateOfWriting = feedback.DateOfWriting;
			Mark = feedback.Mark;
			FeedbackAnswerForm = new FeedbackAnswerFormViewModel(feedback.FeedbackAnswerForm);
			Event = new EventViewModel(feedback.ScheduledEvent.Event);
		}
	}
}
