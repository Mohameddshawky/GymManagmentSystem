using GymManagmentDAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace GymManagmentDAL.Data.Contexts
{
    public class GymDbcontext : IdentityDbContext<ApplicationUser>
    {
        public GymDbcontext(DbContextOptions<GymDbcontext> options):base(options)
        {

        }
      
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ApplicationUser>
                (x =>
                {
                    x.Property(a => a.FirstName)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);
                    x.Property(a => a.LastName)
                    .HasColumnType("varchar")
                    .HasMaxLength(50);

                });
        }
      
        public DbSet<Member> Members { get; set; }
        public DbSet<HealthRecord> healthRecords { get; set; }
        public DbSet<Trainer> trainers { get; set; }
        public DbSet<Plan> plans { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Session> sessions { get; set; }
        public DbSet<MemberShip> memberShips { get; set; }
        public DbSet<MemberSession> memberSessions { get; set; }
    }
}