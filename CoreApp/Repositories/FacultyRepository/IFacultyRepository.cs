﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;

namespace CoreApp.Repositories.FacultyRepository
{
    public interface IFacultyRepository
    {
		List<FacultyViewModel> GetAllFaculties();
		FacultyViewModel GetFaculty (int facultyId);
		FacultyViewModel GetUserFaculty(int userId);
		bool DeleteFaculty(FacultyViewModel item);
		bool AddFaculty(FacultyViewModel item);
		bool UpdateFaculty(FacultyViewModel item);
    }
}
