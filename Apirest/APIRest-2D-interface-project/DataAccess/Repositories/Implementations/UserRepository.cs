﻿using APIRest_2D_interface_project.DataAccess.Context;
using APIRest_2D_interface_project.Domain.Entities;
using APIRest_2D_interface_project.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIRest_2D_interface_project.DataAccess.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly User user;
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext applicationDbContext, User user)
        {
            this.user = user;
            _context = applicationDbContext;
        }

        public async Task<User> RegisterUser(User user)
        {
            user.Id = Guid.NewGuid();
            user.IsVerified = true;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username) ?? throw new InvalidOperationException(username);
        }
    }
}
