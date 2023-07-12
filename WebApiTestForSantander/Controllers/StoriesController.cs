using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiTestForSantander.Infrastructure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApiTestForSantander.Controllers
{
    [Route("api/[controller]")]
    public class StoriesController : ControllerBase
    {
        private IStoriesRequestorService _service;

        public StoriesController(StoriesRequestorService service)
        {
            _service = service;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var result = await _service.GetCountOfAllStories();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();           }
        }

        // GET api/values/5
        [HttpGet("{number}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<string>> GetNumberOfStories(int number)
        {
            if (number<1)
            {
                return BadRequest();
            }

            //var result =  await _service.GetNumberOfStoriesAsync(number);
            var result =  await _service.GetNumberOfStoriesParrallel(number);

            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}

