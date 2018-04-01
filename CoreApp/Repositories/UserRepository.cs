using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;
using CoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly Context context;

		public UserRepository(Context context)
		{
			this.context = context;
		}

		public bool CheckIfLoginAvailable(string login)
		{
			User existingUser = context.Users.FirstOrDefault(s => s.Login == login);

			if (existingUser == null)
				return true;
			else return false;
		}

		//public int? CreateUser(UserViewModel newUser)
		//{
		//	User user = new User()
		//	{
		//		Login = newUser.Login,
		//		Password = newUser.Password,
		//		FirstName = newUser.FirstName,
		//		SurName = newUser.SurName,
		//		Role = context.Roles.SingleOrDefault(m => m.Name == newUser.Role.Name)
		//	};

		//	User existingUser = context.Users.Include(a => a.Role).FirstOrDefault(m => m.Login == user.Login);
		//	if (existingUser == null)
		//	{
		//		context.Users.Add(user);
		//		context.SaveChanges();
		//		return 1;
		//	}
		//	return null;
		//}

		public List<UserViewModel> GetAllUsers()
		{
			var users = context.Users.ToList();

			List<UserViewModel> result = new List<UserViewModel>();

			foreach (var user in users)
			{
				result.Add(new UserViewModel(user));
			}
			return result;
		}

		public int? RegisterCurator(CuratorViewModel curatorViewModel)
		{			
			if (curatorViewModel != null)
			{
				string curatedGroups = curatorViewModel.CuratedGroups;
				//ParseCuratedGroups(curatorViewModel.CuratedGroups, out curatedGroups);


				Curator curator = new Curator()
				{
					User = new User()
					{
						Login = curatorViewModel.User.Login,
						Password = curatorViewModel.User.Password,
						FirstName = curatorViewModel.User.FirstName,
						SurName = curatorViewModel.User.SurName,
						Role = context.Roles.SingleOrDefault(r => r.Name == curatorViewModel.User.Role.Name),
					},
					Faculty = context.Faculties.SingleOrDefault(f => f.Id == curatorViewModel.Faculty.Id),
					CuratedGroups = curatedGroups,
				};

				context.Curators.Add(curator);
				context.SaveChanges();

				return 1;
			}
			return null;

		}

		public void ParseCuratedGroups (string[] groups, out string curatedGroups)
		{
			curatedGroups = "";
			foreach (string gr in groups)
			{
				curatedGroups += gr + " ";
			}
		}

		public int? RegisterElderCurator(ElderCuratorViewModel elderCuratorVM)
		{
			//User user = new User()
			//{
			//	Login = elderCuratorVM.User.Login,
			//	Password = elderCuratorVM.User.Password,
			//	FirstName = elderCuratorVM.User.FirstName,
			//	SurName = elderCuratorVM.User.SurName,
			//	Role = context.Roles.SingleOrDefault(r => r.Name == elderCuratorVM.User.Role.Name),
			//};

			//context.Users.Add(user);
			
			ElderCurator elderCurator = new ElderCurator()
			{
				User = new User ()
				{
					Login = elderCuratorVM.User.Login,
					Password = elderCuratorVM.User.Password,
					FirstName = elderCuratorVM.User.FirstName,
					SurName = elderCuratorVM.User.SurName,
					Role = context.Roles.SingleOrDefault(r => r.Name == elderCuratorVM.User.Role.Name),
				},
				Faculty = context.Faculties.SingleOrDefault(f => f.Id == elderCuratorVM.Faculty.Id),
			};

			context.ElderCurators.Add(elderCurator);
			context.SaveChanges();

			return 1;
		}

		public UserViewModel TryLogIn(UserViewModel user)
		{
			
			if (user != null)
			{
				User loginUser = new User()
				{
					Login = user.Login,
					Password = user.Password
				};

				// For Zubkov

				//User existingUser = null;
				//foreach (User userDB in context.Users.Include(a => a.Role))
				//{
				//	if (loginUser.Login == userDB.Login && loginUser.Password == userDB.Password)
				//	{
				//		existingUser = userDB;
				//		break;
				//	}
				//}

				// For normal work
				User existingUser = context.Users.Include(a => a.Role).FirstOrDefault(m => m.Login == loginUser.Login && m.Password == loginUser.Password);
				if (existingUser != null)
				{
					return new UserViewModel(existingUser);
				}
				
			}
			return null;

		}

		public bool RegisterUser(UserViewModel userVM)
		{
			bool success = true;
			if (userVM != null)
			{
				User newUser = new User
				{
					FirstName = userVM.FirstName,
					Login = userVM.Login,
					Password = userVM.Password,
					Role = context.Roles.SingleOrDefault(r => r.Name == userVM.Role.Name),
					SurName = userVM.SurName,
				};

				foreach (User user in context.Users.Include(r => r.Role))
				{
					if (user.Login == newUser.Login)
					{
						success = false;
						break;
					}
				}
				if (success)
				{
					context.Users.Add(newUser);
					context.SaveChanges();
				}
			}
			else
			{
				success = false;
			}
			return success;
		}

		public CuratorViewModel GetCurator(int userId)
		{
			Curator curator = context.Curators.Include(a => a.Faculty).
				Include(a => a.User).ThenInclude(a => a.ScheduledEvents).
				Include(a => a.User).ThenInclude(a => a.Role).
				FirstOrDefault(u => u.User.Id == userId);
			if (curator != null)
			{
				return new CuratorViewModel(curator);
			}
			return null;
			
		}

		public ElderCuratorViewModel GetElder(int userId)
		{
			ElderCurator elder = context.ElderCurators.Include(a => a.Faculty).
				Include(a => a.User).ThenInclude(a => a.Role).
				FirstOrDefault(u => u.User.Id == userId);
			if (elder != null)
			{
				return new ElderCuratorViewModel(elder);
			}
			return null;
		}
	}
}
