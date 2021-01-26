using DotnetChat.Core.Interfaces;
using DotnetChat.Core.Models;
using DotnetChat.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DotnetChat.Infrasctructure.Security
{
    public class AuthenticactionIdentity: IAutheticationIdentity
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public AuthenticactionIdentity(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ILogger<AuthenticactionIdentity> logger)
        {
            this.userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<LoginResponse> LogIn(LoginRequest model)
        {
            _logger.LogInformation($"user {model.Email} login with identity");
            var user = await userManager.FindByEmailAsync(model.Email);
            var isCorrectPassword = await userManager.CheckPasswordAsync(user, model.Password);
            if (user == null || !isCorrectPassword)
            {
                _logger.LogError($"user {model.Email} identity wrong credetnials");
                return null;
            }
            var authClaims = await BuildUserClaims(user);
   
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = BuildJwtSecurityToken(authClaims, authSigningKey);

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }
        private async Task<List<Claim>> BuildUserClaims(ApplicationUser user)
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            _logger.LogInformation($"setting claims for user {user.Email}", authClaims);
            return authClaims;
        }
        private JwtSecurityToken BuildJwtSecurityToken(List<Claim> authClaims, SymmetricSecurityKey authSigningKey)
        {
            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
        }

        public async Task<RegisterResponse> Register(RegisterRequest model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return new RegisterResponse { Success = false, Message = "User already exists!" };

            ApplicationUser user = BuildApplicationUser(model);


            var result = await userManager.CreateAsync(user, model.Password);

            if(!result.Succeeded)
            {
                var errorMessage = GetIdentityErrorString(result);
                _logger.LogError(errorMessage);
                return new RegisterResponse { Success = false, Message = errorMessage };
            }
            

            return new RegisterResponse { Success = true, Message = "User created successfully!" };
        }
        private string GetIdentityErrorString(IdentityResult result)
        {
            var errorMessage = $"User creation failed! Please check user details and try again";
            foreach (var error in result.Errors)
            {
                errorMessage += error.Description + "/n/r";
            }
            return errorMessage;
        }
        private ApplicationUser BuildApplicationUser(RegisterRequest model)
        {
            return new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
        }
    }
}
