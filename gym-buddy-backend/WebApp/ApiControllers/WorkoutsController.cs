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
    /// API controller to deal with workouts
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkoutsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly WorkoutMapper _mapper;

        /// <summary>
        /// Constructor for the controller
        /// </summary>
        /// <param name="bll">App BLL</param>
        /// <param name="mapper">Mapper between API dto and BLL dto</param>
        public WorkoutsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new WorkoutMapper(mapper);
        }

        // GET: api/Workouts
        /// <summary>
        /// Get all workouts without exercises in them
        /// </summary>
        /// <returns>List of workouts</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicAPI.DTO.v1.WorkoutSimple>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicAPI.DTO.v1.WorkoutSimple>>> GetWorkouts()
        {
            return (await _bll.Workouts.GetAllAsync())
                .Select(x => _mapper.MapSimple(x)!)
                .ToList();
        }

        // GET: api/Workouts/5
        /// <summary>
        /// Get workout by id. Workout has exercises included
        /// </summary>
        /// <param name="id">Id of the workout to retrieve</param>
        /// <returns>Workout with exercises</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Workout), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicAPI.DTO.v1.Workout>> GetWorkout(int id)
        {
            var workout = _mapper.Map(await _bll.Workouts.FirstOrDefaultWithExercisesAsync(id));

            return workout == null ? NotFound() : workout;
        }

        // PUT: api/Workouts/5
        /// <summary>
        /// Update workout.
        /// </summary>
        /// <param name="id">Id of the workout to be updated</param>
        /// <param name="workout">Workout dto with updated information</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutWorkout(int id, PublicAPI.DTO.v1.WorkoutSimple workout)
        {
            if (id != workout.Id) return BadRequest();

            _bll.Workouts.Update(_mapper.MapSimple(workout)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Workouts
        /// <summary>
        /// Create workout
        /// </summary>
        /// <param name="workout">Workout dto that needs creation</param>
        /// <returns>Newly created workout</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.WorkoutSimple), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicAPI.DTO.v1.WorkoutSimple>> PostWorkout(PublicAPI.DTO.v1.WorkoutSimple workout)
        {
            var bllEntity = _mapper.MapSimple(workout)!;
            _bll.Workouts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _mapper.Map(_bll.Workouts.GetUpdatedEntityAfterSaveChanges(bllEntity));

            return CreatedAtAction("GetWorkout", new {id = updatedEntity!.Id}, updatedEntity);
        }

        // DELETE: api/Workouts/5
        /// <summary>
        /// Delete workout by id
        /// </summary>
        /// <param name="id">Id of the workout to be deleted</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var workout = await _bll.Workouts.FirstOrDefaultAsync(id);
            if (workout == null) return NotFound();

            await _bll.Workouts.RemoveAsync(workout.Id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}