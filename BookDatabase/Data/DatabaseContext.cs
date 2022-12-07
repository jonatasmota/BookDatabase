using BookDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDatabase.Data
{
    public class DatabaseContext : DbContext
    {
        // Constructor
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<BookModel> Books { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
