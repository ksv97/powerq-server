using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;
using CoreApp.Models;

namespace CoreApp.Repositories.RolesRepository
{
	public class RolesRepository : IRolesRepository
	{
		private readonly Context context;

		public RolesRepository(Context context)
		{
			this.context = context;
		}

		public List<RoleViewModel> GetAllRoles()
		{
			var roles = context.Roles.ToList();

			List<RoleViewModel> result = new List<RoleViewModel>();

			foreach (var role in roles)
			{
				result.Add(new RoleViewModel(role));
			}

			return result;
		}
	}
}
