using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller to deal with exercises in workouts
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ExerciseInWorkoutController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the controller
        /// </summary>
        /// <param name="bll">App bll</param>
        /// <param name="mapper">Mapper between API dto and BLL dto</param>
        public ExerciseInWorkoutController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/ExerciseInWorkout/5
        /// <summary>
        /// Method to return ExerciseInWorkout dto that was created in the POST method
        /// </summary>
        /// <param name="id">Id of the ExerciseInWorkout</param>
        /// <returns>ExerciseInWorkout dto</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.ExerciseInWorkout), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicAPI.DTO.v1.ExerciseInWorkout>> GetExerciseInWorkout(int id)
        {
            var res = _mapper.Map<PublicAPI.DTO.v1.ExerciseInWorkout>(
                await _bll.ExerciseInWorkouts.FirstOrDefaultAsync(id));
            return res == null ? NotFound() : res;
        }

        // POST: api/ExerciseInWorkout
        /// <summary>
        /// Put an exercise in a workout
        /// </summary>
        /// <param name="exerciseInWorkout">Dto to clarify exercise and workout</param>
        /// <returns>Newly created ExerciseInWorkout dto</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.ExerciseInWorkout), StatusCodes.Status201Created)]
        public async Task<ActionResult<PublicAPI.DTO.v1.ExerciseInWorkout>> PostExerciseInWorkout(
            PublicAPI.DTO.v1.ExerciseInWorkout exerciseInWorkout)
        {
            var bllEntity = _mapper.Map<BLL.App.DTO.ExerciseInWorkout>(exerciseInWorkout)!;
            _bll.ExerciseInWorkouts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity =
                _mapper.Map<PublicAPI.DTO.v1.ExerciseInWorkout>(
                    _bll.ExerciseInWorkouts.GetUpdatedEntityAfterSaveChanges(bllEntity));


            return CreatedAtAction("GetExerciseInWorkout", new {id = updatedEntity.Id}, updatedEntity);
        }

        // DELETE: api/ExerciseInWorkout/5
        /// <summary>
        /// Remove exercise from workout
        /// </summary>
        /// <param name="workoutId">Id of the workout</param>
        /// <param name="exerciseId">Id of the exercise</param>
        /// <returns>No content</returns>
        [HttpDelete("{workoutId}/{exerciseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExerciseInWorkout(int workoutId, int exerciseId)
        {
            var exerciseInWorkout =
                await _bll.ExerciseInWorkouts.FirstOrDefaultByWorkoutIdAndExerciseId(workoutId, exerciseId);
            if (exerciseInWorkout == null) return NotFound();

            await _bll.ExerciseInWorkouts.RemoveAsync(exerciseInWorkout.Id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}