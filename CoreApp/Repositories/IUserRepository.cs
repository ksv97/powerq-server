using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;

namespace CoreApp.Repositories
{
    public interface IUserRepository
    {
		List<UserViewModel> GetAllUsers();
		UserViewModel TryLogIn(UserViewModel user);
		bool CheckIfLoginAvailable(string login);
		int? RegisterCurator(CuratorViewModel curatorViewModel);
		int? RegisterElderCurator(ElderCuratorViewModel elderCuratorVM);
    }
}
