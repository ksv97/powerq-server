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
			ScheduleEvent scheduleEventFromDB = context.ScheduleEvents.SingleOrDefault(i => i.Id == id);
			if (scheduleEventFromDB != null)
			{
				context.ScheduleEvents.Remove(scheduleEventFromDB);
				context.SaveChanges();
				return 1;
			}
			return null;
		}

		public List<ScheduleEventViewModel> GetAllScheduleEvents(int userId, bool isDeadline)
		{
			//User user = context.Users.SingleOrDefault(i => i.Id == userId);
			List<ScheduleEvent> eventsList = context.ScheduleEvents.AsNoTracking().
				Include(i => i.ScheduleEventUsers).ThenInclude(d => d.User).ThenInclude(u => u.Role).Where(e => e.IsDeadline == isDeadline).ToList();
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
			if (viewModel != null)
			{
				ScheduleEvent oldScheduleEvent = context.ScheduleEvents.
					Include(x => x.ScheduleEventUsers).ThenInclude(x => x.User).ThenInclude(x => x.Role).
					SingleOrDefault(x => x.Id == viewModel.Id);
				if (oldScheduleEvent != null)
				{
					oldScheduleEvent.Description = viewModel.Description;
					oldScheduleEvent.Date = viewModel.Date;
					oldScheduleEvent.IsDeadline = viewModel.IsDeadline;
					oldScheduleEvent.Title = viewModel.Title;
					context.SaveChanges();

					// adding new users for current event if any appears
					foreach (UserViewModel user in viewModel.Users)
					{
						User userFromDb = context.Users.Include(r => r.Role).SingleOrDefault(x => x.Id == user.Id);
						if (userFromDb != null)
						{
							ScheduleEventUser scheduleEventUserFromDb = context.ScheduleEventUsers.SingleOrDefault(x => x.ScheduleEvent.Id == oldScheduleEvent.Id && x.User.Id == userFromDb.Id);
							if (scheduleEventUserFromDb == null)
							{
								scheduleEventUserFromDb = new ScheduleEventUser()
								{
									ScheduleEvent = oldScheduleEvent,
									User = userFromDb,
								};
								context.ScheduleEventUsers.Add(scheduleEventUserFromDb);
								context.SaveChanges();
							}
						}
					}
					return 1;
				}
				else return -1;
			}
			return null;
		}
	}
}
