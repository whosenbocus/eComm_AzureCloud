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
    public class QueueController : ControllerBase
    {
        private readonly IQueueRepository queue;
        public QueueController(IQueueRepository _queue)
        {
            queue = _queue;
        }

        [HttpPost]
        public async Task<ActionResult> SaveMessage(string message)
        {
            queue.Save(message);
            return Ok();
        }

    }
}