using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreApp.ViewModels;
using CoreApp.Repositories.EventsRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApp.Controllers
{
    [Route("api/[controller]")]
	[Produces("application/json")]
	public class EventsController : Controller
    {
		private readonly IEventRepository repository;

		public EventsController(IEventRepository repository)
		{
			this.repository = repository;
		}

        [HttpPost]
		[Route("create")]
		public IActionResult CreateEvent ([FromBody]ScheduleEventViewModel viewModel)
		{
			int? result = this.repository.CreateScheduleEvent(viewModel);
			if (result >= 0)
			{
				return Ok(result);
			}

			return BadRequest();
		}

		[HttpGet]
		public IActionResult GetAllScheduleEventsAssignedToUser (int userId)
		{
			var result = repository.GetAllScheduleEvents(userId);
			if (result != null)
			{
				return Ok(result);
			}
			else return NotFound();
		}

		[HttpPost]
		[Route("update")]
		public IActionResult UpdateScheduleEvent ([FromBody] ScheduleEventViewModel viewModel)
		{
			var result = repository.UpdateScheduleEvent(viewModel);
			if (result > 0 && result != null)
			{
				return Ok(result);
			}
			return BadRequest();
		}

		[HttpPost]
		[Route("delete")]
		public IActionResult DeleteScheduleEvent ([FromBody]int id)
		{
			var result = repository.DeleteScheduleEvent(id);
			if (result > 0 && result != null)
			{
				return Ok(result);
			}
			return BadRequest();
		}
    }
}
