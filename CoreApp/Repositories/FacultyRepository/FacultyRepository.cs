using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;
using CoreApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Repositories.FacultyRepository
{
	public class FacultyRepository : IFacultyRepository
	{
		private Context context;

		public FacultyRepository(Context context)
		{
			this.context = context;
		}

		public bool AddFaculty(FacultyViewModel item)
		{
			Faculty faculty = new Faculty();
			faculty.Name = item.Name;

			context.Faculties.Add(faculty);

			context.SaveChanges();
			return true;
		}

		public bool DeleteFaculty(FacultyViewModel item)
		{
			throw new NotImplementedException();
		}

		public List<FacultyViewModel> GetAllFaculties()
		{
			var faculties = context.Faculties.ToList();

			List<FacultyViewModel> result = new List<FacultyViewModel>();

			foreach (var fac in faculties)
			{
				result.Add(new FacultyViewModel(fac));
			}

			return result;
		}

		public FacultyViewModel GetFaculty(int facultyId)
		{
			return new FacultyViewModel(this.context.Faculties.SingleOrDefault(f => f.Id == facultyId));
		}

		public bool UpdateFaculty(FacultyViewModel item)
		{
			throw new NotImplementedException();
		}
	}
}
