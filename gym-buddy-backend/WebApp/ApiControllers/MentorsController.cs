using System.Collections.Generic;
using System.Linq;
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
    /// API controller to deal with Mentors
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MentorsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor of the controller
        /// </summary>
        /// <param name="bll">App BLL</param>
        /// <param name="mapper">Mapper between API dto and BLL dto</param>
        public MentorsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/Mentors
        /// <summary>
        /// Get all mentors with basic information about them
        /// </summary>
        /// <returns>List of mentors</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicAPI.DTO.v1.MentorSimple>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicAPI.DTO.v1.MentorSimple>>> GetMentors()
        {
            return (await _bll.Mentors.GetAllAsync())
                .Select(x => _mapper.Map<PublicAPI.DTO.v1.MentorSimple>(x))
                .ToList();
        }

        // GET: api/Mentors/5
        /// <summary>
        /// Get mentor by Id
        /// </summary>
        /// <param name="id">Id of the mentor to retrieve</param>
        /// <returns>Requested mentor</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Mentor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicAPI.DTO.v1.Mentor>> GetMentor(int id)
        {
            var mentor = _mapper.Map<PublicAPI.DTO.v1.Mentor>(await _bll.Mentors.FirstOrDefaultAsync(id));

            return mentor == null ? NotFound() : mentor;
        }

        // PUT: api/Mentors/5
        /// <summary>
        /// Update mentor.
        /// </summary>
        /// <param name="id">Id of the mentor to be updated</param>
        /// <param name="mentor">Mentor dto with new information</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutMentor(int id, PublicAPI.DTO.v1.MentorSimple mentor)
        {
            if (id != mentor.Id) return BadRequest();

            _bll.Mentors.Update(_mapper.Map<BLL.App.DTO.Mentor>(mentor));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Mentors/5
        /// <summary>
        /// Delete mentor by Id.
        /// </summary>
        /// <param name="id">Id of the mentor to be deleted</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteMentor(int id)
        {
            var mentor = await _bll.Mentors.FirstOrDefaultAsync(id);
            if (mentor == null) return NotFound();

            await _bll.Mentors.RemoveAsync(mentor.Id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}