using System;
using System.ComponentModel;
using Contracts.Domain.Base;
using DAL.App.DTO.Enums;
using Microsoft.AspNetCore.Identity;

#nullable enable

namespace DAL.App.DTO.Identity
{
    public class AppUser : IdentityUser<int>, IDomainEntityId
    {
        public string Firstname { get; set; } = default!;

        public string Lastname { get; set; } = default!;

        public int HeightInCm { get; set; }
        public decimal WeightInKg { get; set; }

        public EGender Gender { get; set; }

        [DisplayName("User Since")] public DateTime UserSince { get; set; } = DateTime.Now;

        public int MentorId { get; set; }
    }
}