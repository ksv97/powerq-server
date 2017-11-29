using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;
using CoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Repositories.EventsRepository
{
	public class EventRepository : IEventRepository
	{
		private readonly Context context;

		public EventRepository(Context context)
		{
			this.context = context;
		}

		public int? CreateScheduleEvent(ScheduleEventViewModel viewModel)
		{
			if (viewModel != null)
			{
				ScheduleEvent scheduleEvent = new ScheduleEvent()
				{
					Date = viewModel.Date,
					Description = viewModel.Description,
					IsDeadline = viewModel.IsDeadline,
					Title = viewModel.Title,
					ScheduleEventUsers = new List<ScheduleEventUser>(),
					
				};

				foreach (UserViewModel userVM in viewModel.Users)
				{
					User user = this.context.Users.SingleOrDefault(i => i.Id == userVM.Id);
					if (user == null)
					{
						return -1;
					}
					else
					{
						scheduleEvent.ScheduleEventUsers.Add(new ScheduleEventUser()
						{
							ScheduleEvent = scheduleEvent,
							User = user
						});
					}
				}

				var entity = context.ScheduleEvents.Add(scheduleEvent);
				context.SaveChanges();
				int? id = entity.Entity.Id;
				return id;
			}
			return null;
		}

		public int? DeleteScheduleEvent(int id)
		{
			throw new NotImplementedException();
		}

		public List<ScheduleEventViewModel> GetAllScheduleEvents(int userId)
		{
			//User user = context.Users.SingleOrDefault(i => i.Id == userId);
			List<ScheduleEvent> eventsList = context.ScheduleEvents.AsNoTracking().
				Include(i => i.ScheduleEventUsers).ThenInclude(d => d.User).ThenInclude(u => u.Role).ToList();
			List<ScheduleEventViewModel> list = new List<ScheduleEventViewModel>();
			foreach (var item in eventsList)
			{
				foreach (var user in item.ScheduleEventUsers)
				{
					if (user.User.Id == userId)
					{
						list.Add(new ScheduleEventViewModel(item));
					}
				}
			}

			

			return list;
			
		}

		public int? UpdateScheduleEvent(ScheduleEventViewModel viewModel)
		{
			throw new NotImplementedException();
		}
	}
}
