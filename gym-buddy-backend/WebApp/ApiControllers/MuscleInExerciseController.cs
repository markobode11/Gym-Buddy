using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Controller to add or remove muscles to exercise
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MuscleInExerciseController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;


        /// <summary>
        /// Constructor for the controller
        /// </summary>
        /// <param name="bll">App bll</param>
        /// <param name="mapper">Mapper between API dto and BLL dto</param>
        public MuscleInExerciseController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to return MuscleInExercise dto that was created in the POST method
        /// </summary>
        /// <param name="id">Id of the MuscleInExercise</param>
        /// <returns>MuscleInExercise dto</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.MuscleInExercise), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicAPI.DTO.v1.MuscleInExercise>> GetMuscleInExercise(int id)
        {
            var res = _mapper.Map<PublicAPI.DTO.v1.MuscleInExercise>(
                await _bll.MuscleInExercises.FirstOrDefaultAsync(id));
            return res == null ? NotFound() : res;
        }

        // POST: api/MuscleInExercise
        /// <summary>
        /// Add a muscle to a workout
        /// </summary>
        /// <param name="muscleInExercise">Dto to clarify muscleId and workoutId</param>
        /// <returns>Newly created MuscleInExercise dto</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.MuscleInExercise), StatusCodes.Status201Created)]
        public async Task<ActionResult<PublicAPI.DTO.v1.MuscleInExercise>> PostMuscleInExercise(
            PublicAPI.DTO.v1.MuscleInExercise muscleInExercise)
        {
            var bllEntity = _mapper.Map<BLL.App.DTO.MuscleInExercise>(muscleInExercise)!;
            _bll.MuscleInExercises.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity =
                _mapper.Map<PublicAPI.DTO.v1.MuscleInExercise>(
                    _bll.MuscleInExercises.GetUpdatedEntityAfterSaveChanges(bllEntity));

            return CreatedAtAction("GetMuscleInExercise", new {id = updatedEntity!.Id}, updatedEntity);
        }

        // DELETE: api/MuscleInExercise/5
        /// <summary>
        /// Remove muscle from exercise
        /// </summary>
        /// <param name="muscleId">Id of the muscle</param>
        /// <param name="exerciseId">Id of the exercise</param>
        /// <returns></returns>
        [HttpDelete("{muscleId}/{exerciseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMuscleInExercise(int muscleId, int exerciseId)
        {
            var muscleInExercise =
                await _bll.MuscleInExercises.FirstOrDefaultByMuscleIdAndExerciseId(muscleId, exerciseId);
            if (muscleInExercise == null) return NotFound();

            await _bll.MuscleInExercises.RemoveAsync(muscleInExercise.Id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}