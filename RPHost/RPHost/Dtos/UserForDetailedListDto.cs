using System;
using System.Collections.Generic;
using RPHost.Models;

namespace RPHost.Dtos
{
    public class UserForDetailedListDto
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        //Extra info from here
        public string Bio { get; set; }
        public string FieldOfInterests { get; set; }
        public string City { get; set; }
        //till here
        public string Country { get; set; }
        public string PhotoPath { get; set; }
        public ICollection<PhotosForDetailedDto> Photos { get; set; }
    }
}