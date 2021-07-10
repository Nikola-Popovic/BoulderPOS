using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BoulderPOS.API.Configuration;
using BoulderPOS.API.Models;
using BoulderPOS.API.Models.DTO;
using BoulderPOS.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BoulderPOS.API.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> CreateUser(CreateUserDto createUserDto);
        Task DeleteUser(int id);
        // Update user password
        // Update user roles
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IOptions<AuthSettings> _options;

        public UserService(ApplicationDbContext context, IOptions<AuthSettings> authSettings)
        {
            _context = context;
            _options = authSettings;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null || !await VerifyPasswordHash(user, password)) 
            {
                return null;
            }

            return user;
        }

        public async Task<User> CreateUser(CreateUserDto userDto)
        {
            var user = await GetUserByUsername(userDto.UserName);
            if (user != null)
            {
                throw new Exception("User already exists");
            }

            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);



            var newUser = new User()
            {
                UserName = userDto.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            if (_context.ProductPayments.Any())
            {
                var newId = await _context.Users.MaxAsync(p => p.Id) + 1;
                newUser.Id = newId;
            }

            var created = await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return created.Entity;
        }

        public async Task DeleteUser(int id)
        {
            var user = await this.GetUserById(id);
            if (user == null)
            {
                return;
            }

            user.Enabled = false;
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.Where(x => x.Enabled).ToListAsync();
        }


        private static async Task<bool> VerifyPasswordHash(User user, string password)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hash = hmac.ComputeHash(passwordBytes);
            return hash.SequenceEqual(user.PasswordHash);
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be empty or null");
            }

            using var hmac = new System.Security.Cryptography.HMACSHA512();

            var keyBytes = Encoding.UTF8.GetBytes(password);
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(keyBytes);
        }
    }
}