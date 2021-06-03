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
    /// API controller to add or remove splits to programs
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SplitInProgramController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the controller
        /// </summary>
        /// <param name="bll">App BLL</param>
        /// <param name="mapper">Mapper between API dto and BLL dto</param>
        public SplitInProgramController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/SplitInProgram/5
        /// <summary>
        /// Method to return SplitInProgram dto that was created in the POST method
        /// </summary>
        /// <param name="id">Id of the SplitInProgram dto</param>
        /// <returns>SplitInProgram dto</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.SplitInProgram), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicAPI.DTO.v1.SplitInProgram>> GetSplitInProgram(int id)
        {
            var res = _mapper.Map<PublicAPI.DTO.v1.SplitInProgram>(await _bll.SplitInPrograms.FirstOrDefaultAsync(id));
            return res == null ? NotFound() : res;
        }

        // POST: api/SplitInProgram
        /// <summary>
        /// Add a split to a program
        /// </summary>
        /// <param name="splitInProgram">Dto to clarify the split and the program</param>
        /// <returns>Newly created SplitInProgram dto</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.SplitInProgram), StatusCodes.Status201Created)]
        public async Task<ActionResult<PublicAPI.DTO.v1.SplitInProgram>> PostSplitInProgram(
            PublicAPI.DTO.v1.SplitInProgram splitInProgram)
        {
            var bllEntity = _mapper.Map<BLL.App.DTO.SplitInProgram>(splitInProgram);
            _bll.SplitInPrograms.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity =
                _mapper.Map<PublicAPI.DTO.v1.SplitInProgram>(
                    _bll.SplitInPrograms.GetUpdatedEntityAfterSaveChanges(bllEntity));

            return CreatedAtAction("GetSplitInProgram", new {id = updatedEntity.Id}, updatedEntity);
        }

        // DELETE: api/SplitInProgram/5
        /// <summary>
        /// Remove a split from a program
        /// </summary>
        /// <param name="programId">Id of the program</param>
        /// <param name="splitId">Id of the split</param>
        /// <returns>No content</returns>
        [HttpDelete("{programId}/{splitId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSplitInProgram(int programId, int splitId)
        {
            var splitInProgram = await _bll.SplitInPrograms.FirstOrDefaultByProgramIdAndSplitId(programId, splitId);
            if (splitInProgram == null) return NotFound();

            await _bll.SplitInPrograms.RemoveAsync(splitInProgram.Id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}