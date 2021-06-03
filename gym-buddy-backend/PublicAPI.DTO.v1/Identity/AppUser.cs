using System;
using System.ComponentModel;
using Domain.Base;
using PublicAPI.DTO.v1.Enums;

#nullable enable

namespace PublicAPI.DTO.v1.Identity
{
    public class AppUser : DomainEntityId
    {
        public string Firstname { get; set; } = default!;

        public string Lastname { get; set; } = default!;

        public int HeightInCm { get; set; }
        public decimal WeightInKg { get; set; }

        public EGender Gender { get; set; }

        [DisplayName("User Since")] public DateTime UserSince { get; set; } = DateTime.Now;

        public int MentorId { get; set; }

        public string Email { get; set; } = default!;
    }

    public class AppUserInfo : DomainEntityId
    {
        public string Firstname { get; set; } = default!;

        public string Lastname { get; set; } = default!;

        public string Email { get; set; } = default!;
    }

    public class AppUserWithRole : DomainEntityId
    {
        public string Email { get; set; } = default!;

        public string? RoleName { get; set; }
    }

    public class AppUserLockDown : DomainEntityId
    {
        public DateTimeOffset LockDownEnd { get; set; }
    }

    public class AppUserInfoWithRoleAndLockDown : DomainEntityId
    {
        public string Firstname { get; set; } = default!;

        public string Lastname { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string? RoleName { get; set; }

        public virtual DateTime? LockoutEndDateTime { get; set; }

        public bool IsLockedOut { get; set; }
    }

    public class AppUserUpdate : DomainEntityId
    {
        public string Firstname { get; set; } = default!;

        public string Lastname { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string? NewPassword { get; set; }
        public string? OldPassword { get; set; }
    }
}