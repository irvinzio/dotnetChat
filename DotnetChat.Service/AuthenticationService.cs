using DotnetChat.Core.Interfaces;
using DotnetChat.Core.Models;
using System.Threading.Tasks;
using DotnetChat.Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotnetChat.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace DotnetChat.Service
{
    public class AuthenticationService : IAutheticationService
    {
        private readonly IAutheticationIdentity _authIdentity;
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AuthenticationService(IAutheticationIdentity authIdentity,IRepository<User> repository, IMapper mapper, ILogger<AuthenticationService> logger)
        {
            _userRepo = repository;
            _mapper = mapper;
            _logger = logger;
            _authIdentity = authIdentity;
        }
        public async Task<LoginResponse> LogIn(LoginRequest model)
        {
            _logger.LogInformation($"user {model.Email} login");
            return await _authIdentity.LogIn(model);
        }

        public async Task<RegisterResponse> Register(RegisterRequest model)
        {
            var registerResponse = await _authIdentity.Register(model);
            if (!registerResponse.Success) return registerResponse;

            var userEntity = _mapper.Map<User>(model);
            _logger.LogInformation($"user {model.Email} register into user table", userEntity);
            await _userRepo.Add(userEntity);

            return registerResponse;
        }
    }
}
