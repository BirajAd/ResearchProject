using System.ComponentModel.DataAnnotations;

namespace RPHost.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(23, MinimumLength=7, ErrorMessage="Password must be between 7-23 characters long.")]
        public string Password { get; set; }
    }
}