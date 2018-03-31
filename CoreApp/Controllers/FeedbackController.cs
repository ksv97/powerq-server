using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CoreApp.Repositories.FeedbackRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApp.Controllers
{
    [Route("api/[controller]")]
    public class FeedbackController : Controller
    {
		private IFeedbackRepository repository;

		public FeedbackController(IFeedbackRepository repo)
		{
			this.repository = repo;
		}

        // GET: api/<controller>
        [HttpGet("all")]
        public IActionResult GetAllFeedbacks()
        {
			List<FeedbackViewModel> allFeedbacks = this.repository.GetAllFeedbacks();
			if (allFeedbacks != null)
			{
				return Ok(allFeedbacks);
			}
			return BadRequest();
        }

        // POST api/<controller>
        [HttpPost("create")]
        public IActionResult CreateFeedback([FromBody]FeedbackViewModel viewModel)
        {
			int? result = this.repository.CreateFeedback(viewModel);
			if (result >= 0)
			{
				return Ok(result);
			}

			return BadRequest();
		}

		[HttpPost("update")]
		public IActionResult UpdateFeedback([FromBody]FeedbackViewModel viewModel)
		{
			if (viewModel != null)
			{
				int? result = this.repository.UpdateFeedback(viewModel);
				if (result >= 0)
				{
					return Ok(result);
				}
			}
			return BadRequest();
		}

		[HttpPost("delete")]
		public IActionResult DeleteFeedback([FromBody]DeleteFeedbackViewModel deleteModel)
		{
			if (deleteModel != null)
			{
				int? result = this.repository.DeleteFeedback(deleteModel);
				if (result >= 0)
				{
					return Ok(result);
				}
			}
			return BadRequest();
		}
	}
}
