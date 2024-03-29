using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace RPHost.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Bio { get; set; }
        public string Institute { get; set; }
        public string FieldOfInterests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }

        public ICollection<Follow> FollowByUsers { get; set; }
        public ICollection<Follow> FollowedUsers { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        public static implicit operator User(ClaimsPrincipal v)
        {
            throw new NotImplementedException();
        }
    }
}