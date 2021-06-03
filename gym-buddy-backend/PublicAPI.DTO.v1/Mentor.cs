using System;
using System.Collections.Generic;
using System.ComponentModel;
using Domain.Base;
using PublicAPI.DTO.v1.Identity;

#nullable enable
namespace PublicAPI.DTO.v1
{
    public class Mentor : DomainEntityId
    {
        [DisplayName("Full name")] public string FullName { get; set; } = default!;
        public string Specialty { get; set; } = null!;

        public DateTime Since { get; set; } = DateTime.Now;

        public string Description { get; set; } = null!;

        public int AppUserId { get; set; }

        public string Email { get; set; } = default!;

        public ICollection<AppUser>? MentorUsers { get; set; }
    }

    public class MentorSimple : DomainEntityId
    {
        [DisplayName("Full name")] public string FullName { get; set; } = default!;
        public string Specialty { get; set; } = null!;

        public DateTime Since { get; set; } = DateTime.Now;

        public string Email { get; set; } = default!;

        public string Description { get; set; } = null!;

        public int AppUserId { get; set; }
    }

    public class CreateMentorAndUser : DomainEntityId
    {
        // for Mentor
        [DisplayName("Full name")] public string FullName { get; set; } = default!;
        public string Specialty { get; set; } = null!;

        public DateTime Since { get; set; } = DateTime.Now;

        public string Description { get; set; } = null!;

        // for AppUser
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;

        public string Firstname { get; set; } = default!;

        public string Lastname { get; set; } = default!;
    }
}