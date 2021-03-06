﻿using DotnetChat.Core.Interfaces;
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

            var localUser = await GetUserResponse(model.Email);

            if (localUser == null) return null;

            var loginResponse =  await _authIdentity.LogIn(model);

            if (loginResponse == null) await RegisterTestUser(localUser);

            loginResponse = await _authIdentity.LogIn(model);

            loginResponse.user = localUser;
            return loginResponse;
        } 
        private async Task RegisterTestUser(UserResponse localUser)
        {
            var indentityRegister = new RegisterRequest()
            {
                Username = localUser.UserName,
                Email = localUser.Email,
                Password = "TestTest123!"
            };
            var registerResponse = await _authIdentity.Register(indentityRegister);
            if (!registerResponse.Success) throw new Exception("Error registering test user");
        }
        private async Task<UserResponse> GetUserResponse(string email)
        {
            var userEntity = await _userRepo.FirstOrDefault(s => s.Email == email);
            return _mapper.Map<UserResponse>(userEntity);
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
