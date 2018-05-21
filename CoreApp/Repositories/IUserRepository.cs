using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;

namespace CoreApp.Repositories
{
    public interface IUserRepository
    {		
		List<CuratorViewModel> GetCuratorsFromFaculty(int facultyId);
		UserViewModel TryLogIn(UserViewModel user);
		CuratorViewModel GetCurator(int userId);
		ElderCuratorViewModel GetElder(int userId);
		bool CheckIfLoginAvailable(string login);
		int? RegisterCurator(CuratorViewModel curatorViewModel);
		int? RegisterElderCurator(ElderCuratorViewModel elderCuratorVM);

		UserViewModel CreateUser(UserViewModel userVM);
		int? UpdateUser(UserViewModel userVm);
		int? DeleteUser(int userId);
		List<UserViewModel> GetAllUsers();
		List<UserViewModel> GetAllUsersWhoAreCurators();
		List<UserViewModel> GetAllUsersWhoAreElders();

	}
}
