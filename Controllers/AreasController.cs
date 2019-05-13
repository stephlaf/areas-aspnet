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
        };

        // GET api/areas/5
        [HttpGet("{id}")]
        public ActionResult<Rectangle> Get(int id)
        {
            var result = _rectanglesList.FirstOrDefault(rectangle => rectangle.Id == id);
            if (result == null) return NotFound($"Rectangle with Id {id} was not Found"); 
            return Ok(result);
        }

        //  GET api/areas
        //  GET api/areas/?term=aa
        [HttpGet()]
        public ActionResult<Rectangle> Get([FromQuery(Name = "term")] string term)
        {
            // If no search term, return all rectangles
            if (term == null) return Ok(_rectanglesList);

            int value;

            if (int.TryParse(term, out value)) {
                var result = _rectanglesList.Where(rectangle => rectangle.Width == value || rectangle.Length == value);
                return Ok(result);
            } else  {
                var result = _rectanglesList.Where(rectangle => rectangle.Name.Contains(term));
                return Ok(result);
            }
            //  else {
            //     return NotFound("No Rectangle contains {term}"); 
            // }

        }

        // POST api/areas
        [HttpPost]
        public void Post([FromBody] Rectangle rectangle)
        {
            _rectanglesList.Add(rectangle);
        }

        // PUT api/areas/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Rectangle updatedRectangle)
        {
            var result = _rectanglesList.FirstOrDefault(rectangle => rectangle.Id == id);
            result.Name = updatedRectangle.Name;
            result.Length = updatedRectangle.Length;
            result.Width = updatedRectangle.Width;
            result.Area = updatedRectangle.Id;
            result.Unit = updatedRectangle.Unit;
        }

        // DELETE api/areas/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
          var rectToDelete = _rectanglesList.FirstOrDefault(rectangle => rectangle.Id == id);
          _rectanglesList.Remove(rectToDelete);
        }
    }
}
