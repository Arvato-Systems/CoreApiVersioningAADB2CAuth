using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarExampleCoreApi.V2.Model;

namespace CarExampleCoreApi.V2.Controllers
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
                new Car(){ ID=0, Manufacturer="Ford", Model="Focus", Horsepower=120},
                new Car(){ ID=1, Manufacturer="Ford", Model="Kuga", Horsepower=140},
                new Car(){ ID=2, Manufacturer="Nissan", Model="Micra", Horsepower=90},
                new Car(){ ID=3, Manufacturer="Nissan", Model="Qashqai", Horsepower=120}
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
                
            }catch(ArgumentOutOfRangeException ex)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET api/cars/Ford
        [HttpGet("{manufacturer:alpha}")]
        [ProducesResponseType(typeof(IList<Car>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetByManufacturer(string manufacturer)
        {
            IList<Car> result = null;
            try
            {
                result = cars.FindAll(c => c.Manufacturer == manufacturer);
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
