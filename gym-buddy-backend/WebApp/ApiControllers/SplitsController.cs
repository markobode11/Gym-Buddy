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
    /// Api controller to deal with Splits
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SplitsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly SplitMapper _mapper;

        /// <summary>
        /// Constructor for the controller
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public SplitsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new SplitMapper(mapper);
        }

        // GET: api/Splits
        /// <summary>
        /// Get the list of splits without workouts in them
        /// </summary>
        /// <returns>List of splits</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicAPI.DTO.v1.SplitSimple>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicAPI.DTO.v1.SplitSimple>>> GetSplits()
        {
            return (await _bll.Splits.GetAllAsync())
                .Select(x => _mapper.MapSimple(x)!)
                .ToList();
        }

        // GET: api/Splits/5
        /// <summary>
        /// Get a split with workouts that are in that split.
        /// </summary>
        /// <param name="id">Id of the split to retrieve</param>
        /// <returns>Split with workouts</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Split), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicAPI.DTO.v1.Split>> GetSplit(int id)
        {
            var split = _mapper.Map(await _bll.Splits.FirstOrDefaultWithWorkoutsAsync(id));
            return split == null ? NotFound() : split;
        }

        // PUT: api/Splits/5
        /// <summary>
        /// Update split
        /// </summary>
        /// <param name="id">Id of  the split to be updated</param>
        /// <param name="split">Split dto with updated information</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutSplit(int id, PublicAPI.DTO.v1.SplitSimple split)
        {
            if (id != split.Id) return BadRequest();

            _bll.Splits.Update(_mapper.MapSimple(split)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Splits
        /// <summary>
        /// Create new split
        /// </summary>
        /// <param name="split">Split dto of the new split</param>
        /// <returns>Newly created split</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.SplitSimple), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicAPI.DTO.v1.SplitSimple>> PostSplit(PublicAPI.DTO.v1.SplitSimple split)
        {
            var bllEntity = _mapper.MapSimple(split)!;
            _bll.Splits.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity = _mapper.Map(_bll.Splits.GetUpdatedEntityAfterSaveChanges(bllEntity));

            return CreatedAtAction("GetSplit", new {id = updatedEntity!.Id}, updatedEntity);
        }

        // DELETE: api/Splits/5
        /// <summary>
        /// Delete split by id
        /// </summary>
        /// <param name="id">Id of the split to be deleted</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteSplit(int id)
        {
            var split = await _bll.Splits.FirstOrDefaultAsync(id);
            if (split == null) return NotFound();

            await _bll.Splits.RemoveAsync(split.Id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}