using back_end.Models;
using back_end.Models.Dtos;
using back_end.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        private readonly IVisitService _visitService;

        public VisitsController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                Visit? visit = await _visitService.Get(id);
                if (visit is null)
                {
                    return NotFound();
                }

                return Ok(visit);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll([FromQuery] string? animalName, [FromQuery] string? ownerName, [FromQuery] string? animalType)
        {
            return Ok(_visitService.GetAll(v => (v.Animal.Name == animalName || animalName == null) &&
                                                (v.Animal.OwnerName == ownerName || ownerName == null) &&
                                                (v.Animal.AnimalType.Name == animalType || animalType == null)));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] VisitDto visit)
        {
            try
            {
                await _visitService.Add(visit);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateVisitRequestBody body)
        {
            try
            {
                await _visitService.Update(body.VisitId, body.Visit);
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
            var result = await _visitService.Delete(id);
            return result ? Ok() : NotFound($"Visit with id {id} does not exist");
        }
    }
}
