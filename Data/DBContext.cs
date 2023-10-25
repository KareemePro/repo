using FoodDelivery.Models.DominModels;
using FoodDelivery.Models.DominModels.Address;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;
using System.Security.Principal;

namespace FoodDelivery.Data;

public class DBContext : IdentityDbContext<User>
{
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Chief>(entity => { entity.ToTable("Chiefs"); });
        builder.Entity<Customer>(entity => { entity.ToTable("Customers"); });
        builder.Entity<Meal>()
            .HasOne(e => e.MealTag)
            .WithOne(e => e.Meal);
        builder.Entity<Tag>()
            .HasOne(e => e.MealTag)
            .WithOne(e => e.Tag);
        builder.Entity<MealTag>()
            .HasKey(e => new { e.TagID, e.MealID });
    }
    public DbSet<Chief> Chiefs { get; set; }

    public DbSet<Meal> Meals { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Governorate> Governorates { get; set; }

    public DbSet<District> Districts { get; set; }

    public DbSet<Street> Streets { get; set; }

    public DbSet<Building> Buildings { get; set; }

}
