using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class Role
    {
		public int Id { set; get; }
		public string Name { set; get; }

		public List<User> Users { set; get; }

		public Role()
		{
			Users = new List<User>();
		}
    }
}
