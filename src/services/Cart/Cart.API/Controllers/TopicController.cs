using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopic topic;
        public TopicController(ITopic _topic)
        {
            topic = _topic;
        }

        [HttpPost]
        public async Task<ActionResult> SaveMessage(string message)
        {
            topic.save(message);
            return Ok();
        }

    }
}