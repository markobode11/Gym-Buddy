using System;
using System.ComponentModel;
using BLL.App.DTO.Enums;
using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

#nullable enable

namespace BLL.App.DTO.Identity
{
    public class AppUser : IdentityUser<int>, IDomainEntityId
    {
        public string Firstname { get; set; } = default!;

        public string Lastname { get; set; } = default!;

        public int HeightInCm { get; set; }
        public decimal WeightInKg { get; set; }

        public EGender Gender { get; set; }

        public int MentorId { get; set; }

        [DisplayName("User Since")] public DateTime UserSince { get; set; } = DateTime.Now;
    }
}