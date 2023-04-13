using D01_Task.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_D02_Task2.Models;
using System.Reflection;

namespace D01_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LogActionFilter))]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> logger;
        public CarController(ILogger<CarController> logger)
        {
            this.logger = logger;
        }
        [HttpGet]
        [Route("counreq")]
        public IActionResult Index()
        {
           LogActionFilter.counter--;
           var count = LogActionFilter.counter;
           return Ok(count);
        }
        [HttpGet]
        public ActionResult<List<Car>> GetAll()
        {
            logger.LogCritical("Test");
            return CarList.cars;
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<List<Car>> GetCarById(int id)
        {

            logger.LogInformation($"Received request to get Example with ID {id}");
            var car = CarList.cars.FirstOrDefault(e => e.id == id);
            if (car == null) { return NotFound(); }
            return Ok(car);
        }
        [HttpPost]
        [Route("v1")]
        public ActionResult AddV1(Car car)
        {
            car.id = new Random().Next(10, 1000);
            car.Type = "Gas";
            CarList.cars.Add(car);
            return CreatedAtAction(
                actionName: nameof(GetCarById),
                routeValues: new { id = car.id },
                value: new { Message = "car has been added successfully." });
        }
        [HttpPost]
        [Route("v2")]
        [ServiceFilter(typeof (ValidateCarTypeAttribute))]
        public ActionResult AddV2(Car car)
        {
            car.id = new Random().Next(10, 1000);
            CarList.cars.Add(car);
            //allows us to set Location URI of the newly created
            //resource by specifying the name of an action where we
            //can retrieve our resource.
            return CreatedAtAction(
                actionName: nameof(GetCarById),
                routeValues: new { id = car.id },
                value: new { Message = "car has been added successfully." });
        }
        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(Car car, int id)
        {
            if (id != car.id)
            {
                return BadRequest();
            }

            var carToUpdate = CarList.cars.FirstOrDefault(m => m.id == id);
            if (carToUpdate is null)
            {
                return NotFound();
            }
            logger.LogCritical("Test");
            carToUpdate.Color = car.Color;
            carToUpdate.Model = car.Model;
            carToUpdate.ProductionDate = car.ProductionDate;

            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteById(int id)
        {

            var carToDelete = CarList.cars.FirstOrDefault(m => m.id == id);
            if (carToDelete is null)
            {
                return NotFound();
            }

            CarList.cars.Remove(carToDelete);
            return NoContent();
        }
    }
}
