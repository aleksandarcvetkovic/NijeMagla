using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nije_Magla_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly INijeMaglaService sensorService;

        public SensorsController(INijeMaglaService sensorService) 
        {
            this.sensorService = sensorService;
        }

        // GET: api/<SensorsController>
        [HttpGet]
        public ActionResult<List<Sensor>> Get()
        {

            return sensorService.GetAllSensors();
        }

        // GET api/<SensorsController>/5
        [HttpGet("{id}")]
        public ActionResult<Sensor> Get(string id)
        {
            var sensor = sensorService.GetSensor(id);

            if(sensor == null)
            {
                return NotFound($"Student with Id = {id} not found");

            }
            return sensor;
        }

        // POST api/<SensorsController>
        [HttpPost]
        public ActionResult<Sensor> Post([FromBody] Sensor sensor)
        {
            sensorService.CreateSensor(sensor);

            return CreatedAtAction(nameof(Get), new { id = sensor.Id }, sensor);
        }

        // PUT api/<SensorsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Sensor sensor)
        {
            var existingSensor = sensorService.GetSensor(id);

            if(existingSensor == null)
            {
                return NotFound($"Sensor with Id= {id} not found");
            }
            sensorService.UpdateSensor(id, sensor);

            return NoContent();
        }

        // DELETE api/<SensorsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {

            var sensor = sensorService.GetSensor(id); 

            if (sensor == null)
            {
                return NotFound($"Sensor with Id = {id} not found");
            }
            sensorService.RemoveSensor(sensor.Id);

            return Ok($"Sensor with Id = {id} delted");

        }
    }
}
