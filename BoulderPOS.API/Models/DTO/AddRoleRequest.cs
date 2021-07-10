using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoulderPOS.API.Models.DTO
{
    public class AddRoleRequest
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
