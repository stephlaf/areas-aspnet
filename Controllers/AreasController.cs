using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using MvcRectangle.Models;

namespace AreaCalculatorRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AreaCalculator")]
    public class AreasController : ControllerBase
    {
        // Static list of rectangles
        private static List<Rectangle> _rectanglesList = new List<Rectangle>()
        {
            new Rectangle(){Id= 1, Name= "AA", Width= 1, Length= 1, Area= 1, Unit= "meters"},
            new Rectangle(){Id= 2, Name= "BB", Width= 2, Length= 2, Area= 2, Unit= "meters"},
            new Rectangle(){Id= 3, Name= "CC", Width= 3, Length= 3, Area= 3, Unit= "feet"}
        }

        // GET api/areas
        [HttpGet]
        public ActionResult<IEnumerable<List<Rectangle>> Get()
        {
            // return new double[] { 10.44, 44 };
            return _rectanglesList;
        }

        // GET api/areas/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/areas
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/areas/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/areas/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
