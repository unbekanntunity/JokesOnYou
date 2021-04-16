﻿using AutoMapper;
using JokesOnYou.Web.Api.DTOs;
using JokesOnYou.Web.Api.Exceptions;
using JokesOnYou.Web.Api.Models;
using JokesOnYou.Web.Api.Repositories.Interfaces;
using JokesOnYou.Web.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesOnYou.Web.Api.Services
{
    public class JokesService : IJokesService
    {
        private readonly IJokesRepository _jokesRepo;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public JokesService(IJokesRepository jokesRepo, IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _jokesRepo = jokesRepo;
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<JokeReplyDto> CreateJokeAsync(JokeCreateDto jokeCreateDto, string userId)
        {

            var user = await _userRepository.GetUserAsync(userId);
            var joke = _mapper.Map<Joke>(jokeCreateDto);
            joke.UploadDate = DateTime.Now;
            joke.Author = user;

            await _jokesRepo.CreateJokeAsync(joke);
            var saved = await _unitOfWork.SaveAsync();
            if (!saved)
            {
                throw new AppException("Error with saving the Joke.");
            }

            var jokeReplyDto = _mapper.Map<JokeReplyDto>(joke);

            return jokeReplyDto;
        }

        public async Task<IEnumerable<JokeReplyDto>> GetAllJokeDtosAsync()
        {
            var jokeDtos = await _jokesRepo.GetAllJokeDtosAsync();

            return jokeDtos;
        }
    }
}
