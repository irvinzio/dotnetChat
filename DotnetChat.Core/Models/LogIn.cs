using System.ComponentModel.DataAnnotations;

namespace DotnetChat.Core.Models
{
    public class LogInRequest
    {
       [Required]
       public string  email { get; set; }
       public string password { get; set; }
    }
}
