using System.ComponentModel.DataAnnotations;

namespace CmentarzKomunalny.Web.DTOs
{
    public class UserAuthenticateDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
