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

		public int? CreateScheduleEvent(EventViewModel viewModel)
		{
			if (viewModel != null)
			{
				Event scheduleEvent = new Event()
				{
					Date = viewModel.Date.AddHours(3),
					Description = viewModel.Description,
					IsDeadline = viewModel.IsDeadline,
					Title = viewModel.Title,
					ScheduledEvents = new List<ScheduledEvent>(),
					Author = this.context.Users.Single(x => x.Id == viewModel.Author.Id),
				};

				//scheduleEvent.Date = scheduleEvent.Date.AddHours(3)			
				

				foreach (UserViewModel userVM in viewModel.Users)
				{
					User user = this.context.Users.SingleOrDefault(i => i.Id == userVM.Id);
					if (user == null)
					{
						return -1;
					}
					else
					{
						scheduleEvent.ScheduledEvents.Add(new ScheduledEvent()
						{
							Event = scheduleEvent,
							User = user
						});
					}
				}

				var entity = context.Events.Add(scheduleEvent);
				context.SaveChanges();
				int? id = entity.Entity.Id;
				return id;
			}
			return null;
		}

		public int? DeleteScheduleEvent(int id)
		{
			Event eventFromDB = context.Events.SingleOrDefault(i => i.Id == id);
			if (eventFromDB != null)
			{
				context.Events.Remove(eventFromDB);
				context.SaveChanges();
				return 1;
			}
			return null;
		}

		public List<EventViewModel> GetAllEventsForFaculty(int facultyId, bool isDeadline)
		{			
			List<Curator> curatorsFromFaculty = this.context.Curators.Include(a => a.Faculty)
				.Include(a => a.User).ThenInclude(a => a.Role)
				.Where(cur => cur.Faculty.Id == facultyId).ToList();
			List<EventViewModel> eventsForFaculty = new List<EventViewModel>();
			foreach (Curator curator in curatorsFromFaculty)
			{
				eventsForFaculty.AddRange(this.GetAllScheduleEvents(curator.User.Id, isDeadline));
			}
			return eventsForFaculty;
		}

		public List<EventViewModel> GetAllScheduleEvents(int userId, bool gettingDeadlines)
		{
			//User user = context.Users.SingleOrDefault(i => i.Id == userId);
			
			List<Event> eventsList = GetEventsFromDb(gettingDeadlines);
			List<EventViewModel> list = new List<EventViewModel>();
			foreach (var item in eventsList)
			{			
				foreach (var scheduledEvent in item.ScheduledEvents)
				{
					if (scheduledEvent.User.Id == userId && scheduledEvent.Feedback == null)
					{
						list.Add(new EventViewModel(item));
					}
				}
			}
			return list;	
		}

		public List<EventViewModel> GetUserDeadlines(int userId)
		{
			bool isDeadline = true;
			List<Event> eventsList = GetEventsFromDb(isDeadline);
			List<EventViewModel> list = new List<EventViewModel>();
			foreach (var item in eventsList)
			{
				foreach (var scheduledEvent in item.ScheduledEvents)
				{
					if (scheduledEvent.User.Id == userId)
					{
						list.Add(new EventViewModel(item));
					}
				}
			}
			return list;
		}

		public int? UpdateScheduleEvent(EventViewModel viewModel)
		{
			if (viewModel != null)
			{
				Event oldScheduleEvent = context.Events.
					Include(x => x.ScheduledEvents).ThenInclude(x => x.User).ThenInclude(x => x.Role).
					SingleOrDefault(x => x.Id == viewModel.Id);
				if (oldScheduleEvent != null)
				{
					oldScheduleEvent.Description = viewModel.Description;
					oldScheduleEvent.Date = viewModel.Date.AddHours(3);
					oldScheduleEvent.IsDeadline = viewModel.IsDeadline;
					oldScheduleEvent.Title = viewModel.Title;					
					context.SaveChanges();

					// adding new users for current event if any appears
					foreach (UserViewModel user in viewModel.Users)
					{
						User userFromDb = context.Users.Include(r => r.Role).SingleOrDefault(x => x.Id == user.Id);
						if (userFromDb != null)
						{
							ScheduledEvent scheduledEventFromDb = context.ScheduledEvents.SingleOrDefault(x => x.Event.Id == oldScheduleEvent.Id && x.User.Id == userFromDb.Id);
							if (scheduledEventFromDb == null)
							{
								scheduledEventFromDb = new ScheduledEvent()
								{
									Event = oldScheduleEvent,
									User = userFromDb,
								};
								context.ScheduledEvents.Add(scheduledEventFromDb);
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

		private List<Event> GetEventsFromDb(bool isDeadline)
		{
			return context.Events.AsNoTracking().Include(e => e.Author)
				.Include(i => i.ScheduledEvents).ThenInclude(d => d.User).ThenInclude(u => u.Role)
				.Include(a => a.ScheduledEvents).ThenInclude(f => f.Feedback)
				.Where(e => e.IsDeadline == isDeadline).OrderBy(i => i.Date).ToList();
		}
	}
}
