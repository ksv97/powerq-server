using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;

namespace CoreApp.ViewModels
{
    public class CuratorViewModel
    {
		public int Id { set; get; }
		public UserViewModel User { set; get; }

		public FacultyViewModel Faculty { set; get; }
		public string[] CuratedGroups { set; get; }

		public CuratorViewModel(Curator curator)
		{
			if (curator != null)
			{
				this.Id = curator.Id;
				this.User = new UserViewModel(curator.User);
				this.Faculty = new FacultyViewModel(curator.Faculty);				
				CuratedGroups = curator.CuratedGroups.Trim().Split(' ');
			}
		}
    }
}
