	using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreApp.ViewModels;
using CoreApp.Repositories.FacultyRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApp.Controllers
{
    [Route("api/[controller]")]
	[Produces("application/json")]
    public class FacultiesController : Controller
    {
		private IFacultyRepository repository;

		public FacultiesController(IFacultyRepository repository)
		{
			this.repository = repository;
		}

        // GET: api/values
        [HttpGet("all")]
        public IActionResult GetAllFaculties()
        {
			return Ok(repository.GetAllFaculties());
        }

		[HttpGet]
		public IActionResult GetFaculty(int facultyId)
		{
			return Ok(repository.GetFaculty(facultyId));
		}

		// POST api/values
		[HttpPost]
        public IActionResult Post([FromBody]FacultyViewModel newFaculty)
        {
			bool result = repository.AddFaculty(newFaculty);
			if (result == true)
			{
				return Ok();
			}
			else return BadRequest();
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
