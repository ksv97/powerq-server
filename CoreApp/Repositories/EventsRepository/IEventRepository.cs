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
		List<EventViewModel> GetUserDeadlines(int userId);
		List<EventViewModel> GetAllScheduleEvents(int userId);
		int? DeleteScheduleEvent(int id);
		int? UpdateScheduleEvent(EventViewModel newScheduleEventVM);
    }
}
