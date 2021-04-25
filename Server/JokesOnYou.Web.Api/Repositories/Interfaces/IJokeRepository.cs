using JokesOnYou.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesOnYou.Web.Api.Repositories.Interfaces
{
    public interface IJokeRepository
    {
        Task<int> CreateJoke(Joke joke);
        Task<Joke> GetJokeByIdAsync(int id);
        Task DeleteJoke(Joke joke);
        Task<Joke> GetJokeByPartialPremise(string partialPremise);
    }
}
