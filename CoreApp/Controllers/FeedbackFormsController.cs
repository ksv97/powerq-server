using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreApp.Repositories.FeedbackFormRepository;
using CoreApp.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApp.Controllers
{

    [Route("api/[controller]")]
    public class FeedbackFormsController : Controller
    {
		private IFeedbackFormRepository repository;

		public FeedbackFormsController(IFeedbackFormRepository repository)
		{
			this.repository = repository;
		}

        // GET: api/<controller>
        [HttpGet]
        public IActionResult GetAllFeedbackForms()
        {
			return Ok(repository.GetAllFeedbackForms());
        }

		[HttpPost]
		[Route("create")]
		public IActionResult CreateFeedbackForm ([FromBody]FeedbackFormViewModel viewModel)
		{
			int? result = this.repository.AddFeedbackForm(viewModel);
			if (result != null)
			{
				return Ok(result);
			}

			return BadRequest();
		}

		[HttpPost]
		[Route("update")]
		public IActionResult UpdateFeedbackForm([FromBody]FeedbackFormViewModel viewModel)
		{
			bool result = this.repository.UpdateFeedbackForm(viewModel);
			if (result == true)
			{
				return Ok(result);
			}

			return BadRequest();
		}

		[HttpPost]
		[Route("delete")]
		public IActionResult DeleteFeedbackForm([FromBody]int id)
		{
			bool result = this.repository.DeleteFeedbackForm(id);
			if (result == true)
			{
				return Ok(result);
			}

			return BadRequest();
		}
	}
}
