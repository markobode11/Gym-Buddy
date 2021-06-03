using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PublicAPI.DTO.v1;
using PublicAPI.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller to add programs to users
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProgramsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;
        private readonly FullProgramMapper _programMapper;

        /// <summary>
        /// Constructor for the controller
        /// </summary>
        /// <param name="bll">App BLL</param>
        /// <param name="mapper">Mapper between API dto and BLL dto</param>
        public UserProgramsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
            _programMapper = new FullProgramMapper(mapper);;
        }

        // GET: api/UserPrograms/5
        /// <summary>
        /// Method to return UserProgram dto that was created in the POST method
        /// </summary>
        /// <param name="id">Id of the UserProgram</param>
        /// <returns>UserProgram dto</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.UserProgram), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicAPI.DTO.v1.UserProgram>> GetUserProgram(int id)
        {
            var response = await _bll.UserPrograms.FirstOrDefaultAsync(id, User.GetUserId()!.Value);
            return response == null ? BadRequest() : _mapper.Map<PublicAPI.DTO.v1.UserProgram>(response);
        }
        
        // GET: api/UserPrograms/all
        /// <summary>
        /// Method to return all UserPrograms for the current user.
        /// </summary>
        /// <returns>Current users programs</returns>
        [HttpGet("all")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicAPI.DTO.v1.UserProgram>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicAPI.DTO.v1.UserProgram>>> GetUserPrograms()
        {
            var response = await _bll.UserPrograms.GetAllAsync(User.GetUserId()!.Value);
            return response.Select(x => _mapper.Map<PublicAPI.DTO.v1.UserProgram>(x)!).ToList();
        }

        // POST: api/UserPrograms
        /// <summary>
        /// Add program to a user
        /// </summary>
        /// <param name="programId">Id of the program to add for the user.</param>
        /// <returns>Newly created UserProgram dto</returns>
        [HttpPost("{programId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.UserProgram), StatusCodes.Status201Created)]
        public async Task<ActionResult<PublicAPI.DTO.v1.UserProgram>> PostUserProgram(int programId)
        {
            var userId = User.GetUserId()!.Value;
            var userProgram = new UserProgram
            {
                AppUserId = userId,
                FullProgramId = programId
            };
            var bllEntity = _mapper.Map<BLL.App.DTO.UserProgram>(userProgram);
            _bll.UserPrograms.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity =
                _mapper.Map<PublicAPI.DTO.v1.UserProgram>(
                    _bll.UserPrograms.GetUpdatedEntityAfterSaveChanges(bllEntity));

            return CreatedAtAction("GetUserProgram", new {id = updatedEntity.Id}, updatedEntity);
        }

        // DELETE: api/UserPrograms/5
        /// <summary>
        /// Remove program from user
        /// </summary>
        /// <param name="programId">Id of the program to remove from the user</param>
        /// <returns>No content</returns>
        [HttpDelete("{programId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserProgram(int programId)
        {
            var userId = User.GetUserId()!.Value;
            var userProgram = await _bll.UserPrograms.FirstOrDefaultByUserIdAndProgramIdAsync(programId, userId);

            await _bll.UserPrograms.RemoveAsync(userProgram.Id, userId);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        
        // GET: api/UserPrograms
        /// <summary>
        /// Get all user full programs without splits.
        /// </summary>
        /// <returns>List of users programs with minimal information</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicAPI.DTO.v1.FullProgramSimple>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicAPI.DTO.v1.FullProgramSimple>>> GetUserFullPrograms()
        {
            var userId = User.GetUserId()!;
            return (await _bll.Programs.GetAllUserFullPrograms(userId.Value))
                .Select(x => _programMapper.MapSimple(x)!)
                .ToList();
        }
    }
}