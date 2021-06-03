using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PublicAPI.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller to deal with exercises.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExercisesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ExerciseMapper _mapper;

        /// <summary>
        /// Constructor for controller.
        /// </summary>
        /// <param name="bll">App BLL</param>
        /// <param name="mapper">Mapper between API dto and BLL dto</param>
        public ExercisesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new ExerciseMapper(mapper);
        }

        // GET: api/Exercises
        /// <summary>
        /// Get all exercises without muscles trained
        /// </summary>
        /// <returns>List of exercises with minimal information</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicAPI.DTO.v1.ExerciseSimple>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicAPI.DTO.v1.ExerciseSimple>>> GetExercises()
        {
            return (await _bll.Exercises.GetAllAsync())
                .Select(x => _mapper.MapSimple(x)!)
                .ToList();
        }

        // GET: api/Exercises/5
        /// <summary>
        /// Get exercises by id. Exercise has included trained muscles.
        /// </summary>
        /// <param name="id">Id of the exercises to get</param>
        /// <returns>Exercise with trained muscles</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Exercise), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<PublicAPI.DTO.v1.Exercise>> GetExercise(int id)
        {
            var exercise = _mapper.Map(await _bll.Exercises.FirstOrDefaultWithMusclesAsync(id));
            return exercise == null ? NotFound() : exercise;
        }

        // PUT: api/Exercises/5
        /// <summary>
        /// Update exercise
        /// </summary>
        /// <param name="id">Id of the exercise to update</param>
        /// <param name="exercise">Exercise dto with new information</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutExercise(int id, PublicAPI.DTO.v1.ExerciseSimple exercise)
        {
            if (id != exercise.Id) return BadRequest();

            _bll.Exercises.Update(_mapper.MapSimple(exercise)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Exercises
        /// <summary>
        /// Create new exercise
        /// </summary>
        /// <param name="exercise">Exercise dto of the new exercise</param>
        /// <returns>Newly created exercise with id</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.ExerciseSimple), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicAPI.DTO.v1.ExerciseSimple>> PostExercise(PublicAPI.DTO.v1.ExerciseSimple exercise)
        {
            var bllEntity = _mapper.MapSimple(exercise)!;
            _bll.Exercises.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _mapper.Map(_bll.Exercises.GetUpdatedEntityAfterSaveChanges(bllEntity));
            
            return CreatedAtAction("GetExercise", new {id = updatedEntity!.Id}, updatedEntity);
        }

        // DELETE: api/Exercises/5
        /// <summary>
        /// Delete an exercise
        /// </summary>
        /// <param name="id">Id of exercise to be deleted</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var exercise = await _bll.Exercises.FirstOrDefaultAsync(id);
            if (exercise == null) return NotFound();

            await _bll.Exercises.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}