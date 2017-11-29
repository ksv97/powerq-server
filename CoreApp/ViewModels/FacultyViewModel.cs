using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;

namespace CoreApp.ViewModels
{
    public class FacultyViewModel
    {
		public int Id { set; get; }
		public string Name { set; get; }

		public FacultyViewModel(Faculty faculty)
		{
			if (faculty != null)
			{
				this.Id = faculty.Id;
				this.Name = faculty.Name;
			}
		}
    }
}
