﻿using Data.Entities;
using Data.Entities.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AuctionDbContext : DbContext
    {
        /* public AuctionDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AuctionDb;Trusted_Connection=True;");
        }*/
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
        { }

        public virtual DbSet<User> Persons { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StatusConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new ItemCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ItemPhotoConfiguration());
        }
    }
}
