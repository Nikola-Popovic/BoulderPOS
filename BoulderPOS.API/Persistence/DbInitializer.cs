using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext ctx)
        {
            ctx.Database.EnsureCreated();
            ctx.Database.Migrate();
        }
    }
}
