using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;

namespace CoreApp.Repositories.EventsRepository
{
    public interface IEventRepository
    {
		int? CreateScheduleEvent(ScheduleEventViewModel scheduleEventVM);
		List<ScheduleEventViewModel> GetAllScheduleEvents(int userId, bool isDeadline);
		int? DeleteScheduleEvent(int id);
		int? UpdateScheduleEvent(ScheduleEventViewModel newScheduleEventVM);				
    }
}
