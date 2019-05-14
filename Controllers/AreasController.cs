using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            new Rectangle(){Id= 2, Name= "BB", Width= 2, Length= 2, Area= 4, Unit= "meters"},
            new Rectangle(){Id= 3, Name= "CC", Width= 3, Length= 3, Area= 9, Unit= "feet"},
            new Rectangle(){Id= 3, Name= "DD", Width= 4, Length= 5, Area= 20, Unit= "feet"}
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
            Console.WriteLine("BIZOUNE");
            // If no search term, return all rectangles
            if (term == null) return Ok(_rectanglesList);

            int value;

            if (int.TryParse(term, out value)) {
                var result = _rectanglesList.Where(predicate: rectangle => this.IntCondition(rectangle, value));
                return Ok(result);
            } else if (this.PrefixCondition(term))  {
                string prefix = term.Substring(0, 1);
                string numberStr = term.Remove(0, 1);
                if (term.Length > 1) {
                    int number = Convert.ToInt32(numberStr);
                    string measure = this.GetMeasure(prefix);
                    PropertyInfo property = typeof(Rectangle).GetProperty(measure);
                    
                    var result = _rectanglesList.Where(rectangle => rectangle.Width == number || rectangle.Length == number );
                    // var result = _rectanglesList.Where((rectangle) => {
                    //     var something = property.GetValue(rectangle);
                    //     // Console.WriteLine(measure);
                    //     // return true;
                    //     });
                    return Ok(result);
                }   else  {
                var result = _rectanglesList.Where(rectangle => rectangle.Name.Contains(term));
                return Ok(result);
                }
            } else  {
                // var result = _rectanglesList.Where(rectangle => rectangle.Name.Contains(term));
                return Ok(_rectanglesList);
            } 
        }


        private Boolean IntCondition(Rectangle rectangle, int value) {
            return  rectangle.Width == value || rectangle.Length == value || rectangle.Area == value;
        }
        private Boolean PrefixCondition(string term) {
                        Console.WriteLine(value: term);
            string lowerTerm = term.ToLower();
            return lowerTerm.StartsWith("l") || lowerTerm.StartsWith("w") || lowerTerm.StartsWith("a");
        }

        private String GetMeasure(string prefix) {
            string lowerPrefix = prefix.ToLower();
            switch (lowerPrefix)
            {
                case "l":
                    return "Length";
                case "w":
                    return "Width";
                case "a":
                    return "Area";
                default:
                    return "";
            }
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
