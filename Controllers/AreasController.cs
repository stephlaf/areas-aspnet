﻿using System;
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

        // GET api/areas
        // [HttpGet]
        // public ActionResult<IEnumerable<Rectangle>> Get()
        // {
        //     return _rectanglesList;
        // }

        // GET api/areas/5
        [HttpGet("{id}")]
        public ActionResult<Rectangle> Get(int id)
        {
            var result = _rectanglesList.FirstOrDefault(rectangle => rectangle.Id == id);
            if (result == null) return NotFound($"Rectangle with Id {id} was not Found"); 
            return Ok(result);
        }

        //  GET api/areas/?name=aa
        [HttpGet()]
        public ActionResult<Rectangle> Get([FromQuery(Name = "term")] string term)
        {
            if (term == null) return Ok(_rectanglesList);

            var result = _rectanglesList.Where(rectangle => rectangle.Name == term);
            if (result == null) return NotFound($"Rectangle with Name {term} was not Found"); 
            return Ok(result);
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
