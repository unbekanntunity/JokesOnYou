using JokesOnYou.Web.Api.DTOs;
using JokesOnYou.Web.Api.Models;
using JokesOnYou.Web.Api.Repositories.Interfaces;
using JokesOnYou.Web.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesOnYou.Web.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task DeleteUser(string id)
        {
            var user = await _userRepository.GetUserAsync(id) ?? null;

            if (user == null)
                throw new Exception();

            var userToDelete = new User { Id = user.Id };

            await _userRepository.DeleteUserAsync(userToDelete);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
           return await _userRepository.GetUsersAsync();
        }
       
        public async Task<User> GetUserById(string id)
        {
            return await _userRepository.GetUserAsync(id);
        }

        public async Task UpdateUser(UserUpdateDTO updateDTO)
        {
            // something something darkside
        }
    }
}
