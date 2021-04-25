using JokesOnYou.Web.Api.Data;
using JokesOnYou.Web.Api.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JokesOnYou.Web.Api.DTOs;
using JokesOnYou.Web.Api.Exceptions;
using JokesOnYou.Web.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;

namespace JokesOnYou.Web.Api.Repositories
{
    public class JokeRepository
    {
        readonly DataContext _dataContext;
        public JokeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> CreateJoke(Joke joke)
        {
            await _dataContext.Jokes.AddAsync(joke);
            return (await _dataContext.Jokes.FindAsync(joke)).Id;
        }

        public async Task<Joke> GetJokeByIdAsync(int id)
        {
            return await _dataContext.Jokes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteJoke(Joke joke)
        {
            _dataContext.Jokes.Remove(joke);
        }

        public async Task<Joke> GetJokeByPartialPremise(string partialPremise)
        {
            return await _dataContext.Jokes.FirstOrDefaultAsync(x => x.Premise.ToLower().Contains(partialPremise.ToLower()));
        }

        public async Task<List<Joke>> GetJokesFilteredByPremise(string filter)
        {
            List<Joke> foundJokes = new List<Joke>();

            foreach (var item in _dataContext.Jokes)
                if (item.Premise.ToLower().Contains(filter.ToLower()))
                    foundJokes.Add(item);

            return foundJokes;
        }

        public async Task<List<Joke>> GetJokesFilteredByAuthor(string authorName)
        {
            List<Joke> foundJokes = new List<Joke>();

            foreach (var item in _dataContext.Jokes)
                if (item.Author.Name.ToLower().Contains(authorName.ToLower()))
                    foundJokes.Add(item);

            return foundJokes;
        }

    }
}
