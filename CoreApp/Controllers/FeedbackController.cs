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
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
