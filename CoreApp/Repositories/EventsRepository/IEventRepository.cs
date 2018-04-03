using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;

namespace CoreApp.Repositories.EventsRepository
{
    public interface IEventRepository
    {
		int? CreateScheduleEvent(EventViewModel scheduleEventVM);
		List<EventViewModel> GetAllScheduleEventsAssignedToUser(int userId, bool gettingDeadlines);
		List<EventViewModel> GetAllEvents(bool gettingDeadlines);

		int? DeleteScheduleEvent(int id);
		int? UpdateScheduleEvent(EventViewModel newScheduleEventVM);
		List<EventViewModel> GetAllEventsForFaculty(int facultyId, bool isDeadline);		
	}
}
