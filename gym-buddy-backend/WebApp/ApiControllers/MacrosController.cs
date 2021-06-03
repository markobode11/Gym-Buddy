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
    /// API controller to deal with user macros.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MacrosController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;


        /// <summary>
        /// Constructor for the controller
        /// </summary>
        /// <param name="bll">App bll</param>
        /// <param name="mapper">Mapper between API dto and BLL dto</param>
        public MacrosController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        // GET: api/Macros/5
        /// <summary>
        /// Get user macros.
        /// </summary>
        /// <returns>Macros for the user</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Macros), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicAPI.DTO.v1.Macros>> GetMacros()
        {
            var macros =
                _mapper.Map<PublicAPI.DTO.v1.Macros>(
                    await _bll.Macros.FirstOrDefaultByUserIdAsync(User.GetUserId()!.Value));
            return macros == null ? NotFound() : macros;
        }

        // POST: api/Macros
        /// <summary>
        /// Create macros for user.
        /// </summary>
        /// <param name="macros">Macros dto of the new macros</param>
        /// <returns>Newly created macros</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Macros), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicAPI.DTO.v1.Macros>> PostMacros(PublicAPI.DTO.v1.MacrosCalculation macros)
        {
            var userId = User.GetUserId()!.Value;
            if (await _bll.Macros.ExistsAsync(userId))
            {
                await _bll.Macros.RemoveAsync((await _bll.Macros.FirstOrDefaultByUserIdAsync(userId))!.Id);
            }
            var bllEntity = _mapper.Map<BLL.App.DTO.MacrosCalculation>(macros);
            var userMacros = _bll.Macros.CalculateMacros(bllEntity);
            userMacros.AppUserId = userId;
            _bll.Macros.Add(userMacros);
            await _bll.SaveChangesAsync();

            var updatedEntity =
                _mapper.Map<PublicAPI.DTO.v1.Macros>(_bll.Macros.GetUpdatedEntityAfterSaveChanges(userMacros));

            return CreatedAtAction("GetMacros", new {id = updatedEntity.Id}, updatedEntity);
        }
    }
}