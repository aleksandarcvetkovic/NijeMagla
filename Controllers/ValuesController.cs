﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nije_Magla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly INijeMaglaService sensorService;

        public ValuesController(INijeMaglaService sensorService)
        {
            this.sensorService = sensorService;
        }


        [HttpGet()]
        public ActionResult<List<PolutionListItem>> GetSortedList()//vraca poslednju vrednost
        {
            var sortedPoltionList = sensorService.GetSortedPolution();
            if (sortedPoltionList == null)
            {
                return NotFound($"There is no measurements for any sensor");
            }

            return sortedPoltionList;

        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<Measurement> Get(string id)//vraca poslednju vrednost
        {
            var merenje = sensorService.ReadMeasurement(id);
            if(merenje == null)
            {
                return NotFound($"There is no measurements for sensor Id = {id}");
            }

            return merenje;

        }

        [HttpGet("{id}/{n}")]
        public ActionResult<List<Measurement>> Get(string id, int n)//vraca poslednju vrednost
        {
            var merenja = sensorService.GetMeasurements(id, n);
            if (merenja == null)
            {
                return NotFound($"There is no measurements for sensor Id = {id}");
            }

            return merenja;

        }

        [HttpGet("Average{id}")]
        public ActionResult<int> GetAverage(string id)//vraca poslednju vrednost
        {
            var merenje = sensorService.GetAverageMeasurement(id);
            if (merenje == null)
            {
                return NotFound($"There is no measurements for sensor Id = {id}");
            }

            return merenje;

        }

        // POST api/<ValuesController>
        [HttpPost]
        public ActionResult Post([FromBody] Measurement value)
        {
            sensorService.AddMeasurement(value.IdSenzora, value);

            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
        }

        

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)//Izbrisi podatke senzora
        {
            sensorService.DeleteSensorData(id);
        }
    }
}
