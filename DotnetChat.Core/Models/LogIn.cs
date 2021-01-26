using System;
using System.ComponentModel.DataAnnotations;

namespace DotnetChat.Core.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
