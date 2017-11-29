using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class ElderCurator
    {
		public int Id { set; get; }

		public int UserId { set; get; }
		public User User { set; get; }

		public int FacultyId { set; get; }
		public Faculty Faculty { set; get; }
    }
}
