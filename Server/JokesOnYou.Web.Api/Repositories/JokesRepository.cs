﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using JokesOnYou.Web.Api.Data;
using JokesOnYou.Web.Api.DTOs;
using JokesOnYou.Web.Api.Models;
using JokesOnYou.Web.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JokesOnYou.Web.Api.Repositories
{
    public class JokesRepository : IJokesRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public JokesRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<bool> DoesJokeExist(string normalizedPremise, string normalizedPunchline) =>
            _context.Jokes.AnyAsync(joke => joke.NormalizedPremise == normalizedPremise &&
                                            joke.NormalizedPunchLine == normalizedPunchline);

        public async Task CreateJokeAsync(Joke joke) => await _context.Jokes.AddAsync(joke).AsTask();
        public async Task<IEnumerable<Joke>> GetAllJokesAsync() => await _context.Jokes.ToListAsync();
        public async Task<IEnumerable<JokeReplyDto>> GetAllJokeDtosAsync() => await _context.Jokes.ProjectTo<JokeReplyDto>(_mapper.ConfigurationProvider).ToListAsync();

        public Task<JokeReplyDto> GetJokeDtoAsync(int id)
        {
            return _context.Jokes.ProjectTo<JokeReplyDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(j => j.Id == id);
        }
    }
}