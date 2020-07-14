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
    public class BlobManagementController : ControllerBase
    {
        private readonly IBlobRepository blob; 
        public BlobManagementController(IBlobRepository _blob)
        {
            blob = _blob;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetBlob()
        {
            var blobs = blob.RetrieveBlob();
            return Ok(blobs);
        }

        [HttpPost]
        public async Task<ActionResult> SaveBlob(string url)
        {
            blob.SaveBlob(url);
            return Ok();
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteBlob()
        {
            blob.DeleteBlob();
            return Ok();
        }

    }
}
