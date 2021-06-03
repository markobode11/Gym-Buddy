using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace PublicAPI.DTO.v1.Identity
{
    public class AppRole : IdentityRole<int>, IDomainEntityId
    {
    }
}