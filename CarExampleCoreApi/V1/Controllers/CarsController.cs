using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarExampleCoreApi.V1.Model;

namespace CarExampleCoreApi.V1.Controllers
{
    //[Authorize]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        static List<Car> cars = null;
        public CarsController()
        {
            if (cars != null) return;
            cars = new List<Car>()
            {
                new Car(){ ID=0, Manufacturer="Ford", Model="Focus"},
                new Car(){ ID=1, Manufacturer="Ford", Model="Kuga"},
                new Car(){ ID=2, Manufacturer="Nissan", Model="Micra"},
                new Car(){ ID=3, Manufacturer="Nissan", Model="Qashqai"}
            };
        }

        // GET api/car
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Car>), 200)]
        public IActionResult Get()
        {
            return Ok(cars);
        }

        // GET api/cars/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Car), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            Car result = null;
            try
            {
                result = cars[id];

            }
            catch (ArgumentOutOfRangeException ex)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // DELETE api/cars/5
        [Authorize(Policy = "Admin")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            try
            {
                cars.RemoveAt(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
