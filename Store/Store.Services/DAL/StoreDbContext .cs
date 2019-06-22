using System;
using System.Collections.Generic;
using System.Data.Entity;
using Store.Models.Models;

namespace Store.Services.DAL
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext() : base("name = StoreDbContextDB")
            {

            }
        public DbSet<Products> Product { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Company> Companies { get; set; }
       

    }
}