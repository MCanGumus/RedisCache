using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace Distrubuted.Caching.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IDistributedCache _distrubutedCache;

        public ValuesController(IDistributedCache distrubutedCache)
        {
            _distrubutedCache = distrubutedCache; 
        }

        [HttpGet("set")]
        public async Task<IActionResult> Set(string name, string surname)
        {
            await _distrubutedCache.SetStringAsync("name", name, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5),
                
            });


            await _distrubutedCache.SetAsync("surname", Encoding.UTF8.GetBytes(surname), options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5),
            });

            return Ok();
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var name = await _distrubutedCache.GetStringAsync("name");

            var surnameBinary = await _distrubutedCache.GetAsync("surname");
            var surname = Encoding.UTF8.GetString(surnameBinary);

            return Ok(new
            {
                name,
                surname,
                surnameBinary
            });
        }
    }
}
