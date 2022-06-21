using DAL.Entities;
using DAL.Entities.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class AuctionDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public AuctionDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AuctionDb;Trusted_Connection=True;");
        }
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
        { }

        public virtual DbSet<User> Persons { get; set; }
        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<AuctionCategory> AuctionCategories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StatusConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new AuctionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new AuctionCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ItemCategoryConfiguration());
        }
    }
}
