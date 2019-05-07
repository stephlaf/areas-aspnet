using System;
using System.ComponentModel.DataAnnotations;

namespace MvcRectangle.Models
{
    public class Rectangle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Area { get; set; }
        public string Unit { get; set; }
    }
}