using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class Faculty
    {
		public int Id { set; get; }
		public string Name { set; get; }

		public List<User> Users { set; get; }

		public Faculty()
		{
			Users = new List<User>();
		}
    }
}
