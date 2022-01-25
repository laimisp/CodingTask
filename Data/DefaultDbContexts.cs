using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTask.Data
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
        : base(options)
        {
        }

        public DbSet<Tax> Tax { get; set; }
        public DbSet<Municipality> Municipality { get; set; }
    }
}
