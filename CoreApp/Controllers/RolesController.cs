using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreApp.Repositories.RolesRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApp.Controllers
{
    [Route("api/[controller]")]
	[Produces("application/json")]
	public class RolesController : Controller
    {
		private readonly IRolesRepository repository;

		public RolesController(IRolesRepository repository)
		{
			this.repository = repository;
		}


        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
			return Ok(repository.GetAllRoles());
		}
       
    }
}
