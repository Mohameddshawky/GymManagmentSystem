using GymManagmentDAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Data.Configurations
{
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(o => o.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(o => o.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("GymUser_Email_Check", "Email like '_%@_%.%'");
                t.HasCheckConstraint("GymUser_PhoneNumber_Check", "PhoneNumber like '01%' and PhoneNumber not like '%[^0-9]%' ");

            });
            builder.HasIndex(o => o.Email).IsUnique();
            builder.HasIndex(o => o.PhoneNumber).IsUnique();
           

            builder.Property(o => o.PhoneNumber)
                .HasColumnType("varchar")
                .HasMaxLength(11);

            builder.OwnsOne(o => o.Address, a =>
            {
                a.Property(x => x.Street)
                  .HasColumnType("varchar")
                  .HasColumnName("Street")
                  .HasMaxLength(30);
                a.Property(x => x.City)
                  .HasColumnType("varchar")
                  .HasColumnName("City")
                  .HasMaxLength(30);
                a.Property(x => x.BuildingNumber)
                .HasColumnName("BuildingNumber");
            });
        }
    }
}
