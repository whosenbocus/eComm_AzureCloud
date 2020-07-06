using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Product.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BlobManagementController : ControllerBase
    {
        //POST - Pass URL and detail
        // Save into Storage Queue
        // AZ function download and save image in container, insert record in SQL and update cache


        //DEL - Delete Image
        //Remove from Storage Container and re update cache
        //check if still in queue and remove


        //GET - Get all Details
        //Get information from Cache


    }
}
