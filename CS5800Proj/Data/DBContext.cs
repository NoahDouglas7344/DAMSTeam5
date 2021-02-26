using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CS5800Proj.Models;

    public class DBContext : DbContext
    {
        public DBContext (DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<CS5800Proj.Models.User> User { get; set; }
    }
