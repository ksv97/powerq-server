using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;

namespace CoreApp.ViewModels
{
	public class EventViewModel
	{
		public int Id { set; get; }
		public DateTime Date { set; get; }
		public string Title { set; get; }
		public string Description { set; get; }
		public bool IsDeadline { set; get; }
		public List<UserViewModel> Users { set; get; }
		public UserViewModel Author { set; get; }

		public EventViewModel(Event newEvent)
		{
			if (newEvent != null)
			{
				this.Id = newEvent.Id;
				this.Date = newEvent.Date;
				this.Title = newEvent.Title;
				this.Description = newEvent.Description;
				this.IsDeadline = newEvent.IsDeadline;
				this.Author = new UserViewModel(newEvent.Author);
				this.Users = new List<UserViewModel>();
				foreach (ScheduledEvent element in newEvent.ScheduledEvents)
				{
					UserViewModel userViewModel = new UserViewModel(element.User);
					this.Users.Add(userViewModel);
				}				
			}
		}

    }
}
