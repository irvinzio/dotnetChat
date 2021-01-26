using System.ComponentModel.DataAnnotations;

namespace DotnetChat.Core.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
    public class LoginResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
