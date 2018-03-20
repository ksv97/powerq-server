using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreApp.Repositories.FeedbackFormRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApp.Controllers
{

    [Route("api/[controller]")]
    public class FeedbackFormsController : Controller
    {
		private FeedbackFormRepository repository;

		public FeedbackFormsController(FeedbackFormRepository repository)
		{
			this.repository = repository;
		}

        // GET: api/<controller>
        [HttpGet]
        public IActionResult GetAllFeedbackForms()
        {
			return Ok(repository.GetAllFeedbackForms());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
