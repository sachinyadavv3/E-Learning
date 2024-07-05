using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LetsLearnTogether.Models
{
    public class DBContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryItem> CategoryItem { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<UserCategory> UserCategory { get; set; }
    }
}