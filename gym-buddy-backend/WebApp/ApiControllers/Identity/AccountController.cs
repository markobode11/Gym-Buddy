using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PublicAPI.DTO.v1.Account;
using PublicAPI.DTO.v1.Identity;
using AppRole = Domain.App.Identity.AppRole;
using AppUser = Domain.App.Identity.AppUser;

namespace WebApp.ApiControllers.Identity
{
    /// <summary>
    /// API controller for registering and logging in.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IAppBLL _bll;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for controller.
        /// </summary>
        /// <param name="signInManager">Sign in manager</param>
        /// <param name="userManager">User manager</param>
        /// <param name="logger">Logger</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="bll">App BLL</param>
        /// <param name="mapper">Generic mapper</param>
        /// <param name="roleManager">Role manager</param>
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
            ILogger<AccountController> logger, IConfiguration configuration, IAppBLL bll, IMapper mapper,
            RoleManager<AppRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
            _bll = bll;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Try to log in user
        /// </summary>
        /// <param name="dto">Login dto</param>
        /// <returns>JWT response with token</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Account.JwtResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser == null)
            {
                _logger.LogWarning("WebApi login failed. User {User} not found", dto.Email);
                return NotFound(new Message("User/Password problem!"));
            }

            if (appUser.LockoutEnd != null &&
                appUser.LockoutEnd > DateTime.Now)
                return NotFound(
                    new Message($"You are locked out of you account until \n {appUser.LockoutEnd.Value.Date}"));

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, dto.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                var jwt = Extensions.Base.IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims,
                    _configuration["JWT:Key"],
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Issuer"],
                    DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                );
                _logger.LogInformation("WebApi login. User {User}", dto.Email);
                return Ok(new JwtResponse
                {
                    Token = jwt,
                    Firstname = appUser.Firstname,
                    Lastname = appUser.Lastname,
                });
            }

            _logger.LogWarning("WebApi login failed. User {User} - bad password", dto.Email);
            return NotFound(new Message("User/Password problem!"));
        }


        /// <summary>
        /// Try to register new user.
        /// </summary>
        /// <param name="dto">Register dto</param>
        /// <returns>JWT response with token for new user</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Account.JwtResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Register dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser != null)
            {
                _logger.LogWarning(" User {User} already registered", dto.Email);
                return BadRequest(new Message("User already registered"));
            }

            appUser = new Domain.App.Identity.AppUser()
            {
                Email = dto.Email,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                UserName = dto.Email
            };
            var result = await _userManager.CreateAsync(appUser, dto.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} created a new account with password", appUser.Email);

                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                    var jwt = Extensions.Base.IdentityExtensions.GenerateJwt(
                        claimsPrincipal.Claims,
                        _configuration["JWT:Key"],
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Issuer"],
                        DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                    );
                    _logger.LogInformation("WebApi login. User {User}", dto.Email);
                    return Ok(new JwtResponse()
                    {
                        Token = jwt,
                        Firstname = appUser.Firstname,
                        Lastname = appUser.Lastname,
                    });
                }

                _logger.LogInformation("User {Email} not found after creation", appUser.Email);
                return BadRequest(new Message("User not found after creation!"));
            }

            var errors = result.Errors.Select(error => error.Description).ToList();
            return BadRequest(new Message() {Messages = errors});
        }

        /// <summary>
        /// Create new Mentor and Account for the mentor.
        /// </summary>
        /// <param name="dto">Dto with information to create a Mentor and a User account for the mentor</param>
        /// <returns>Newly created mentor</returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.MentorSimple), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicAPI.DTO.v1.MentorSimple>> RegisterMentor(
            PublicAPI.DTO.v1.CreateMentorAndUser dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser != null)
            {
                _logger.LogWarning(" User {User} already registered", dto.Email);
                return BadRequest(new Message("User already registered"));
            }

            var registerEntity = new Register
            {
                Email = dto.Email,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Password = dto.Password
            };

            await Register(registerEntity);

            appUser = await _userManager.FindByEmailAsync(dto.Email);
            await _userManager.AddToRoleAsync(appUser, "Mentor");

            var mentorEntity = new PublicAPI.DTO.v1.MentorSimple
            {
                FullName = dto.FullName,
                Description = dto.Description,
                Since = dto.Since,
                Specialty = dto.Specialty,
                Email = dto.Email,
                AppUserId = appUser.Id
            };

            var bllEntity = _mapper.Map<BLL.App.DTO.Mentor>(mentorEntity)!;
            _bll.Mentors.Add(bllEntity);
            await _bll.SaveChangesAsync();

            var updatedEntity =
                _mapper.Map<PublicAPI.DTO.v1.MentorSimple>(_bll.Mentors.GetUpdatedEntityAfterSaveChanges(bllEntity));

            return Ok(updatedEntity);
        }

        /// <summary>
        /// Update user info
        /// </summary>
        /// <param name="dto">Dto with information to update the user</param>
        /// <returns>No content</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> ChangeUserInfo(AppUserUpdate dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

            user.Firstname = dto.Firstname;
            user.Lastname = dto.Lastname;
            user.Email = dto.Email;
            user.UserName = dto.Email;

            if (dto.NewPassword != null)
            {
                var passwordResult = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
                if (!passwordResult.Succeeded) return Problem("Error updating password!");
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest("Unexpected error when updating user info.");

            await _signInManager.RefreshSignInAsync(user);
            return Ok("Profile updated!");
        }

        /// <summary>
        /// Get user info.
        /// </summary>
        /// <returns>AppUser dto with minimal information about the user.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Identity.AppUserInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicAPI.DTO.v1.Identity.AppUserInfo>> GetUser()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            return _mapper.Map<PublicAPI.DTO.v1.Identity.AppUserInfo>(user);
        }

        /// <summary>
        /// Get all users with role and lockdown info. Currently logged in user will be excluded.
        /// </summary>
        /// <returns>List of app users</returns>
        [HttpGet]
        [ProducesResponseType(
            typeof(IEnumerable<PublicAPI.DTO.v1.Identity.AppUserInfoWithRoleAndLockDown>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<PublicAPI.DTO.v1.Identity.AppUserInfoWithRoleAndLockDown>>>
            GetAllUsers()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            
            var appUsers = _userManager.Users;
            var dtoList = new List<AppUserInfoWithRoleAndLockDown>();
            foreach (var appUser in appUsers)
            {
                if (appUser == currentUser) continue; // exclude current user from the list

                var dtoUser = _mapper.Map<AppUserInfoWithRoleAndLockDown>(appUser);
                dtoUser.RoleName = (await _userManager.GetRolesAsync(appUser)).FirstOrDefault();
                var lockoutEnd = await _userManager.GetLockoutEndDateAsync(appUser);
                dtoUser.LockoutEndDateTime = lockoutEnd?.UtcDateTime ?? null;
                dtoUser.IsLockedOut = await _userManager.IsLockedOutAsync(appUser);
                dtoList.Add(dtoUser);
            }

            return dtoList;
        }

        /// <summary>
        /// Get minimal user with role by user id.
        /// </summary>
        /// <param name="userId">Id of the requested user</param>
        /// <returns>AppUser dto with minimal information about the user including role name</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Identity.AppUserWithRole), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicAPI.DTO.v1.Identity.AppUserWithRole>> GetUserById(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) return NotFound("Bad user id!");

            var appUserWithRole = _mapper.Map<PublicAPI.DTO.v1.Identity.AppUserWithRole>(user);
            appUserWithRole.RoleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return appUserWithRole;
        }

        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <returns>List of app roles</returns>
        [HttpGet]
        [ProducesResponseType(
            typeof(IEnumerable<PublicAPI.DTO.v1.Identity.AppRole>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<PublicAPI.DTO.v1.Identity.AppRole>> GetRoles()
        {
            return _roleManager.Roles
                .Select(x => _mapper.Map<PublicAPI.DTO.v1.Identity.AppRole>(x)).ToList();
        }

        /// <summary>
        /// Remove role from user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>No content</returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> RemoveRole(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return NotFound("Bad user id!");

            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var result = await _userManager.RemoveFromRoleAsync(user, userRole);
            if (result.Succeeded) return NoContent();
            return BadRequest("Error removing role!");
        }

        /// <summary>
        /// Add role for user
        /// </summary>
        /// <param name="dto">Dto to specify role and user id</param>
        /// <returns>No content</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> AddRole(AppUserWithRole dto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (user == null) return NotFound("Bad user email!");

            var result = await _userManager.AddToRoleAsync(user, dto.RoleName);
            if (result.Succeeded) return NoContent();
            return BadRequest("Error adding role!");
        }

        /// <summary>
        /// Start lockdown for user
        /// </summary>
        /// <param name="dto">Dto to specify user and lockdown end</param>
        /// <returns>No content</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> StartLockdown(AppUserLockDown dto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (user == null) return NotFound("Bad user id!");

            var result = await _userManager.SetLockoutEndDateAsync(user, dto.LockDownEnd);
            if (result.Succeeded) return NoContent();
            return BadRequest("Error starting lockdown!");
        }

        /// <summary>
        /// Start lockdown for user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>No content</returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> EndLockdown(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return NotFound("Bad user id!");

            var result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now);
            if (result.Succeeded) return NoContent();
            return BadRequest("Error ending lockdown!");
        }
    }
}