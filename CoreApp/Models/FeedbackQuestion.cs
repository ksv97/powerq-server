using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    public class FeedbackQuestion
    {
		public int Id { set; get; }
		public string Name { set; get; }

		public int FeedbackFormId { set; get; }
		public FeedbackForm FeedbackForm { set; get; }
    }
}
