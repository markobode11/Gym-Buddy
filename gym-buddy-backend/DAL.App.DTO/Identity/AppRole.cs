using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.DTO.Identity
{
    public class AppRole : IdentityRole<int>, IDomainEntityId
    {
    }
}