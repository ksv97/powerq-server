using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;

namespace CoreApp.ViewModels
{
	public class ScheduleEventViewModel
	{
		public int Id { set; get; }
		public DateTime Date { set; get; }
		public string Title { set; get; }
		public string Description { set; get; }
		public bool IsDeadline { set; get; }
		public List<UserViewModel> Users { set; get; }

		public ScheduleEventViewModel(ScheduleEvent newEvent)
		{
			if (newEvent != null)
			{
				this.Id = newEvent.Id;
				this.Date = newEvent.Date;
				this.Title = newEvent.Title;
				this.Description = newEvent.Description;
				this.IsDeadline = newEvent.IsDeadline;
				this.Users = new List<UserViewModel>();
				foreach (ScheduleEventUser element in newEvent.ScheduleEventUsers)
				{
					UserViewModel userViewModel = new UserViewModel(element.User);
					this.Users.Add(userViewModel);
				}
			}
		}

    }
}
