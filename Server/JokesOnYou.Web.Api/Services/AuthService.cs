﻿using JokesOnYou.Web.Api.DTOs;
using JokesOnYou.Web.Api.Exceptions;
using JokesOnYou.Web.Api.Models;
using JokesOnYou.Web.Api.Repositories.Interfaces;
using JokesOnYou.Web.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesOnYou.Web.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;

        public AuthService(SignInManager<User> signInManager, IUserRepository userRepo, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userRepo = userRepo;
            _tokenService = tokenService;
        }
        
        public async Task<UserReplyDTO> LoginAsync(UserLoginDTO userLogin)
        {
            var user = await _userRepo.GetUserByEmailAsync(userLogin.LoginName);
            if (user != null)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

                if (!signInResult.Succeeded)
                {
                    throw new AppException("Sign in failed");
                }
                else
                {
                    var userReplyDTO = new UserReplyDTO()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        UserName = user.Email,
                        Token = _tokenService.GetToken(user)
                    };
                    return userReplyDTO;
                }
            }
            else
            {
                throw new AppException("User not found");
            }
        }

        public async Task RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            var user = await _userRepo.CreateUserAsync(userRegisterDTO);

            if (user == null)
            {
                throw new AppException("Failed to find / create user in the Database.");
            }
        }
    }
}
