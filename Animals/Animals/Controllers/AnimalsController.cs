using Animals.DTOs;
using Animals.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Animals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {

        IDatabaseService _IDatabaseService;

        public AnimalsController(IDatabaseService iDatabaseService)
        {
            _IDatabaseService = iDatabaseService;
        }

        [HttpGet]
        public IActionResult ShowListOfAnimals([FromQuery] string orderBy = "Name")
        {

            try
            {
                var animalList = _IDatabaseService.ShowAllAnimals(orderBy);
            }
            catch (ArgumentException e) {
            
            return BadRequest(e.Message);
            
            }
            return Ok(_IDatabaseService.ShowAllAnimals(orderBy));
        }

   
       
        [HttpPost]
        public IActionResult Post([FromBody]Animal animal)
        {

            _IDatabaseService.AddNewAnimal(animal);
            return Ok();
        }

    
        [HttpPut("{idAnimal}")]
        public IActionResult Put(int idAnimal, [FromBody] Animal animal)
        {
            try
            {
                _IDatabaseService.UpdateAnimal(animal, idAnimal);
            }
            catch (ArgumentException e) {

                return BadRequest(e.Message);
            }
            return Ok();
        }


        [HttpDelete("{idAnimal}")]
        public IActionResult Delete(int idAnimal)
        {
            try
            {
                _IDatabaseService.DeleteAnimal(idAnimal);
                
            }
            catch (ArgumentException e) {

                return BadRequest(e.Message);
          
            }

            return Ok();
        
        }
    }
}
