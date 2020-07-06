using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Product.API.Infrastructure.Repository;
using Product.API.Model;

namespace Product.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class QueueManagementController : ControllerBase
    {
        private readonly IQueueRepository queue;
        public QueueManagementController(IQueueRepository _queue)
        {
            queue = _queue;
        }
        //POST - Pass URL and detail
        // Save into Storage Queue
        // AZ function download and save image in container, insert record in SQL and update cache

        //GET - Get first message in queue

        //PUT - Change content of queue

        //DEL - Delete message from queue



        //DEL - Delete Image
        //Remove from Storage Container and re update cache
        //check if still in queue and remove


        //GET - Get all Details
        //Get information from Cache
        [HttpGet]
        public async Task<ActionResult<QueueMessage>> GetMessage()
        {
            var message = await queue.GetMessage();
            return Ok(message);
        }



        [HttpPut]
        public async Task<ActionResult> UpdateQueue(QueueMessage message)
        {
            await queue.UpdateMessage(message);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> SaveQueue(QueueMessage message)
        {
            await queue.SaveMessage(message);
            return Ok();
        }
    }
}
