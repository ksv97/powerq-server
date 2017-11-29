using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;

namespace CoreApp.Repositories.RolesRepository
{
    public interface IRolesRepository
    {
		List<RoleViewModel> GetAllRoles();
    }
}
