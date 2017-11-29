using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class ScheduleEventUser
    {
		public int UserId { set; get; }
		public User User { set; get; }

		public int ScheduleEventId { set; get; }
		public ScheduleEvent ScheduleEvent { set; get; }
    }
}
