using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BoulderPOS.API.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled = true;
        [JsonIgnore]
        public virtual List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

    public class RoleDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Role ToRole ()
        {
            return new Role()
            {
                Description = this.Description,
                Id = this.Id,
                Name = this.Name
            };
        }
    }

    public static class Roles
    {
        public static RoleDefinition AdminRole = new RoleDefinition()
            {Id = 9997, Description = "Can do admin tasks", Name = "Admin"};

        public static RoleDefinition EmployeeRole = new RoleDefinition()
            {Id = 663, Description = "Can do employee tasks", Name = "Employee"};
    }
}
