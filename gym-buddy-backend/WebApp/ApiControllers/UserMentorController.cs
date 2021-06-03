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

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller to add and remove Users from Mentor
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserMentorController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;


        /// <summary>
        /// Constructor for the controller
        /// </summary>
        /// <param name="bll">App BLL</param>
        /// <param name="mapper">Mapper between API dto and BLL dto</param>
        public UserMentorController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/UserMentor
        /// <summary>
        /// Method to return UserMentor dto that was created in the POST method
        /// </summary>
        /// <returns>UserMentor dto</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.UserMentor), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicAPI.DTO.v1.UserMentor>> GetUserMentor()
        {
            var response = await _bll.UserMentors.FirstOrDefaultByUserIdAsync(User.GetUserId()!.Value);
            return _mapper.Map<PublicAPI.DTO.v1.UserMentor>(response);
        }

        // GET: api/UserMentor/mentor
        /// <summary>
        /// Method to return the Mentor dto for the User
        /// </summary>
        /// <returns>UserMentor dto</returns>
        [HttpGet("mentor")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.MentorSimple), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicAPI.DTO.v1.MentorSimple>> GetUsersMentor()
        {
            var response = await _bll.UserMentors.FirstOrDefaultByUserIdAsync(User.GetUserId()!.Value);
            if (response == null) return NotFound("No mentor has been added!");
            var mentor = await _bll.Mentors.FirstOrDefaultAsync(response.MentorId);
            return _mapper.Map<PublicAPI.DTO.v1.MentorSimple>(mentor);
        }

        // POST: api/UserMentor
        /// <summary>
        /// Add user to a mentor
        /// </summary>
        /// <param name="mentorId">Id of the mentor to add to the user</param>
        /// <returns>Newly created UserMentor dto</returns>
        [HttpPost("{mentorId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.UserMentor), StatusCodes.Status201Created)]
        public async Task<ActionResult<PublicAPI.DTO.v1.UserMentor>> PostUserMentor(int mentorId)
        {
            var userId = User.GetUserId()!.Value;
            var userMentor = new PublicAPI.DTO.v1.UserMentor
            {
                AppUserId = userId,
                MentorId = mentorId
            };
            var bllEntity = _mapper.Map<BLL.App.DTO.UserMentor>(userMentor);

            _bll.UserMentors.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity =
                _mapper.Map<PublicAPI.DTO.v1.UserMentor>(
                    _bll.UserMentors.GetUpdatedEntityAfterSaveChanges(bllEntity));

            return CreatedAtAction("GetUserMentor", new {id = updatedEntity.Id}, updatedEntity);
        }

        // DELETE: api/UserMentor/5
        /// <summary>
        /// Remove user from a mentor
        /// </summary>
        /// <returns>No content</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserMentor()
        {
            var userMentor = await _bll.UserMentors.FirstOrDefaultByUserIdAsync(User.GetUserId()!.Value);

            await _bll.UserMentors.RemoveAsync(userMentor!.Id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/UserMentor/trainees
        /// <summary>
        /// Method to return the Mentor dto for the User
        /// </summary>
        /// <returns>UserMentor dto</returns>
        [HttpGet("trainees")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Identity.AppUser), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PublicAPI.DTO.v1.Identity.AppUser>>> GetMentorsTrainees()
        {
            var response = await _bll.Users.GetAllMentorsTrainees(User.GetUserId()!.Value);

            return response.Select(x => _mapper.Map<PublicAPI.DTO.v1.Identity.AppUser>(x)).ToList();
        }
    }
}