using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Models;

namespace CoreApp.ViewModels
{
    public class RoleViewModel
    {
		public int Id { set; get; }
		public string Name { set; get; }

		public RoleViewModel(Role role)
		{
			 if (role != null)
			{
				Id = role.Id;
				Name = role.Name;
			}
		}
    }
}
