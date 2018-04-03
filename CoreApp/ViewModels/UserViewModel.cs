using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;

namespace CoreApp.ViewModels
{
    public class UserViewModel
    {
		public int Id { set; get; }
		public string Login { set; get; }
		public string Password { set; get; }
		public string FirstName { set; get; }
		public string SurName { set; get; }
		public bool IsAdmin { set; get; }

		public RoleViewModel Role { set; get; }

		public UserViewModel(User user)
		{
			if (user != null)
			{
				Id = user.Id;
				Login = user.Login;
				Password = user.Password;
				FirstName = user.FirstName;
				SurName = user.SurName;
				IsAdmin = user.IsAdmin;
				Role = new RoleViewModel(user.Role);
			}			
		}
		
    }
}
