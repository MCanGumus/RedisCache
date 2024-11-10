using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache; 
        }

        //[HttpGet("set")]
        //public void Set(string name)
        //{
        //    _memoryCache.Set("Name", name);

        //    _memoryCache.Set("Name", name, options: new()
        //    {

        //    });
        //}

        //[HttpGet("get")]
        //public string Get()
        //{
        //    if (_memoryCache.TryGetValue<string>("Name", out string name))
        //    {
        //        return name;
        //    }
        //    else
        //    {
        //        return "Value is null";
        //    }
        //}

        [HttpGet("set")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
        }

        [HttpGet("get")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }
    }
}
