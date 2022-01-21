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
        Task<User> CreateUser(LoginInfoRequest createUserRequest);
        Task AddUserRole(AddRoleRequest roleRequest);
        Task DeleteUser(int id);
        // Update user password
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IRoleService _roleService;
        private readonly IOptions<AuthSettings> _options;

        public UserService(ApplicationDbContext dbContext,IRoleService roleService, IOptions<AuthSettings> authSettings)
        {
            _dbContext = dbContext;
            _roleService = roleService;
            _options = authSettings;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null || !await VerifyPasswordHash(user, password)) 
            {
                return null;
            }

            return user;
        }

        public async Task<User> CreateUser(LoginInfoRequest userRequest)
        {
            var user = await GetUserByUsername(userRequest.UserName);
            if (user != null)
            {
                throw new Exception("User already exists");
            }

            CreatePasswordHash(userRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);
            
            var newUser = new User()
            {
                UserName = userRequest.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            if (_dbContext.ProductPayments.Any())
            {
                var newId = await _dbContext.Users.MaxAsync(p => p.Id) + 1;
                newUser.Id = newId;
            }

            var created = await _dbContext.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
            return created.Entity;
        }

        public async Task AddUserRole(AddRoleRequest roleRequest)
        {
            var user = await this.GetUserById(roleRequest.UserId);
            var role = await _roleService.GetRoleBydId(roleRequest.RoleId);
            if (user == null || role == null)
            {
                throw new ArgumentException("User or role doesn't exist");
            }

            var userRole = new UserRole()
            {
                RoleId = roleRequest.RoleId,
                UserId = roleRequest.UserId
            };
            await _dbContext.UserRoles.AddAsync(userRole);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await this.GetUserById(id);
            if (user == null)
            {
                return;
            }

            user.Enabled = false;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _dbContext.Users.Where(x => x.Enabled).ToListAsync();
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