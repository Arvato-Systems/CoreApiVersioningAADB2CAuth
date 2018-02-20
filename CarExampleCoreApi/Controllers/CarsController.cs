using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarExampleCoreApi.Model;

namespace CarExampleCoreApi.Controllers
{
    [Authorize]
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
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Car), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            Car result = null;
            try
            {
                result = cars[id];
                
            }catch(ArgumentOutOfRangeException ex)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // DELETE api/cars/5
        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
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
