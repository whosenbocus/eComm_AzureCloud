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
    public class CacheController : ControllerBase
    {
        private readonly ICacheRepository cache; 
        public CacheController(ICacheRepository _cache)
        {
            cache = _cache;
        }
        [HttpGet]
        public async Task<ActionResult<CacheObject>> GetCache(string metadata)
        {
            var message = cache.GetCache(metadata);
            return Ok(message);
        }

        [HttpPost]
        public async Task<ActionResult> SaveCache(CacheObject message)
        {
            cache.SaveCache(message,message.Metadata);
            return Ok();
        }

    }
}
