using GymManagmentDAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace GymManagmentDAL.Data.Contexts
{
    internal class GymDbcontext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=SHAWKY\\MSQLSERVER;Database=GymManagmentDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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