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
    /// API controller to deal with full programs.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FullProgramsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly FullProgramMapper _mapper;

        /// <summary>
        /// Constructor for controller.
        /// </summary>
        /// <param name="bll">App BLL</param>
        /// <param name="mapper">Mapper between API dto and BLL dto</param>
        public FullProgramsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new FullProgramMapper(mapper);
        }

        // GET: api/FullPrograms
        /// <summary>
        /// Get all full programs without splits.
        /// </summary>
        /// <returns>List of programs with minimal information</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicAPI.DTO.v1.FullProgramSimple>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicAPI.DTO.v1.FullProgramSimple>>> GetFullPrograms()
        {
            return (await _bll.Programs.GetAllAsync())
                .Select(x => _mapper.MapSimple(x)!)
                .ToList();
        }

        // GET: api/FullPrograms/5
        /// <summary>
        /// Get full program by id. Program has included splits.
        /// </summary>
        /// <param name="id">Id of the full program to get</param>
        /// <returns>Full program with splits</returns>
        /// [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.FullProgram), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PublicAPI.DTO.v1.FullProgram>> GetFullProgram(int id)
        {
            var fullProgram = _mapper.Map(await _bll.Programs.FirstOrDefaultWithSplitsAsync(id));
            return fullProgram == null ? NotFound() : fullProgram;
        }

        // PUT: api/FullPrograms/5
        /// <summary>
        /// Update program
        /// </summary>
        /// <param name="id">Id of the program to update</param>
        /// <param name="fullProgram">FullProgram dto with new info</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutFullProgram(int id, PublicAPI.DTO.v1.FullProgramSimple fullProgram)
        {
            if (id != fullProgram.Id) return BadRequest();

            _bll.Programs.Update(_mapper.MapSimple(fullProgram)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/FullPrograms
        /// <summary>
        /// Create new full program.
        /// </summary>
        /// <param name="fullProgram">DTO of the program to create.</param>
        /// <returns>Newly created program</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.FullProgramSimple), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicAPI.DTO.v1.FullProgramSimple>> PostFullProgram(
            PublicAPI.DTO.v1.FullProgramSimple fullProgram)
        {
            var bllEntity = _mapper.MapSimple(fullProgram)!;
            _bll.Programs.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _mapper.Map(_bll.Programs.GetUpdatedEntityAfterSaveChanges(bllEntity));

            return CreatedAtAction("GetFullProgram", new {id = updatedEntity!.Id}, updatedEntity);
        }

        // DELETE: api/FullPrograms/5
        /// <summary>
        /// Delete program by id.
        /// </summary>
        /// <param name="id">Id of the program to be deleted</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteFullProgram(int id)
        {
            var fullProgram = await _bll.Programs.FirstOrDefaultAsync(id);
            if (fullProgram == null) return NotFound();

            await _bll.Programs.RemoveAsync(fullProgram.Id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}