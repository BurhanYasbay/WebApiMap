using Microsoft.AspNetCore.Mvc;
using wepapimap1.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace wepapimap1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private const string jsonFilePath = "points.json";

        // GET: api/points
        [HttpGet]
        public IActionResult GetPoints()
        {
            if (!System.IO.File.Exists(jsonFilePath))
            {
                return NotFound();
            }

            string json = System.IO.File.ReadAllText(jsonFilePath);
            List<Point> points = JsonConvert.DeserializeObject<List<Point>>(json);
            return Ok(points);
        }

        // POST: api/points
        [HttpPost]
        public IActionResult AddPoint([FromBody] Point point)
        {
            List<Point> points;

            if (System.IO.File.Exists(jsonFilePath))
            {
                string json = System.IO.File.ReadAllText(jsonFilePath);
                points = JsonConvert.DeserializeObject<List<Point>>(json);
            }
            else
            {
                points = new List<Point>();
            }

            points.Add(point);
            string updatedJson = JsonConvert.SerializeObject(points);
            System.IO.File.WriteAllText(jsonFilePath, updatedJson);
            return CreatedAtAction(nameof(GetPoints), points);
        }
    }
}
