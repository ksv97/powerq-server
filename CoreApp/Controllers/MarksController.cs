using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreApp.ViewModels;
using CoreApp.Repositories.MarkRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApp.Controllers
{
    [Route("api/[controller]")]
    public class MarksController : Controller
    {
		private IMarkRepository repository;

		public MarksController(IMarkRepository repository)
		{
			this.repository = repository;
		}

		[HttpPost("add")]
		public IActionResult AddMark ([FromBody]FeedbackViewModel feedback)
		{
			var result = this.repository.UpdateMark(feedback);
			if (result != null)
			{
				return Ok(result);
			}
			return BadRequest();
		}
    }
}
