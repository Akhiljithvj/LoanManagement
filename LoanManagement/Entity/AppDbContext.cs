﻿using LoanManagement.Entity;
using Microsoft.EntityFrameworkCore;

namespace UserAuthentication.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerOnboarding> CustomerOnboardings { get; set; }
        public DbSet<LoanGeneration> LoanGenerations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
