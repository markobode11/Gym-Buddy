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
    /// API controller to add and remove workouts from splits
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkoutInSplitController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the controller
        /// </summary>
        /// <param name="bll">App BLL</param>
        /// <param name="mapper">Mapper between API dto and BLL dto</param>
        public WorkoutInSplitController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/WorkoutInWorkoutInSplit/5
        /// <summary>
        /// Method to return WorkoutInSplit dto that was created in the POST method
        /// </summary>
        /// <param name="id">Id of the WorkoutInSplit</param>
        /// <returns>Requested WorkoutInSplit dto</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.WorkoutInSplit), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicAPI.DTO.v1.WorkoutInSplit>> GetWorkoutInSplit(int id)
        {
            var res = _mapper.Map<PublicAPI.DTO.v1.WorkoutInSplit>(await _bll.WorkoutInSplits.FirstOrDefaultAsync(id));
            return res == null ? NotFound() : res;
        }

        // POST: api/WorkoutInWorkoutInSplit
        /// <summary>
        /// Add a workout to a split
        /// </summary>
        /// <param name="workoutInSplit">Dto to clarify the workout and the split</param>
        /// <returns>Newly created WorkoutInSplitDto</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.WorkoutInSplit), StatusCodes.Status201Created)]
        public async Task<ActionResult<PublicAPI.DTO.v1.WorkoutInSplit>> PostWorkoutInSplit(
            PublicAPI.DTO.v1.WorkoutInSplit workoutInSplit)
        {
            var bllEntity = _mapper.Map<BLL.App.DTO.WorkoutInSplit>(workoutInSplit);
            _bll.WorkoutInSplits.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity =
                _mapper.Map<PublicAPI.DTO.v1.WorkoutInSplit>(
                    _bll.WorkoutInSplits.GetUpdatedEntityAfterSaveChanges(bllEntity));

            return CreatedAtAction("GetWorkoutInSplit", new {id = updatedEntity.Id}, updatedEntity);
        }

        // DELETE: api/WorkoutInWorkoutInSplit/5
        /// <summary>
        /// Remove workout from split
        /// </summary>
        /// <param name="workoutId">Id of the workout</param>
        /// <param name="splitId">Id of the split</param>
        /// <returns>No content</returns>
        [HttpDelete("{workoutId}/{splitId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteWorkoutInSplit(int workoutId, int splitId)
        {
            var splitInWorkout = await _bll.WorkoutInSplits.FirstOrDefaultByWorkoutIdAndSplitId(workoutId, splitId);
            if (splitInWorkout == null) return NotFound();

            await _bll.WorkoutInSplits.RemoveAsync(splitInWorkout.Id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}