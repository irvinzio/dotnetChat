using DotnetChat.Core.Interfaces;
using DotnetChat.Core.Models;
using System.Threading.Tasks;

namespace DotnetChat.Service
{
    public class SecurityService : ISecurityService
    {
        public Task<UserResponse> LogIn(LogInRequest loginRequest)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> LogOut()
        {
            throw new System.NotImplementedException();
        }
    }
}
