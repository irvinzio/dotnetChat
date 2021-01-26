using DotnetChat.Core.Models;
using System.Threading.Tasks;

namespace DotnetChat.Core.Interfaces
{
    public interface IAutheticationIdentity
    {
        Task<LoginResponse> LogIn(LoginRequest loginRequest);
        Task<RegisterResponse> Register(RegisterRequest loginRequest);
    }
}
