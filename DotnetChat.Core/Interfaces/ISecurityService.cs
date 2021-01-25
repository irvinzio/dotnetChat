using DotnetChat.Core.Models;
using System.Threading.Tasks;

namespace DotnetChat.Core.Interfaces
{
    public interface ISecurityService
    {
        Task<UserResponse> LogIn(LogInRequest loginRequest);
        Task<bool> LogOut();
    }
}
