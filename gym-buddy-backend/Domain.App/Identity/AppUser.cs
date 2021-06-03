﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Contracts.Domain.Base;
using Domain.App.Enums;
using Microsoft.AspNetCore.Identity;

#nullable enable

namespace Domain.App.Identity
{
    public class AppUser : IdentityUser<int>, IDomainEntityId
    {
        public string Firstname { get; set; } = default!;

        public string Lastname { get; set; } = default!;

        public int HeightInCm { get; set; }
        public decimal WeightInKg { get; set; }

        public EGender Gender { get; set; }

        [DisplayName("User Since")] public DateTime UserSince { get; set; } = DateTime.Now;

        public Mentor? ConnectedMentor { get; set; }

        public ICollection<UserProgram>? UserPrograms { get; set; }

        public ICollection<Macros>? Macros { get; set; }

        public ICollection<UserMentor>? UserMentors { get; set; }
    }
}