using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller to get the list of muscles.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MusclesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly MuscleMapper _mapper;

        /// <summary>
        /// Constructor for the controller
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public MusclesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new MuscleMapper(mapper);
        }

        // GET: api/Muscles
        /// <summary>
        /// Get the list of muscles
        /// </summary>
        /// <returns>List of muscle DTOs</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicAPI.DTO.v1.Muscle>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicAPI.DTO.v1.Muscle>>> GetMuscles()
        {
            return (await _bll.Muscles.GetAllAsync()).Select(x => _mapper.Map(x)!).ToList();
        }
    }
}
