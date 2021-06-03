using Contracts.BLL.App.Services;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;

namespace BLL.App.Services
{
    public class AccountService : IAccountService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
    }
}