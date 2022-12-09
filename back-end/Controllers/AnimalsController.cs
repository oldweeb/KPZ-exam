using back_end.Models;
using back_end.Models.Dtos;
using back_end.Services;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                Animal? animal = await _animalService.Get(id);
                if (animal is null)
                {
                    return NotFound();
                }

                return Ok(animal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll([FromQuery] string? ownerName)
        {
            if (ownerName == null)
            {
                return Ok(_animalService.GetAll());
            }

            return Ok(_animalService.GetAll(a => a.OwnerName == ownerName));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AnimalDto animal)
        {
            try
            {
                await _animalService.Add(animal);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("type")]
        public async Task<IActionResult> AddType([FromBody] AnimalTypeDto type)
        {
            try
            {
                await _animalService.AddType(type.Name);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAnimalRequestBody body)
        {
            try
            {
                await _animalService.Update(body.AnimalId, body.Animal);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _animalService.Delete(id);
            return result ? Ok() : NotFound($"Animal with id {id} does not exist");
        }
    }
}
