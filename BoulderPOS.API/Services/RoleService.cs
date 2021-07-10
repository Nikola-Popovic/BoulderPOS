using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoulderPOS.API.Models;
using BoulderPOS.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role> GetRoleBydId(int id);
        Task DeleteRole(int id);
        // Create roles
    }

    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleService(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _dbContext.Roles.Where(x => x.Enabled).ToListAsync();
        }

        public async Task<Role> GetRoleBydId(int id)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteRole(int id)
        {
            var role = await this.GetRoleBydId(id);
            if (role == null)
            {
                throw new Exception("Role doesn't exist");
            }

            role.Enabled = false;
            await _dbContext.SaveChangesAsync();
        }
    }
}
