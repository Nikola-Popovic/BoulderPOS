using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BoulderPOS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Persistence.Builder
{
    public static class UserIdentityModelBuilder
    {
        public static ModelBuilder ConfigureUserIdentityModelBuilder(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(Roles.AdminRole.ToRole());
            modelBuilder.Entity<Role>().HasData(Roles.EmployeeRole.ToRole());

            modelBuilder.Entity<UserRole>().HasKey(x => new {x.RoleId, x.UserId});
            modelBuilder.Entity<UserRole>().HasOne(x => x.User)
                .WithMany(x => x.Roles).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<UserRole>().HasOne(x => x.Role)
                .WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();

            return modelBuilder;
        }
    }
}
