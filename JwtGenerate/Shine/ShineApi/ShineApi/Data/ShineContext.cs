using Microsoft.EntityFrameworkCore;
using ShineApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShineApi.Data
{
    public class ShineContext : DbContext
    {
        public ShineContext (DbContextOptions<ShineContext> options) : base(options)
        {   
        }
        public DbSet<Client> Client { get; set; }
    }
}
