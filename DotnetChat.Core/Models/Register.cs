using System.ComponentModel.DataAnnotations;

namespace DotnetChat.Core.Models
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
