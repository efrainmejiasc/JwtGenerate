using Microsoft.EntityFrameworkCore;
using ShineApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShineApi.Models
{
    public class ShineContext : DbContext
    {
        public ShineContext()
        {
        }

        public ShineContext (DbContextOptions<ShineContext> options) : base(options)
        {   
        }
        public DbSet<Models.Client> Client { get; set; }
        public DbSet<Models.CodeToVerification> CodeToVerification { get; set; }
    }
}
