using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;

namespace CoreApp.ViewModels
{
    public class ElderCuratorViewModel
    {
		public int Id { set; get; }
		public UserViewModel User { set; get; }

		public FacultyViewModel Faculty { set; get; }

		public ElderCuratorViewModel(ElderCurator curator)
		{
			if (curator != null)
			{
				this.Id = curator.Id;
				this.User = new UserViewModel(curator.User);
				this.Faculty = new FacultyViewModel(curator.Faculty);				
			}
		}

	}
}
